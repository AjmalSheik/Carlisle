using AutoMapper;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Interfaces.Doctors;
using DLM.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.Queries
{
    public class GetDoctorsQuery : IRequest<List<DoctorSPDto>>
    {
        public string? Search { get; set; }
        public int? Status { get; set; }
    }
    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, List<DoctorSPDto>>
    {
        private readonly IDoctorRepository _repo;
        private readonly IMapper _mapper;

        public GetDoctorsQueryHandler(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DoctorSPDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {

            var result= await _repo.GetDoctorsAsync(request.Search,request.Status);



            return result;

        }
    }
}
