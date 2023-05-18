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
		StringBuilder currentPage = new StringBuilder();

		int wordCount = 0;
		foreach (string word in words)
		{
			wordCount++;

			// If adding the current word would put us over the page limit, check if we need to end the page
			if (currentPage.Length + word.Length + 1 > maxWordsByPage)
			{
				if (currentPage.Length >= minWordsByPage && IsLastPunctuation(currentPage.ToString()))
				{
					pages.Add(currentPage.ToString().TrimEnd());
					currentPage.Clear();
					wordCount = 0;
				}
				else
				{
					pages.Add(currentPage.ToString().TrimEnd());
					currentPage.Clear().Append(word).Append(' ');
					wordCount = 1;
				}
			}
			else
			{
				currentPage.Append(word).Append(' ');
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