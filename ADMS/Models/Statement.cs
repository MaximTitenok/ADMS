using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADMS.Models
{
    internal class Statement
    {
        [Key]
        public int Id { get; set; }
        //первая, вторая, третья ведомость
        public int? StatementNumber { get; set; }
        public Subject Subject { get; set; }
        public int? Semester {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <value> true - open, false - closed </value>>
        public bool Status { get; set; }
        public DateTime ClosedDate { get; set; }
        public Group? Group { get; set; }
        public Faculty? Faculty { get; set; }
        public Employee? MainTeacher { get; set; }
        public Employee? PracticeTeacher { get; set; }
        public DateTime? AddedTime { get; set; }

        internal Statement() { }

        internal Statement(Statement statement)
        {
            Id = statement.Id;
            StatementNumber = statement.StatementNumber;
            Subject = statement.Subject;
            Semester = statement.Semester;
            StartDate = statement.StartDate;
            EndDate = statement.EndDate;
            ClosedDate = statement.ClosedDate;  
            Group = statement.Group;
            Faculty = statement.Faculty;
            MainTeacher = statement.MainTeacher;
            PracticeTeacher = statement.PracticeTeacher;
            AddedTime = statement.AddedTime;
            

        }
    }
}
