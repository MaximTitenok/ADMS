using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class StatementMark
    {
        [Key]
        public int Id { get; set; }
        public Statement? Statement { get; set; }
        public Student? Student { get; set; }
        public int? Mark { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
