using DLM.Application.Features.Doctors.DTOs;
using DLM.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DLM.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        //for sp sets
        public DbSet<DoctorSPDto> DoctorSPResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          

          
            modelBuilder.Entity<DoctorSPDto>(entity =>
            {
                entity.HasNoKey();        
                   

                entity.Property(x => x.Id);
                entity.Property(x => x.FullName);
                entity.Property(x => x.Email);
                entity.Property(x => x.Specialization);
                entity.Property(x => x.LicenseNumber);
                entity.Property(x => x.LicenseExpiryDate);
                entity.Property(x => x.Status);
                entity.Property(x => x.CreatedDate);
            });
        }

    }
}
