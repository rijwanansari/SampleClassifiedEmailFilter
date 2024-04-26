using System.Net.Mail;
using System.Net;

namespace SampleCalssifiedEmailFilter.Services;
/// <summary>
/// Smtp email service
/// </summary>
public class SmtpEmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailFilter _emailFilter;
    private readonly List<string> _classifiedWords;
    public SmtpEmailService(IConfiguration configuration, IEmailFilter emailFilter)
    {
        _configuration = configuration;
        _emailFilter = emailFilter;
        _classifiedWords = _configuration.GetSection("ClassifiedWords").Get<List<string>>();
    }

    /// <summary>
    /// Send email
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipients"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task SendEmailAsync(string subject, string body, params string[] recipients)
    {
        try
        {
            string smtpServer = _configuration["SmtpSettings:Server"];
            int port = _configuration.GetValue<int>("SmtpSettings:Port");
            string senderEmail = _configuration["SmtpSettings:Email"];
            string senderPassword = _configuration["SmtpSettings:Password"];

            using (var client = new SmtpClient(smtpServer, port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress(senderEmail);

                foreach (var recipient in recipients)
                {
                    message.To.Add(recipient);
                }

                message.Subject = subject;
                message.Body = body;

                await client.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error sending email", ex);
        }
    }

    /// <summary>
    /// Send email with filter
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipients"></param>
    /// <returns></returns>
    public async Task SendEmailWithFilterAsync(string subject, string body, params string[] recipients)
    {
        var (isClassified, censoredText) = await _emailFilter.FilterEmailBodyAsync(_classifiedWords, body);

        if (isClassified)
        {
            await SendEmailAsync(subject, censoredText, recipients);
        }
        else
        {
            await SendEmailAsync(subject, body, recipients);
        }
    }

}
