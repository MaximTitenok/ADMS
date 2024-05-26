using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Subject
    {
        [Key]
        public ushort Id { get; set; }
        public SubjectBank? SubjectBankId { get; set; }
        public int? Semester {  get; set; }
        public int? ECTS {  get; set; }
        public int? AllHours { get; set; }
        public int? LectureHours { get; set;}
        public int? PracticeHours { get; set; }
        public int? SeminarHours { get; set; }
        public int? LabourHours { get; set; }
        public int? ConsultationHours { get; set; }
        public bool? Exam { get; set; }
        //zachet
        public bool? Credit { get; set;}
        //cursach
        public bool? CourseProject { get; set;}
        //RGR
        public bool? ComputationalGraphicWork { get; set; }
        public bool Diploma {  get; set; }
        public Department Department { get; set; }
        public string Note { get; set; }
    }
}
