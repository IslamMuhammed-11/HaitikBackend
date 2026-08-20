using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Common.Models.Email;

public sealed class EmailMessage
{
    public string Subject { get; private set; } = string.Empty;

    public string Body { get; private set; } = string.Empty;

    public string To { get; private set; } = string.Empty;

    public static EmailMessage Create(string subject, string body, string to)
    {
        if (string.IsNullOrEmpty(body))
            throw new ArgumentNullException("body Can't be null");

        if (string.IsNullOrEmpty(to) || !to.Contains("@"))
            throw new ArgumentException("The email isn't valid");

        return new EmailMessage
        {
            Subject = subject,
            Body = body,
            To = to
        };
    }


}

