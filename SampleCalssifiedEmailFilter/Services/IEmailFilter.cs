namespace SampleCalssifiedEmailFilter.Services;
/// <summary>
/// Interface for email filter
/// </summary>
public interface IEmailFilter
{
    /// <summary>
    /// Interface method to filter email body
    /// </summary>
    /// <param name="classifiedWords"></param>
    /// <param name="emailText"></param>
    /// <returns></returns>
    Task<(bool, string)> FilterEmailBodyAsync(List<string> classifiedWords, string emailText);

}
