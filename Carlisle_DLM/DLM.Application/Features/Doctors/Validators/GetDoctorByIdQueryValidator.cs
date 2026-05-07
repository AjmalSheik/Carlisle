using DLM.Application.Features.Doctors.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Validators
{
    public class GetDoctorByIdQueryValidator : AbstractValidator<GetDoctorByIdQuery>
    {
        public GetDoctorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Doctor Id is required");
        }
    }
}
