using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Faculty
    {
        [Key]
        public ushort Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
