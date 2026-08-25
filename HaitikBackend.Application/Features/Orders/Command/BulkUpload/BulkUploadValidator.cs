using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.Orders.Command.BulkUpload;

public class BulkUploadValidator : AbstractValidator<BulkUploadCommand>
{
    public BulkUploadValidator()
    {
        RuleFor(e => e.File.Validate());
    }
}
