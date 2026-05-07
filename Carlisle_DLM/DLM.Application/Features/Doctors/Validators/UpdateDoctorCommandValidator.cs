using DLM.Application.Features.Doctors.Commands;
using DLM.Application.Interfaces.Doctors;
using FluentValidation;


namespace DLM.Application.Features.Doctors.Validators
{

    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        private readonly IDoctorRepository _repo;

        public UpdateDoctorCommandValidator(IDoctorRepository repo)
        {
            _repo = repo;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Doctor Id is required");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(BeUniqueEmail)
                .WithMessage("Email already exists");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MustAsync(BeUniqueLicenseNumber)
                .WithMessage("License number already exists");

            RuleFor(x => x.LicenseExpiryDate)
                .NotEmpty();
        }

        private async Task<bool> BeUniqueEmail(UpdateDoctorCommand command, string email, CancellationToken cancellationToken)
        {
            return !await _repo.IsEmailExistsAsyncForDoctorUpdate(email, command.Id);
        }

        private async Task<bool> BeUniqueLicenseNumber(UpdateDoctorCommand command, string license, CancellationToken cancellationToken)
        {
            return !await _repo.IsLicenseExistsAsyncForDoctorUpdate(license, command.Id);
        }
    }
}
