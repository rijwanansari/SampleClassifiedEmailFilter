using System.Text.RegularExpressions;

namespace SampleCalssifiedEmailFilter.Services;

/// <summary>
/// Implementation of IEmailFilter to filter classified words in email body
/// </summary>
internal class ClassifiedEmailFilter : IEmailFilter
{
    /// <summary>
    /// Filter email body for classified words
    /// </summary>
    /// <param name="classifiedWords"></param>
    /// <param name="emailText"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<(bool, string)> FilterEmailBodyAsync(List<string> classifiedWords, string emailText)
    {
        if (string.IsNullOrEmpty(emailText))
        {
            throw new ArgumentNullException(nameof(emailText));
        }

        if (classifiedWords == null || classifiedWords.Count == 0)
        {
            throw new ArgumentNullException(nameof(classifiedWords));
        }

        bool isClassified = false;
        string censoredText = emailText;

        foreach (string word in classifiedWords)
        {
            if (emailText.Contains(word, StringComparison.OrdinalIgnoreCase))
            {
                isClassified = true;
                censoredText = Regex.Replace(censoredText, $@"\b{Regex.Escape(word)}\b", "*****", RegexOptions.IgnoreCase);
            }
        }

        return (isClassified, censoredText);
    }

}