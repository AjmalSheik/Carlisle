using AutoMapper;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Interfaces.Doctors;
using DLM.Domain.Entities;
using DLM.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Commands
{
    public class CreateDoctorCommand : IRequest<DoctorDTO>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
    }

    public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, DoctorDTO>
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public CreateDoctorHandler(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> Handle(CreateDoctorCommand request, CancellationToken ct)
        {


            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Specialization = request.Specialization,
                LicenseNumber = request.LicenseNumber,
                LicenseExpiryDate = request.LicenseExpiryDate,

               
                Status = request.LicenseExpiryDate < DateTime.UtcNow
           ? DoctorStatus.Expired
           : DoctorStatus.Active,

                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repo.CreateDoctorAsync(doctor);

            var doctorDto = _mapper.Map<DoctorDTO>(doctor);

            return doctorDto;
        }
    }
}
