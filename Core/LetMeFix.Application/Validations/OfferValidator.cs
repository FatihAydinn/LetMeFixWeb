using FluentValidation;
using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Validations
{
    public class OfferValidator : AbstractValidator<OffersDTO>
    {
        private readonly IGenericRepository<Job> _jobRepository;
        public OfferValidator(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price cannot be less than 0!");

            RuleFor(x => x).CustomAsync(async (dto, context, cancellationToken) =>
            {
                var job = await _jobRepository.GetByIdAsync(dto.JobId);

                if (job == null)
                {
                    context.AddFailure("JobId", "This job is not available anymore.");
                    return;
                }

                if (job.Id != dto.Id) 
                {
                    context.AddFailure("JobVersion", "The job status has changed. Please refresh the page.");
                }
            });
        }
    }
}
