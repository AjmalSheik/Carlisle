using AutoMapper;
using DLM.Application.Features.Doctors.Commands;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Domain.Entities;


namespace DLM.Application.Common.Automapper
{
   





        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
        {
           

            CreateMap<Doctor, DoctorDTO>();



            }
        }
  




}

