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

        internal EmployeeRate(Employee employee, Department department, float rate, short staffingId, Position position,
            DateTime startWork, DateTime plannedFinishWork, DateTime finishedWork, DateTime addedTime)
        {
            Employee = employee;
            Department = department;
            Rate = rate;
            StaffingId = staffingId;
            Position = position;
            StartWork = startWork;
            PlannedFinishWork = plannedFinishWork;
            FinishedWork = finishedWork;
            AddedTime = addedTime;
        }
    }
}
