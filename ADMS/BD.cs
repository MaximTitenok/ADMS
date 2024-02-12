using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ADMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ADMS
{
    public class AppDBContext : DbContext
    { 

        public AppDBContext()
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();
            var faculty = new Faculty() { Id = 1, Name = "Faculty", ShortName = "Fclt" };
            var position = new Position() { Id = 1, Name = "Position" };
            var depart = new Department() { Id = 1, Name = "Departament", ShortName = "Depart", Faculty = faculty };
            var spec = new Speciality() { Id = 1, Name = "Speciality", ShortName = "Spec", NumberOfSpeciality = 0, Faculty = faculty };
            var group = new Group() { Id = 1, Name = "Group", Faculty = faculty, Department = depart, StartEducation = DateTime.UtcNow, AddedTime = DateTime.UtcNow };
            var employee = new Employee() { Id = 1, Surname = "Surname1", Name = "Name1", Secondname = "Secondname", Birthday = DateTime.UtcNow, Gender = true, Phone = "06773", OfficePhone = "06795", Email = "maxim@gmail.com", OfficeEmail = "maxim2@gmail.com", Tin = 23, PassportId = "456123", Position = position, WorkRate = 0.95f, StaffingId = 1, StartWork = DateTime.UtcNow, PannedFinishWork = DateTime.UtcNow, FinishedWork = DateTime.UtcNow, Note = "test", СorrectiveEmployee = null };
            var statement = new Statement() { Id = 1, Faculty = faculty, Group = group, StatementNumber = 1, StartDate=DateTime.UtcNow,EndDate=DateTime.UtcNow, ClosedDate=DateTime.UtcNow, Teacher = employee, AddedTime = DateTime.UtcNow };
            var student = new Student() { Id = 1, Surname = "Surname", Name = "Name", Secondname = "Secondname", Birthday = DateTime.UtcNow, Gender = true, Phone = "06773", Email = "maxim@gmail.com", OfficeEmail = "maxim2@gmail.com", Tin = 456123, PassportId = "AE456231", StudyLevel = 1, StudyForm = 1, Speciality = spec, Faculty = faculty, Group = group, StudentId = 0 };
            if (Faculties?.Any() == false)
            {
                Faculties.Add(faculty);
            }
            if (Departments?.Any() == false)
            {
                Departments.Add(depart);
            }
            if (Specialities?.Any() == false)
            {
                Specialities.Add(spec);
            }
            if (Positions?.Any() == false)
            {
                Positions.Add(position);
            }
            if (Employees?.Any() == false)
            {
                Employees.Add(employee);
            }
            if (Groups?.Any() == false)
            {
                Groups.Add(group);
            }
            if (Students?.Any() == false)
            {
                Students.Add(student);
            }
            if (Subjects?.Any() == false)
            {
                Subjects.Add(new Subject() { Id = 1, Name = "Subject", ShortName = "Subj" });
            }
            if (Statements?.Any() == false)
            {
                Statements.Add(statement);
            }
            if (StatementMarks?.Any() == false)
            {
                StatementMarks.Add(new StatementMark() { Id = 0, Mark = 85, Statement = statement, Student = student, AddedTime = DateTime.UtcNow });
            }
            if (Orders?.Any() == false)
            {
                Orders.Add(new Order()
                {
                    Id = 0,
                    Number = "number01",
                    Name = "Name",
                    Type = 1,
                    Description = "Description",
                    Status = 1,
                    Groups = new int[] { group.Id },
                    Students = new int[] { student.Id },
                    File = new byte[] { 0x20, 0x20, 0x20 },
                    AddedEmplyoee = employee,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow,
                    AddedTime = DateTime.UtcNow
                });
            }
            SaveChanges();*/
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.UseNpgsql("Host=localhost;Port=8000;Database=ADMS;Username=API_ADMS;Password=parol9823");/*parol9823*/
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }
        internal DbSet<Department> Departments { get; set; }
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<Faculty> Faculties { get; set; }
        internal DbSet<Group> Groups { get; set; }
        internal DbSet<Position> Positions { get; set; }
        internal DbSet<Speciality> Specialities { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<Subject> Subjects { get; set; }
        internal DbSet<Statement> Statements { get; set; }
        internal DbSet<StatementMark> StatementMarks { get; set; }
        internal DbSet<Order> Orders{ get; set; }

    }
}
