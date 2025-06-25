using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"Email sent to {email} with subject '{subject}' and body: {htmlMessage}");
        return Task.CompletedTask;
    }
}
