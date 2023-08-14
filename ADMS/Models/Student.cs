using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Models
{
    internal class Student
    {
        private DateTime _birthday;
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public int Inn { get; set; }
        public string PassportId { get; set; }
        public ushort StudyLevel { get; set; }
        public ushort StudyForm { get; set; }
        public Speciality Speciality { get; set;}
        public Faculty Faculty { get; set; }
        public Group Group { get; set; }
        /// <summary>
        /// номер студентського квитка
        /// </summary>
        public uint StudentId { get; set; }



    }
}
