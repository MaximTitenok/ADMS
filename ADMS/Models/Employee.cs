using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Secondname { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? OfficePhone { get; set; }
        public string? Email { get; set; }
        public string? OfficeEmail { get; set; }
        public uint? Tin { get; set; }
        public string? PassportId { get; set; }
        public string? Note { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
