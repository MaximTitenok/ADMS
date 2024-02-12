using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Speciality
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public int? NumberOfSpeciality { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
