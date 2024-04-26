namespace SampleCalssifiedEmailFilter.Services;

/// <summary>
/// Interface for email service
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Send email
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipients"></param>
    /// <returns></returns>
    Task SendEmailAsync(string subject, string body, params string[] recipients);
    /// <summary>
    /// Send email with filter
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipients"></param>
    /// <returns></returns>
    Task SendEmailWithFilterAsync(string subject, string body, params string[] recipients);
}
