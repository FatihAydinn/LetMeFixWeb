using FluentValidation;
using LetMeFix.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Validations
{
    public class ChatSessionValidator : AbstractValidator<ChatSessionDTO>
    {
        public ChatSessionValidator()
        {
            RuleFor(x => x.MessageContent[0].Content).NotEmpty().MinimumLength(2).MaximumLength(50).When(x => x.MessageContent[0].Type == "text").WithMessage("Message must be between 2 and 200 characters!");
        }
    }
}
