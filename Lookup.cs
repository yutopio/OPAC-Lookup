using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace OpacLookup
{
	class Program
	{
		const string searchUT = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_list.cgi?smode=1&cmode=0&kywd1_exp={0}&con1_exp=6";
		const string searchWebCat = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_list.cgi?smode=1&cmode=1&nii_kywd1_exp={0}&nii_con1_exp=6";
		const string lookupBibid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?lang=0&amode=11&bibid={0}";
		const string lookupNcid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?amode=13&dbname=BOOK&ncid={0}";

		/* For Lookup by ISSN (magazines)
		const string searchUT = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_list.cgi?smode=1&cmode=0&kywd1_exp={0}&con1_exp=7";
		const string searchWebCat = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_list.cgi?smode=1&cmode=1&nii_kywd1_exp={0}&nii_con1_exp=7";
		const string lookupBibid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?lang=0&amode=12&bibid={0}";
		const string lookupNcid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?amode=13&dbname=BOOK&ncid={0}";
		*/

		const string resultMarker = "list_result";
		const string bibidMarker = "bibid=";
		const string ncidMarker = "ncid=";

		const string bookDetailTitleBegin = "<title>";
		const string bookDetailTitleEnd = "</title>";
		const string bookDetailEntryBegin = "bb_item_tr";
		const string bookDetailEntryType1 = "<td><span ";
		const string bookDetailEntryType2 = "=\"";
		const string bookDetailEntryValue = "\">";
		const string bookDetailEntryEnd = "</span></td>";

		const string libraryEntryBegin = "bl_item_tr\">";
		const string libraryEntryEnd = "</tr>";
		const string engLib2Marker = "工2・図書室";

		static void Main(string[] args)
		{
			var c = new WebClient();

			var isbnI = 0;
			var isbn = new[] { "9784901347259", "9784901347266" };

		Start:
			if (isbnI == isbn.Length) return;

			var ISBN = isbn[isbnI++];
			var fs = new FileStream(string.Format(@"D:\Desktop\books\{0}.xml", ISBN), FileMode.Create, FileAccess.Write);
			var xw = XmlWriter.Create(fs);
			var start = Environment.TickCount;

			try
			{
				xw.WriteStartDocument();
				xw.WriteStartElement("Book");

				// First we look up the book by UT OPAC.
				string searchPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(searchUT, ISBN)));
				string detailPage = null;
				var index = searchPage.IndexOf(resultMarker);
				if (index != -1)
				{
					// If the book is available at UT library, obtain the bibID and download the detailed information.
					index = searchPage.IndexOf(bibidMarker, index) + bibidMarker.Length;
					var bibid = searchPage.Substring(index, searchPage.IndexOf('&', index) - index);
					xw.WriteStartAttribute("bibid");
					xw.WriteValue(bibid);
					xw.WriteEndAttribute();

					detailPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(lookupBibid, bibid)));
				}
				else
				{
					// If unavailable, look up the book from WebCat.
					searchPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(searchWebCat, ISBN)));
					index = searchPage.IndexOf(resultMarker);
					if (index != -1)
					{
						index = searchPage.IndexOf(ncidMarker, index) + ncidMarker.Length;
						var ncid = searchPage.Substring(index, searchPage.IndexOf('&', index) - index);
						xw.WriteStartAttribute("ncid");
						xw.WriteValue(ncid);
						xw.WriteEndAttribute();

						detailPage = Encoding.UTF8.GetString(c.DownloadData(string.Format(lookupNcid, ncid)));
					}
					else
					{
						// Didn't find the book on UT or WebCat.
						// throw new ApplicationException("No such book found either at UT Library or on WebCat.");
						goto Start;
					}
				}

				// Assure no more result hit.
				Debug.Assert(searchPage.IndexOf(resultMarker, index) == -1);

				// Start to get the detail.

				var detail = new List<Tuple<string, string>>();

				index = detailPage.IndexOf(bookDetailTitleBegin) + bookDetailTitleBegin.Length;
				var title = detailPage.Substring(index, detailPage.IndexOf(bookDetailTitleEnd, index) - index);
				detail.Add(new Tuple<string, string>("Title", title));

			BeginDetail:
				var index2 = detailPage.IndexOf(bookDetailEntryBegin, index);
				if (index2 == -1) goto EndDetail;
				index = detailPage.IndexOf(bookDetailEntryType1, index2 + bookDetailEntryBegin.Length);
				index = detailPage.IndexOf(bookDetailEntryType2, index) + bookDetailEntryType2.Length;
				index2 = detailPage.IndexOf(bookDetailEntryValue, index);
				var type = detailPage.Substring(index, index2 - index);
				index2 += bookDetailEntryValue.Length;
				index = detailPage.IndexOf(bookDetailEntryEnd, index2);
				var value = detailPage.Substring(index2, index - index2);
				detail.Add(new Tuple<string, string>(type, value));
				goto BeginDetail;

			EndDetail:
				xw.WriteStartElement("Detail");
				foreach (var keyValue in detail)
				{
					xw.WriteStartElement("Record");
					xw.WriteStartAttribute("key");
					xw.WriteValue(keyValue.Item1);
					xw.WriteEndAttribute();
					xw.WriteCData(keyValue.Item2);
					xw.WriteEndElement();
				}
				xw.WriteEndElement();

				var eng2list = new List<Dictionary<string, string>>();
			Eng2Search:
				index = detailPage.IndexOf(libraryEntryBegin, index);
				if (index != -1)
				{
					index2 = detailPage.IndexOf(libraryEntryEnd, index += libraryEntryBegin.Length);
					var doc = XDocument.Parse("<root>" + detailPage.Substring(index, index2 - index) + "</root>");

					var book = new Dictionary<string, string>();
					foreach (var elem in doc.Descendants("td"))
					{
						var fieldName = elem.Attribute("class").Value;
						string fieldValue = null;
						var link = elem.Element("a");
						if (link != null) fieldValue = link.Value;
						else if (elem.Element("br") == null) fieldValue = elem.Value;
						book.Add(fieldName, fieldValue);
					}
					eng2list.Add(book);

					goto Eng2Search;
				}

				xw.WriteStartElement("Collection");
				foreach (var eng2book in eng2list)
				{
					xw.WriteStartElement("Info");
					foreach (var eng2bookInfo in eng2book)
					{
						xw.WriteStartElement(eng2bookInfo.Key);
						xw.WriteString(eng2bookInfo.Value);
						xw.WriteEndElement();
					}
					xw.WriteEndElement();
				}
				xw.WriteEndElement();
			}
			finally
			{
				xw.WriteEndDocument();
				xw.Close();
				Console.WriteLine("{0}\t{1}", ISBN, Environment.TickCount - start);
			}

			goto Start;
		}
	}
}
