using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class StoryHelper
{
    public List<string> SplitIntoPages(string text, int maxWordsPerPage)
    {
        string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        List<string> pages = new List<string>();
        string currentPage = "";
        int wordCount = 0;

        foreach (string line in lines)
        {
            string[] words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                currentPage += word + " ";
                wordCount++;

                if (wordCount >= maxWordsPerPage)
                {
                    pages.Add(currentPage.Trim());
                    currentPage = "";
                    wordCount = 0;
                }
            }
            currentPage += "</br>";
        }
        if (!string.IsNullOrEmpty(currentPage))
        {
            pages.Add(currentPage.Trim());
        }
        return pages;
    }
}