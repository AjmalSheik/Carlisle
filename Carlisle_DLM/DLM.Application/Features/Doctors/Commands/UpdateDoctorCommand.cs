using AutoMapper;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Interfaces.Doctors;
using DLM.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Commands
{
    public class UpdateDoctorCommand : IRequest<DoctorDTO>
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
    }

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, DoctorDTO>
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public UpdateDoctorCommandHandler(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _repo.GetDoctorById(request.Id);

            if (doctor == null)
                return null;

            
            doctor.FullName = request.FullName;
            doctor.Email = request.Email;
            doctor.Specialization = request.Specialization;
            doctor.LicenseNumber = request.LicenseNumber;
            doctor.LicenseExpiryDate = request.LicenseExpiryDate;

          
            doctor.Status = doctor.LicenseExpiryDate < DateTime.UtcNow
                ? DoctorStatus.Expired
                : DoctorStatus.Active;

            await _repo.UpdateDoctor(doctor);

            return _mapper.Map<DoctorDTO>(doctor);
        }
    }

}
