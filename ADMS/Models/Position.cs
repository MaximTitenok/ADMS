using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    /// <summary>
    /// Посада
    /// </summary>
    internal class Position
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? AddedTime { get; set; }
    }
}
