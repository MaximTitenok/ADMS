using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Group
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public Faculty? Faculty { get; set; }
        public Department? Department { get; set; }
        public DateTime? StartEducation { get; set; }
        public DateTime? AddedTime { get; set; }

        internal Group() { }

        internal Group(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            Faculty = group.Faculty;
            Department = group.Department;  
            StartEducation = group.StartEducation;
            AddedTime = group.AddedTime;
            
        }
    }
}
