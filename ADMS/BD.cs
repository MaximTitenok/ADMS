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
              if (Faculties?.Any() == false)
              {
                  var faculty = new Faculty() { Name = "Faculty", ShortName = "Fclt" };
                  Faculties.Add(faculty);
                  SaveChanges();
              }
              if (Departments?.Any() == false)
              {
                  var depart = new Department() { Name = "Departament", ShortName = "Depart", Faculty = Faculties.FirstOrDefault() };
                  Departments.Add(depart);
                  SaveChanges();
              }
              if (Specialities?.Any() == false)
              {
                  var spec = new Speciality() { Name = "Speciality", ShortName = "Spec", NumberOfSpeciality = 0, Faculty = Faculties.FirstOrDefault() };
                  Specialities.Add(spec);
                  SaveChanges();
              }
              if (Positions?.Any() == false)
              {
                  var position = new Position() { Name = "Position" };
                  Positions.Add(position);
                  SaveChanges();
              }
              if (Employees?.Any() == false)
              {
                  var employee = new Employee() { Id = 1, Surname = "Surname1", Name = "Name1", Secondname = "Secondname", Birthday = DateTime.UtcNow, Gender = true, Phone = "06773", OfficePhone = "06795", Email = "maxim@gmail.com", OfficeEmail = "maxim2@gmail.com", Tin = 23, PassportId = "456123", Position = Positions.FirstOrDefault(), WorkRate = 0.95f, StaffingId = 1, StartWork = DateTime.UtcNow, PannedFinishWork = DateTime.UtcNow, FinishedWork = DateTime.UtcNow, Note = "test", СorrectiveEmployee = null };
                  Employees.Add(employee);
                  SaveChanges();
              }
              if (Groups?.Any() == false)
              {
                  var group = new Group() { Name = "Group", Faculty = Faculties.FirstOrDefault(), Department = Departments.FirstOrDefault(), StartEducation = DateTime.UtcNow, AddedTime = DateTime.UtcNow };
                  Groups.Add(group);
                  SaveChanges();
              }
              if (Students?.Any() == false)
              {
                  var student = new Student() { Surname = "Surname", Name = "Name", Secondname = "Secondname", Birthday = DateTime.UtcNow, Gender = true, Phone = "06773", Email = "maxim@gmail.com", OfficeEmail = "maxim2@gmail.com", Tin = 456123, PassportId = "AE456231", StudyLevel = 1, StudyForm = 1, Speciality = Specialities.FirstOrDefault(), Faculty = Faculties.FirstOrDefault(), Group = Groups.FirstOrDefault(), StudentId = 0 };
                  Students.Add(student);
                  SaveChanges();
              }
              if (SubjectsBank?.Any() == false)
              {
                  SubjectsBank.Add(new SubjectBank() { Name = "Subject", ShortName = "Subj" });
                  SaveChanges();
              }
            if (Subjects?.Any() == false)
            {
                Subjects.Add(new Subject() { SubjectBankId = SubjectsBank.FirstOrDefault(), Semester = 3, ECTS = 4, AllHours = 150, LectureHours = 120, PracticeHours = 10, SeminarHours = 5, LabourHours = 7, ConsultationHours = 8,Exam = true, Credit = false, CourseProject = true, ComputationalGraphicWork = false, Diploma = false, Department = Departments.FirstOrDefault(), Note = "Po zapisu"});
                SaveChanges();
            }
            if (Statements?.Any() == false)
              {
                  var statement = new Statement() { Faculty = Faculties.FirstOrDefault(),Group = Groups.FirstOrDefault(), Semester = 3, SubjectId = Subjects.FirstOrDefault(), StatementNumber = 1, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow, ClosedDate = DateTime.UtcNow, Teacher = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow };
                  Statements.Add(statement);
                  SaveChanges();
              }
              if (StatementMarks?.Any() == false)
              {
                  StatementMarks.Add(new StatementMark() { Mark = 85, Statement = Statements.FirstOrDefault(), Student = Students.FirstOrDefault(), AddedTime = DateTime.UtcNow });
                  SaveChanges();
              }
              if (DocFiles?.Any() == false)
              {
                  var doc1 = new DocFile() { Name = "DocName1", Description = "Descr1", File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow };
                  var doc2 = new DocFile() { Name = "DocName2", Description = "Descr2", File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow };
                  DocFiles.Add(doc1);
                  DocFiles.Add(doc2);
                  SaveChanges();
              }

            if (Orders?.Any() == false)
            {
                var files = DocFiles.Take(2).Select(x => x.Id).ToArray();
                var groups = new int[] { Groups.FirstOrDefault().Id };
                var students = new int[] { Students.FirstOrDefault().Id };
                var employeee = Employees.FirstOrDefault();
                var oreder =
                    new Order()
                    {
                        Id = 0,
                        Number = "number01",
                        Name = "Name",
                        Type = 1,
                        Description = "Description",
                        Status = 1,
                        Groups = groups,
                        Students = students,
                        File = files,
                        AddedEmplyoee = employeee,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        AddedTime = DateTime.UtcNow
                    };

                Orders.Add(oreder);
                SaveChanges();
            }*/

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.UseNpgsql("Host=localhost;Port=8000;Database=ADMS;Username=API_ADMS;Password=parol9823");/*parol9823*/
            optionsBuilder.EnableSensitiveDataLogging();
        }
        internal DbSet<Department> Departments { get; set; }
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<Faculty> Faculties { get; set; }
        internal DbSet<Group> Groups { get; set; }
        internal DbSet<Position> Positions { get; set; }
        internal DbSet<Speciality> Specialities { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<SubjectBank> SubjectsBank { get; set; }
        internal DbSet<Subject> Subjects { get; set; }
        internal DbSet<Statement> Statements { get; set; }
        internal DbSet<StatementMark> StatementMarks { get; set; }
        internal DbSet<Order> Orders{ get; set; }
        internal DbSet<DocFile> DocFiles { get; set; }

    }
}
