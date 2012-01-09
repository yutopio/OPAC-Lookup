using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace OpacLookup
{
	class CiNiiBooks
	{
		// Organization ID for UT. Used for book search only.
		const string kid = "KI000221";

		// Library IDs in UT. First look up the libraries with the name pattern,
		// then keep it on fano list.
		const string libraryNamePattern = "東京大学";
		static string[] fano;

		// Search endpoints. Reference: http://ci.nii.ac.jp/info/ja/api/b_opensearch.html
		const string bookSearch = "http://ci.nii.ac.jp/books/opensearch/search?format=rss";
		const string librarySearch = "http://ci.nii.ac.jp/books/opensearch/library?format=rss";
		const string holderSearch = "http://ci.nii.ac.jp/books/opensearch/holder?format=rss";

		// Resulting RSS data namespaces.
		const string xmlns = "http://purl.org/rss/1.0/";
		const string rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";

		static CiNiiBooks()
		{
			// Obtain library fanos.
		}

		public static ItemRecord[] SearchByISBN(string ISBN)
		{
			var query = bookSearch + "&isbn=" + ISBN + "&kid=" + kid;
			var c = new WebClient();
			var response = Encoding.UTF8.GetString(c.DownloadData(query));

			var list = new List<ItemRecord>();
			var doc = XDocument.Parse(response);
			foreach (var match in doc.Descendants(XName.Get("item", xmlns)))
			{
				list.Add(new ItemRecord
				{
					Name = match.Element(XName.Get("title", xmlns)).Value,
					NCID = match.Attribute(XName.Get("about", rdf)).Value.Split('/').Last(),
					URL = match.Element(XName.Get("link", xmlns)).Value
				});
			}

			return list.ToArray();
		}
	}
}
