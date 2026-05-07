using DLM.Application.Interfaces.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Commands
{
    public class DeleteDoctorCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
    {
        private readonly IDoctorRepository _repo;

        public DeleteDoctorCommandHandler(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _repo.GetDoctorById(request.Id);

            if (doctor == null)
                return false;

            doctor.IsDeleted = true;  

            await _repo.UpdateDoctor(doctor);

            return true;
        }
    }
}
