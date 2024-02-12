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
        [Key]
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Secondname { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? OfficeEmail { get; set; }
        public int? Tin { get; set; }
        public string? PassportId { get; set; }
        public ushort? StudyLevel { get; set; }
        public ushort? StudyForm { get; set; }
        public Speciality? Speciality { get; set;}
        public Faculty? Faculty { get; set; }
        public Group? Group { get; set; }
        /// <summary>
        /// номер студентського квитка
        /// </summary>
        public uint? StudentId { get; set; }
        internal Student() { }

        internal Student(Student student) 
        { 
            Id = student.Id;
            Surname = student.Surname;
            Name = student.Name;
            Secondname = student.Secondname;
            Birthday = student.Birthday;
            Gender = student.Gender;
            Phone = student.Phone;
            Email = student.Email;
            OfficeEmail = student.OfficeEmail;
            Tin = student.Tin;
            PassportId = student.PassportId;
            StudyLevel = student.StudyLevel;
            StudyForm = student.StudyForm;
            Speciality = student.Speciality;
            Faculty = student.Faculty;
            Group = student.Group;
            StudentId = student.StudentId;
        }



    }
}
