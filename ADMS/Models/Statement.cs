using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Statement
    {
        [Key]
        public int Id { get; set; }
        //первая, вторая, третья ведомость
        public int? StatementNumber { get; set; }
        public Subject SubjectId { get; set; }
        public int? Semester {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public Group? Group { get; set; }
        public Faculty? Faculty { get; set; }
        public Employee? Teacher { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
