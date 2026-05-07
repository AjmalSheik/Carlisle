using DLM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLM.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Specialization { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime LicenseExpiryDate { get; set; }

        public DoctorStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
