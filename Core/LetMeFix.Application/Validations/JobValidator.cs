using FluentValidation;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Validations
{
    public class JobValidator : AbstractValidator<Job>
    {
        public JobValidator() {
            RuleFor(x => x.AvailableDays).NotEmpty().WithMessage("You must select at least one day!");
            RuleFor(x => x.AvailableHours).NotEmpty().WithMessage("You must select at least hour day!");
            RuleFor(x => x.Description).NotNull().WithMessage("Description required!").MinimumLength(10).MaximumLength(500);
            RuleFor(x => x.Title).NotNull().WithMessage("Title required!").MinimumLength(5).MaximumLength(50);
        }
    }
}
