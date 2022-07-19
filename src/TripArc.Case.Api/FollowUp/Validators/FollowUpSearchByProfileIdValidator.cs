using System;
using FluentValidation;
using TripArc.Case.Shared.FollowUp.InputModels;
using TripArc.Common.Constants;
using TripArc.Common.Extensions;

namespace TripArc.Case.Api.FollowUp.Validators;

public class FollowUpSearchByProfileIdValidator : AbstractValidator<FollowUpSearchByProfileIdInputModel>
{
    public FollowUpSearchByProfileIdValidator()
    {
        RuleFor(x => x.ProfileId).Cascade(CascadeMode.Stop)
            .NotEmptyWithDefaultMessage()
            .GreaterThanWithDefaultMessage(0);
        
        RuleFor(x => x.DueDateFrom).Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(DateTime.MinValue)
            .WithMessage(ValidationErrorMessages.NotEmpty);

        RuleFor(x => x.DueDateTo).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(ValidationErrorMessages.NotEmpty)
            .GreaterThanOrEqualTo(x => x.DueDateFrom.Value)
            .WithMessage("DueDateTo must be greater than or equal to DueDateFrom")
            .When(x => x.DueDateFrom.HasValue);

        RuleFor(x => x.DateCreatedFrom).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(ValidationErrorMessages.NotEmpty)
            .When(x => x.DateCreatedTo.HasValue);

        RuleFor(x => x.DateCreatedTo).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(ValidationErrorMessages.NotEmpty)
            .GreaterThanOrEqualTo(x => x.DateCreatedFrom.Value)
            .WithMessage("DateCreatedTo must be greater than or equal to DateCreatedFrom")
            .When(x => x.DateCreatedFrom.HasValue);
        
        RuleFor(x => x.StartDateFrom).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(ValidationErrorMessages.NotEmpty)
            .When(x => x.StartDateTo.HasValue);
        
        RuleFor(x => x.StartDateTo).Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(ValidationErrorMessages.NotEmpty)
            .GreaterThanOrEqualTo(x => x.StartDateFrom.Value)
            .WithMessage("StartDateTo must be greater than or equal to StartDateFrom")
            .When(x => x.StartDateFrom.HasValue);        
    }
}