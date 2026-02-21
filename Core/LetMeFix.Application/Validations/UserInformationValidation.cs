using FluentValidation;
using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Validations
{
    public class UserInformationValidation : AbstractValidator<UserInformationsDTO>
    {
        public UserInformationValidation() {
            RuleFor(x => x.BirthDate).NotEmpty().Must(age => age <= DateTime.Today.AddYears(-18)).WithMessage("The user must be over 18!");
            RuleFor(x => x.MobileNumber).Length(10).WithMessage("Please enter a 10-digit phone number (e.g., 5551234567)").Must(phone => phone.All(char.IsDigit)).WithMessage("Enter only numbers!");
            RuleFor(x => x.Skills).NotEmpty().Must(y => y.Count >= 3).WithMessage("You must select at least 3 skills!");
        }
    }
}
