using SendGrid;
using SendGrid.Helpers.Mail;
using ContactProBlazor.Data;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ContactProBlazor.Services
{
    public class SendGridService : IEmailSender, IEmailSender<ApplicationUser>
    {
        private readonly string _sendGridKey;
        private readonly string _fromAddress;
        private readonly string _fromName;

        public SendGridService(IConfiguration config)
        {
            // IConfiguration by default looks in Environment variables/user secrets/appsettings
            _sendGridKey = config["SendGridKey"] ?? Environment.GetEnvironmentVariable("SendGridKey")
                ?? throw new InvalidOperationException("SendGridKey not found in config!");

            _fromAddress = _sendGridKey = config["SendGridEmail"] ?? Environment.GetEnvironmentVariable("SendGridEmail")
                ?? throw new InvalidOperationException("SendGridEmail not found in config!");

            _fromName = _sendGridKey = config["SendGridName"] ?? Environment.GetEnvironmentVariable("SendGridName") 
                ?? throw new InvalidOperationException("SendGridName not found in config!");
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) => 
            SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) => 
            SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) => 
            SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
    }
}