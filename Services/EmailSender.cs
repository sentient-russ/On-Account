﻿using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace OnAccount.Services;
public class EmailService
{
    private string GC_Email_Pass;

    public EmailService()
    {
        GC_Email_Pass = Environment.GetEnvironmentVariable("GC_Email_Pass");
    }
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }
    public async Task Execute(string subject, string message, string toEmail)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("cs@magnadigi.com"));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };
        using var smtp = new SmtpClient();
        smtp.Connect("us2.smtp.mailhostbox.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("cs@magnadigi.com", GC_Email_Pass);
        var response = smtp.Send(email);
        smtp.Disconnect(true);
    }
}

