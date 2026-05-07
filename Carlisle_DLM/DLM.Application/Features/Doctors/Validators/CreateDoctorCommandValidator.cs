using DLM.Application.Features.Doctors.Commands;
using DLM.Application.Interfaces.Doctors;
using FluentValidation;

namespace DLM.Application.Features.Doctors.Validators
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator(IDoctorRepository repo)
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required")
                .MaximumLength(200);


            RuleFor(x => x.Email)
     .NotEmpty().WithMessage("Email is required")
     .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
     .WithMessage("Invalid email format");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License Number is required")
                .MustAsync(async (license, cancellation) =>
                {
                    return !await repo.IsLicenseNumberExisis(license);
                })
                .WithMessage("License Number already exists");

            RuleFor(x => x.LicenseExpiryDate)
                .GreaterThan(DateTime.UtcNow.Date)
                .WithMessage("License must be valid in future");
        }
    }
}