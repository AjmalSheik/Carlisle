using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Application.Features.Doctors.DTOs
{
    public class DoctorDTO
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Specialization { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime LicenseExpiryDate { get; set; }

        public string Status { get; set; }
    }
}
