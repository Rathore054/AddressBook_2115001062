﻿using FluentValidation;
using ModelLayer.Model;

public class EntryValidator : AbstractValidator<EntryModel>
{
    public EntryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^[0-9]{10}$").WithMessage("Phone number must be exactly 10 digits");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255);
    }
}