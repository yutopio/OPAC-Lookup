using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace OpacLookup
{
	public class ItemRecord
	{
		public FileType Type { get; set; }
		public string Name { get; set; }
		public string URL { get; set; }
		public Tuple<string, string>[] Other { get; set; }
		public string BibID { get; set; }
		public string NCID { get; set; }
	}

	public enum FileType
	{
		Book = 10,		// 図書, 和図書, 洋図書
		AV = 11,		// AV
		EBook = 19,		// 電子ブック
		Magazine = 20,	// 雑誌, 和雑誌, 洋雑誌
		EJournal = 29,	// 電子ジャーナル
		Unknown = -1
	}

	class UTOpacLookup
	{
		const string lookupBibid = "https://opac.dl.itc.u-tokyo.ac.jp/opac/opac_details.cgi?lang=0&amode=11&bibid={0}";

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

		public static ItemRecord[] SearchByUrl(string url)
		{
			try
			{
				// First we look up the book by UT OPAC.
				var items = ObtainBookListOpac(url);
				if (items.Length != 0) return items;

				// Didn't find the book on UT or Webcat.
				throw new ApplicationException("書籍が見つかりませんでした。");
			}
			catch (IndexOutOfRangeException) { throw new ApplicationException("OPAC での検索結果の処理中にエラーが発生しました。このプログラムの設計に問題があります。"); }
		}

		public static ItemRecord[] ObtainBookListOpac(string url)
		{
			// Download the search result page.
			var list = DownloadUTF8(url);

			// Look for the items list. If not found, the query didn't have any matches.
			var i = list.IndexOf("<div id=\"main_list\">");
			if (i == -1) return new ItemRecord[0];

			var j = list.IndexOf("<div class=\"link_block\">", i);
			var searchResult = list.Substring(i, j - i);

			// Format unescaped URLs (& --> &amp;)
			searchResult = new Regex("href=\"javascript:DisplayWindow\\(([^\"]+),'1'\\);\"").Replace(searchResult,
				(MatchEvaluator)(x => "href=\"" + x.Groups[1].Value.Replace("&", "&amp;").Replace("&amp;amp;", "&amp;") + "\""));

			// Format unclosed tags.
			searchResult = new Regex("\\<((br|img|input)([^\\>]*[^/])?)\\>").Replace(searchResult,
				(MatchEvaluator)(x => string.Format("<{0}/>", x.Groups[1].Value)));

			// Why the heck OPAC generates such a bad-mannered HTML!?
			// Who implemeneted that system??

			// Parse as an XML document and process the useful part into ItemRecord.
			return XDocument.Parse(searchResult).Element("div").Elements("table")
				.Select(x => ProcessOpacItem(x.Element("tr").Elements("td").ToArray())).ToArray();
		}

		static ItemRecord ProcessOpacItem(XElement[] item)
		{
			var data = item[0].Elements("input").Select(x => x.Attribute("value").Value).ToArray();
			var bibid = data[0];
			var type = int.Parse(data[1]);
			var title = ProcessLinkName(item[2].Element("span"));
			var other = new List<Tuple<string, string>>();
			foreach (var elem in item[2].Elements("div").Where(x => x.Attribute("class").Value == "other").First().Nodes())
			{
				if (elem is XText) other.Add(new Tuple<string, string>(((XText)elem).Value, null));
				else if (elem is XElement)
				{
					var proc = ProcessLinkName((XElement)elem);
					other.Add(new Tuple<string, string>(proc.Item1, proc.Item2 != null ? proc.Item2.BibID : null));
				}
			}

			return new ItemRecord
			{
				Type = (FileType)type,
				Name = title.Item1,
				URL = title.Item2 != null ? title.Item2.URL : null,
				Other = other.ToArray(),
				BibID = bibid,
				NCID = null
			};
		}

		static Tuple<string, ItemRecord> ProcessLinkName(XElement span)
		{
			try
			{
				var link = span.Element("a");
				var href = link.Attribute("href").Value;
				var text = link.Element("strong").Value;

				if (href.StartsWith("'/opac/opac_details.cgi?"))
				{
					var i = href.IndexOf("&bibid=");
					if (i != -1)
					{
						i += "&bibid=".Length;
						var record = new ItemRecord { BibID = href.Substring(i, href.IndexOf('&', i) - i) };
						return new Tuple<string, ItemRecord>(text, record);
					}
					i = href.IndexOf("&ncid=");
					if (i != -1)
					{
						i += "&ncid=".Length;
						var record = new ItemRecord { NCID = href.Substring(i, href.IndexOf('&', i) - i) };
						return new Tuple<string, ItemRecord>(text, record);
					}
					return new Tuple<string, ItemRecord>(text, null);
				}
				else if (href.StartsWith("'http://vs2ga4mq9g"))
				{
					var url = href.Substring(1, href.IndexOf('\'', "'http://vs2ga4mq9g".Length) - 1);
					if (href.Contains("encodeURIComponent")) url += HttpUtility.UrlEncode(text);
					return new Tuple<string, ItemRecord>(text, new ItemRecord { URL = url });
				}
				else throw new NotImplementedException();
			}
			catch { return new Tuple<string, ItemRecord>(span.Value, null); }
		}

		static string DownloadUTF8(string url)
		{
			try
			{
				var c = new WebClient();
				return Encoding.UTF8.GetString(c.DownloadData(url));
			}
			catch (WebException exp) { throw new ApplicationException("OPAC に接続中にエラーが発生しました。ネットワークに関係する問題が発生しています。", exp); }
		}

		public static Tuple<List<Tuple<string, string>>, List<Dictionary<string, string>>> ExtractDataByDetailPage(ItemRecord bookID)
		{
			string lookupURL;
			if (bookID.BibID != null) lookupURL = string.Format(lookupBibid, bookID.BibID);
			else throw new ArgumentException("bookID");
			var detailPage = DownloadUTF8(lookupURL);

			// Start to get the detail.
			var detail = new List<Tuple<string, string>>();

			int i, j;
			// Obtain the book title.
			i = detailPage.IndexOf(bookDetailTitleBegin) + bookDetailTitleBegin.Length;
			var title = detailPage.Substring(i, detailPage.IndexOf(bookDetailTitleEnd, i) - i);
			detail.Add(new Tuple<string, string>("Title", title));

			// Obtain each detail field of the book.
			while ((j = detailPage.IndexOf(bookDetailEntryBegin, i)) != -1)
			{
				i = detailPage.IndexOf(bookDetailEntryType1, j + bookDetailEntryBegin.Length);
				i = detailPage.IndexOf(bookDetailEntryType2, i) + bookDetailEntryType2.Length;
				j = detailPage.IndexOf(bookDetailEntryValue, i);
				var type = detailPage.Substring(i, j - i);
				j += bookDetailEntryValue.Length;
				i = detailPage.IndexOf(bookDetailEntryEnd, j);
				var value = detailPage.Substring(j, i - j);
				detail.Add(new Tuple<string, string>(type, value));
			}

			// If the book is stored at UT Library, get the location list.
			var collectionList = new List<Dictionary<string, string>>();
			while ((i = detailPage.IndexOf(libraryEntryBegin, i)) != -1)
			{
				j = detailPage.IndexOf(libraryEntryEnd, i += libraryEntryBegin.Length);
				var doc = XDocument.Parse("<A>" + detailPage.Substring(i, j - i) + "</A>");

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
				collectionList.Add(book);
			}

			return new Tuple<List<Tuple<string, string>>, List<Dictionary<string, string>>>(detail, collectionList);
		}

		public static string ValidateISBN(string code)
		{
			if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");
			code = code.Trim().Replace("-", "");
			if (code.Length == 10)
			{
				var sum = 0;
				for (var i = 0; i < 9; i++)
				{
					var t = code[i] - '0';
					if (t < 0 || t > 9) throw new ArgumentOutOfRangeException("code");
					sum += t * (10 - i);
				}
				if (!"0X987654321".Contains(code[9])) throw new ArgumentOutOfRangeException("code");
				if (code[9] != "0X987654321"[sum % 11]) throw new ArgumentException();
			}
			else if (code.Length == 13)
			{
				var sum = 0;
				for (var i = 0; i < 12; i++)
				{
					var t = code[i] - '0';
					if (t < 0 || t > 9) throw new ArgumentOutOfRangeException("code");
					sum += t * ((i & 1) == 0 ? 1 : 3);
				}
				if (!"0987654321".Contains(code[12])) throw new ArgumentOutOfRangeException("code");
				if (code[12] != "0987654321"[sum % 10]) throw new ArgumentException();
			}
			else throw new ArgumentOutOfRangeException("code");
			return code;
		}

		public static void AnalyzeCodeString(string codes, out string BibID, out string NCID, out string ISBN)
		{
			try
			{
				codes = codes.Replace("&nbsp;", " ");
				var nodes = XElement.Parse("<A>" + codes + "</A>").Nodes().ToArray();
				if (nodes.Length == 2)
				{
					BibID = null;
					NCID = nodes[1].ToString();
					ISBN = null;
				}
				else if (nodes.Length != 7)
				{
					BibID = nodes[1].ToString();
					NCID = nodes[3].ToString();
					ISBN = null;
				}
				else
				{
					BibID = nodes[1].ToString();
					NCID = nodes[3].ToString();
					ISBN = nodes[6].ToString();
				}
			}
			catch (Exception exp) { throw new ApplicationException("書誌 ID および NCID の取得中にエラーが発生しました。", exp); }
		}
	}
}
