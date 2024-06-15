using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADMS.Models
{
    internal class EmployeeRate
    {
        public int Id { get; set; } 
        public Employee? Employee { get; set; }
        public Department? Department { get; set; }
        public float? Rate { get; set; }
        /// <value>Штатність працівника</value>
        public short? StaffingId { get; set; }
        public Position? Position { get; set; }
        public DateTime? StartWork { get; set; }
        /// <value>Якщо робота по контракту з плановим закінченням роботи</value>
        public DateTime? PlannedFinishWork { get; set; }
        public DateTime? FinishedWork { get; set; }
        public DateTime? AddedTime { get; set; }

        internal EmployeeRate() { }

        internal EmployeeRate(EmployeeRate rate)
        {
            Id = rate.Id;
            Employee = rate.Employee;
            Department = rate.Department;
            Rate = rate.Rate;
            StaffingId = rate.StaffingId;
            Position = rate.Position;
            StartWork = rate.StartWork;
            PlannedFinishWork = rate.PlannedFinishWork;
            FinishedWork = rate.FinishedWork;
            AddedTime = rate.AddedTime;
        }
    }
}
