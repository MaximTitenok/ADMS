using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class DocFile
    {
        [Key]
        public ushort Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? File { get; set; }
        public Employee? AddedEmployee { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
