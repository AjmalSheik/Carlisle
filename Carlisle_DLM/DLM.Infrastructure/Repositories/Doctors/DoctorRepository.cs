using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Interfaces.Doctors;
using DLM.Domain.Entities;
using DLM.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DLM.Infrastructure.Repositories.Doctors
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        // doctor creation
        public async Task CreateDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor?> GetDoctorById(Guid id)
        {
            return await _context.Doctors
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<DoctorSPDto>> GetDoctorsAsync(string search, int? status)
        {
            var searchParam = new SqlParameter("@Search", search ?? (object)DBNull.Value);
            var statusParam = new SqlParameter("@Status", status ?? (object)DBNull.Value);

            var doctors = await _context.Set<DoctorSPDto>()
                .FromSqlRaw("EXEC sp_GetDoctors @Search, @Status", searchParam, statusParam)
                .AsNoTracking()
                .ToListAsync();

            return doctors;
        }

        public async Task<bool> IsEmailExistsAsyncForDoctorUpdate(string email, Guid excludeId)
        {
            return await _context.Doctors.AnyAsync(x =>
             x.Email == email &&
             x.Id != excludeId &&
             !x.IsDeleted);
        }

        public async Task<bool> IsLicenseExistsAsyncForDoctorUpdate(string licenseNumber, Guid excludeId)
        {
            return await _context.Doctors.AnyAsync(x =>
            x.LicenseNumber == licenseNumber &&
            x.Id != excludeId &&
            !x.IsDeleted);
        }

        public async Task<bool> IsLicenseNumberExisis(string licenseNumber)
        {
            return await _context.Doctors
            .AnyAsync(x => x.LicenseNumber == licenseNumber && !x.IsDeleted);
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}