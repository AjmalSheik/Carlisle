using DLM.Application.Features.Doctors.DTOs;
using DLM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Interfaces.Doctors
{
    public interface IDoctorRepository
    {

        Task<bool> IsLicenseNumberExisis(string licenseNumber);
        Task CreateDoctorAsync(Doctor doctor);

        Task<List<DoctorSPDto>> GetDoctorsAsync(string search, int? status);
        Task<Doctor?> GetDoctorById(Guid id);

        Task UpdateDoctor(Doctor doctor);

        Task<bool> IsEmailExistsAsyncForDoctorUpdate(string email, Guid excludeId);
        Task<bool> IsLicenseExistsAsyncForDoctorUpdate(string licenseNumber, Guid excludeId);
    }
}
