using AutoMapper;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Interfaces.Doctors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Queries
{
    public class GetDoctorByIdQuery : IRequest<DoctorDTO>
    {
        public Guid Id { get; set; }
    }


    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDTO>
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public GetDoctorByIdQueryHandler(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _repo.GetDoctorById(request.Id);

            if (doctor == null)
                return null;

            return _mapper.Map<DoctorDTO>(doctor);
        }
    }

}
