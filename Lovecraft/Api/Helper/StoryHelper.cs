using System.Text;

namespace Lovecraft.Api.Helper;

public class StoryHelper
{
	private int minWordsByPage = 240;
	private int maxWordsByPage = 300;
	public List<string> extractPagesFromText(string text)
	{
		string[] words = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

		List<string> pages = new List<string>();
		//StringBuilder currentPage = new StringBuilder();
		string currentPage = "";

		int wordCount = 0;
		foreach (string word in words)
		{
			//wordCount++;

			if (wordCount < minWordsByPage)
			{
				currentPage = currentPage +  " " + word;
				wordCount++;
			}
			else
			{
				if (IsLastPunctuation(word))
				{
					currentPage = currentPage + " " + word;
					pages.Add(currentPage.TrimEnd());
					wordCount = 0;
					currentPage = "";
				}
				else
				{
					currentPage = currentPage + " " + word;
					if (wordCount == maxWordsByPage)
					{
						pages.Add(currentPage.TrimEnd());
						wordCount = 0;
						currentPage = "";
					}
					else
					{
						wordCount++;
					}
				}

			}
		}
		return pages;
	}

	private bool IsLastPunctuation(string text)
	{
		char lastChar = text.TrimEnd().LastOrDefault();
		return lastChar == '.' || lastChar == '?' || lastChar == '!';
	}
}