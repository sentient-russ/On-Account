﻿using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using oa;
using System.Diagnostics;

namespace oa.Areas.Identity.Services;
public class EmailService : IEmailSender
{
    private string? emailPass;
    private string? emailServer;
    private string? emailAddress;

    public EmailService(IConfiguration configuration)
    {
        emailPass = Environment.GetEnvironmentVariable("OA_Email_Pass");
        emailAddress = Environment.GetEnvironmentVariable("OA_Email_Address");
        emailServer = Environment.GetEnvironmentVariable("OA_Email_Server");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }
    public async Task Execute(string subject, string message, string toEmail)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailAddress));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };
        using var smtp = new SmtpClient();
        smtp.Connect(emailServer, 465, SecureSocketOptions.Auto);
        smtp.Authenticate(emailAddress, emailPass);
        var response = smtp.Send(email);
        Debug.WriteLine($"Email sent to: {toEmail}, Subject: {subject}, Body: {message}");
        smtp.Disconnect(true);
    }
}