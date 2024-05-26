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
        public int? Tin { get; set; }
        public string? PassportId { get; set; }
        public Department? Department { get; set; }
        public Position? Position { get; set; }
        /// <value>Ставка працівника</value>
        public float? WorkRate { get; set; }
        /// <value>Штатність працівника</value>
        public short? StaffingId { get; set; }
        public DateTime? StartWork { get; set; }
        /// <value>Якщо робота по контракту з плановим закінченням роботи</value>
        public DateTime? PannedFinishWork { get; set; }
        public DateTime? FinishedWork { get; set; }
        public string? Note { get; set; }
        /// <value>Той, хто корегував дані працівника</value>
        public Employee? СorrectiveEmployee { get; set; }
    }
}
