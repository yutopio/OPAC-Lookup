using System;

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

		// Search aliases
		const string bookSearch = "http://ci.nii.ac.jp/books/opensearch/search?";
		const string librarySearch = "http://ci.nii.ac.jp/books/opensearch/library?name=";
		const string holderSearch = "http://ci.nii.ac.jp/books/opensearch/holder?ncid=";

		static CiNiiBooks()
		{
			// Obtain library fanos.
		}

		
	}
}
