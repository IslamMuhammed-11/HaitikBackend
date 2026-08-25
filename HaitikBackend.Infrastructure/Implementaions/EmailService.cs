using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.Email;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Infrastructure.Implementaions;

public class EmailService : IEmailService
{

    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(EmailMessage message, CancellationToken ct = default)
    {
        var Message = _createMessage(message);


        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync
            (_settings.Host,
            _settings.Port,
            MailKit.Security.SecureSocketOptions.StartTls,
            ct);

        await smtp.AuthenticateAsync(
            _settings.Username,
            _settings.Password,
            ct);

        await smtp.SendAsync(Message, ct);

        await smtp.DisconnectAsync(true, ct);
    }


    private MimeMessage _createMessage(EmailMessage message)
    {
        var Message = new MimeMessage();

        Message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

        Message.To.Add(MailboxAddress.Parse(message.To));

        Message.Subject = message.Subject;

        Message.Body = new TextPart("Plain")
        {
            Text = message.Body
        };

        return Message;
    }
}

