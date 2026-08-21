using HaitikBackend.Application.Common.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Abstractions;

public interface IEmailService
{
    public Task SendEmailAsync(EmailMessage message, CancellationToken ct = default);

}
