using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ADMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ADMS
{
    public class AppDBContext : DbContext
    {
        //TODO: add the properties for passwords and logins at employees and students and active/deactive
        //TODO: добавить кнопу деактивации и удаления для админа
        //TODO: добавить человека на все записи которые добавляют который добавляет
        /*public AppDBContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            if (Faculties?.Any() == false)
            {
                var faculty = new Faculty() { Name = "Факультет інформаційних технологій", ShortName = "ФІТ", IsActive = true, AddedTime = DateTime.UtcNow.AddDays(-5) };
                var faculty2 = new Faculty() { Name = "Електротехнічний факультет", ShortName = "ЕТФ", IsActive = true, AddedTime = DateTime.UtcNow.AddDays(-6) };
                Faculties.Add(faculty);
                Faculties.Add(faculty2);
                SaveChanges();
            }
            if (Departments?.Any() == false)
            {
                var depart = new Department() { Name = "Кафедра автоматизації, комп`ютерних наук і технологій", ShortName = "АКНТ", IsActive = true, Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-4) };
                var depart2 = new Department() { Name = "Кафедра комп`ютерних систем та мереж", ShortName = "КСМ", IsActive = true, Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-3) };
                var depart3 = new Department() { Name = "Кафедра електромеханіки", ShortName = "КЕМ", IsActive = true, Faculty = Faculties.Where(x => x.ShortName == "ЕТФ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-4) };
                Departments.Add(depart);
                Departments.Add(depart2);
                Departments.Add(depart3);
                SaveChanges();
            }
            if (Specialities?.Any() == false)
            {
                var spec = new Speciality() { Name = "Автоматизація та комп`ютерно-інтегровані технології", ShortName = "АКІТ", NumberOfSpeciality = 151, Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                var spec2 = new Speciality() { Name = "Комп`ютерні науки", ShortName = "КН", NumberOfSpeciality = 122, Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2), IsActive = true };
                var spec3 = new Speciality() { Name = "Теплоенергетика", ShortName = "ТЕП", NumberOfSpeciality = 144, Faculty = Faculties.Where(x => x.ShortName == "ЕТФ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2), IsActive = true };
                Specialities.Add(spec);
                Specialities.Add(spec2);
                Specialities.Add(spec3);
                SaveChanges();
            }
            if (Positions?.Any() == false)
            {
                var position = new Position() { Name = "ст. викл.", AddedTime = DateTime.UtcNow.AddDays(-2), IsActive = true };
                var position2 = new Position() { Name = "асист.", AddedTime = DateTime.UtcNow.AddDays(-2), IsActive = true };
                var position3 = new Position() { Name = "доц.", AddedTime = DateTime.UtcNow.AddDays(-2), IsActive = true };
                Positions.Add(position);
                Positions.Add(position2);
                Positions.Add(position3);
                SaveChanges();
            }
            if (Employees?.Any() == false)
            {
                var employee = new Employee() { Surname = "Каденюк", Name = "Леонід", Secondname = "Костянтинович", Birthday = DateTime.UtcNow.AddYears(-43), Gender = false, Phone = "0955803120", OfficePhone = "144", Email = "kadenchik@gmail.com", OfficeEmail = "kadenuk@gmail.com", Tin = 3194101162, PassportId = "002115697", Note = "", AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                var employee2 = new Employee() { Surname = "Костенко", Name = "Ліна", Secondname = "Василівна", Birthday = DateTime.UtcNow.AddYears(-94).AddMonths(-4), Gender = true, Phone = "0945853121", OfficePhone = "256", Email = "l.kostenko@gmail.com", OfficeEmail = "kostenko@gmail.com", Tin = 3093101562, PassportId = "AE154624", Note = "Стажування", AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                Employees.Add(employee);
                Employees.Add(employee2);
                SaveChanges();
            }
            if (EmployeeRates?.Any() == false)
            {
                var employeeRate = new EmployeeRate() { Employee = Employees.Where(x => x.Surname == "Каденюк").FirstOrDefault(), Position = Positions.Where(x => x.Name == "ст. викл.").FirstOrDefault(), Department = Departments.Where(x => x.ShortName == "АКНТ").FirstOrDefault(), Rate = 0.8f, StaffingId = 0, StartWork = DateTime.UtcNow.AddMonths(-15), AddedTime = DateTime.UtcNow.AddMonths(-15) };
                var employeeRate2 = new EmployeeRate() { Employee = Employees.Where(x => x.Surname == "Каденюк").FirstOrDefault(), Position = Positions.Where(x => x.Name == "доц.").FirstOrDefault(), Department = Departments.Where(x => x.ShortName == "АКНТ").FirstOrDefault(), Rate = 0.2f, StaffingId = 2, StartWork = DateTime.UtcNow.AddMonths(-4), PlannedFinishWork = DateTime.UtcNow.AddMonths(3), AddedTime = DateTime.UtcNow.AddMonths(-4) };
                var employeeRate3 = new EmployeeRate() { Employee = Employees.Where(x => x.Surname == "Костенко").FirstOrDefault(), Position = Positions.Where(x => x.Name == "асист.").FirstOrDefault(), Department = Departments.Where(x => x.ShortName == "КСМ").FirstOrDefault(), Rate = 1f, StaffingId = 1, StartWork = DateTime.UtcNow.AddMonths(-9), AddedTime = DateTime.UtcNow.AddMonths(-9) };
                EmployeeRates.Add(employeeRate);
                EmployeeRates.Add(employeeRate2);
                EmployeeRates.Add(employeeRate3);
                SaveChanges();
            }
            if (Groups?.Any() == false)
            {
                var group = new Group() { Name = "АКІТ-20", Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), Department = Departments.Where(x => x.ShortName == "АКНТ").FirstOrDefault(), Speciality = Specialities.Where(x => x.Faculty.ShortName == "ФІТ").FirstOrDefault(), IsActive = true, StartEducation = DateTime.UtcNow.AddMonths(-2), AddedTime = DateTime.UtcNow.AddMonths(-3) };
                var group2 = new Group() { Name = "ЕЕМ-21-1", Faculty = Faculties.Where(x => x.ShortName == "ЕТФ").FirstOrDefault(), Department = Departments.Where(x => x.ShortName == "КСМ").FirstOrDefault(), Speciality = Specialities.Where(x => x.Faculty.ShortName == "ЕТФ").FirstOrDefault(), IsActive = true, StartEducation = DateTime.UtcNow.AddMonths(-2), AddedTime = DateTime.UtcNow.AddMonths(-3) };
                Groups.Add(group);
                Groups.Add(group2);
                SaveChanges();
            }
            if (Students?.Any() == false)
            {
                var student = new Student() { Surname = "Баранов", Name = "Олександр", Secondname = "Юрійович", Birthday = DateTime.UtcNow.AddYears(-23), Gender = false, Phone = "0955463120", Email = "oleks@gmail.com", OfficeEmail = "", Tin = 3191201162, PassportId = "002114697", StudyLevel = 0, StudyForm = 0, Speciality = Specialities.Where(x => x.ShortName == "АКІТ").FirstOrDefault(), Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), Group = Groups.Where(x => x.Name == "АКІТ-20").FirstOrDefault(), StudentId = 0, AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                var student2 = new Student() { Surname = "Вермянко", Name = "Василь", Secondname = "Петрович", Birthday = DateTime.UtcNow.AddYears(-22), Gender = false, Phone = "0955879120", Email = "vasil@gmail.com", OfficeEmail = "veriamko.vasil@gmail.com", Tin = 3194151262, PassportId = "AE411231", StudyLevel = 1, StudyForm = 0, Speciality = Specialities.Where(x => x.ShortName == "АКІТ").FirstOrDefault(), Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), Group = Groups.Where(x => x.Name == "АКІТ-20").FirstOrDefault(), StudentId = 1, AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                var student3 = new Student() { Surname = "Борисенко", Name = "Андрій", Secondname = "Анатолійович", Birthday = DateTime.UtcNow.AddYears(-21), Gender = false, Phone = "0955103120", Email = "andrii@gmail.com", OfficeEmail = "", Tin = 3194103542, PassportId = "AE455231", StudyLevel = 0, StudyForm = 0, Speciality = Specialities.Where(x => x.ShortName == "ТЕП").FirstOrDefault(), Faculty = Faculties.Where(x => x.ShortName == "ЕТФ").FirstOrDefault(), Group = Groups.Where(x => x.Name == "ЕЕМ-21-1").FirstOrDefault(), StudentId = 3, AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true };
                Students.Add(student);
                Students.Add(student2);
                Students.Add(student3);
                SaveChanges();
            }
            if (SubjectsBank?.Any() == false)
            {
                SubjectsBank.Add(new SubjectBank() { Name = "Архітектура комп`ютерних систем", ShortName = "АКС", AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true });
                SubjectsBank.Add(new SubjectBank() { Name = "Системи теплових енергоносіїв", ShortName = "СТЕ", AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true });
                SaveChanges();
            }
            if (Subjects?.Any() == false)
            {
                Subjects.Add(new Subject() { SubjectBank = SubjectsBank.Where(x => x.ShortName == "АКС").FirstOrDefault(), Semester = 1, ECTS = 5, AllHours = 150, LectureHours = 120, PracticeHours = 10, SeminarHours = 5, LabourHours = 7, ConsultationHours = 8, Exam = true, Credit = false, CourseProject = true, ComputationalGraphicWork = false, Diploma = false, Department = Departments.Where(x => x.ShortName == "АКНТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true });
                Subjects.Add(new Subject() { SubjectBank = SubjectsBank.Where(x => x.ShortName == "СТЕ").FirstOrDefault(), Semester = 2, ECTS = 3, AllHours = 120, LectureHours = 90, PracticeHours = 10, SeminarHours = 15, LabourHours = 5, ConsultationHours = 0, Exam = true, Credit = true, CourseProject = true, ComputationalGraphicWork = false, Diploma = false, Department = Departments.Where(x => x.ShortName == "КСМ").FirstOrDefault(), Note = "По запису", AddedTime = DateTime.UtcNow.AddDays(-3), IsActive = true });
                SaveChanges();
            }
            if (Statements?.Any() == false)
            {
                var statement = new Statement() { Faculty = Faculties.Where(x => x.ShortName == "ФІТ").FirstOrDefault(), Group = Groups.Where(x => x.Name == "АКІТ-20").FirstOrDefault(), Semester = 3, Subject = Subjects.FirstOrDefault(), StatementNumber = 1, StartDate = DateTime.UtcNow.AddDays(-15), EndDate = DateTime.UtcNow.AddDays(-5), ClosedDate = DateTime.UtcNow.AddDays(-6), Status = false, MainTeacher = EmployeeRates.Where(x => x.Department.ShortName == "АКНТ").Select(x => x.Employee).FirstOrDefault(), PracticeTeacher = EmployeeRates.Where(x => x.Department.ShortName == "АКНТ").Select(x => x.Employee).FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-20) };
                var statement2 = new Statement() { Faculty = Faculties.Where(x => x.ShortName == "ЕТФ").FirstOrDefault(), Group = Groups.Where(x => x.Name == "ЕЕМ-21-1").FirstOrDefault(), Semester = 3, Subject = Subjects.FirstOrDefault(), StatementNumber = 2, StartDate = DateTime.UtcNow.AddDays(-5), EndDate = DateTime.UtcNow.AddDays(1), ClosedDate = DateTime.UtcNow.AddDays(-1), Status = true, MainTeacher = EmployeeRates.Where(x => x.Department.ShortName == "КЕМ").Select(x => x.Employee).FirstOrDefault(), PracticeTeacher = EmployeeRates.Where(x => x.Department.ShortName == "КЕМ").Select(x => x.Employee).FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-15) };
                Statements.Add(statement);
                Statements.Add(statement2);
                SaveChanges();
            }
            if (StatementMarks?.Any() == false)
            {
                StatementMarks.Add(new StatementMark() { Mark = 85, Statement = Statements.Where(x => x.Faculty.ShortName == "ФІТ").FirstOrDefault(), Student = Students.Where(x => x.Faculty.ShortName == "ФІТ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2) });
                StatementMarks.Add(new StatementMark() { Mark = 75, Statement = Statements.Where(x => x.Faculty.ShortName == "ЕТФ").FirstOrDefault(), Student = Students.Where(x => x.Faculty.ShortName == "ЕТФ").FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-1) });
                SaveChanges();
            }
            *//*if (DocFiles?.Any() == false)
            {
                var doc1 = new DocFile() { Name = "Скан наказу", Description = "Скан наказу про зарахування студентів", File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2) };
                var doc2 = new DocFile() { Name = "Скан наказу", Description = "Сказ наказу про призначення на посаду викладача", File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2) };
                DocFiles.Add(doc1);
                DocFiles.Add(doc2);
                SaveChanges();
            }*//*
            if (DocFiles?.Any() == false)
            {
                var doc1 = new DocFile() {File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2) };
                var doc2 = new DocFile() { File = new byte[] { 0x20, 0x20, 0x20 }, AddedEmployee = Employees.FirstOrDefault(), AddedTime = DateTime.UtcNow.AddDays(-2) };
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
                        Number = "№3/23",
                        Name = "Наказ",
                        Type = 1,
                        Description = "Наказ про зарахування та призначення",
                        Status = 1,
                        Groups = groups,
                        Students = students,
                        File = files,
                        AddedEmplyoee = employeee,
                        StartDate = DateTime.UtcNow.AddDays(-5),
                        EndDate = DateTime.UtcNow.AddDays(15),
                        AddedTime = DateTime.UtcNow.AddDays(-6)
                    };

                Orders.Add(oreder);
                SaveChanges();
            }

        }*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            optionsBuilder.UseNpgsql("Host=localhost;Port=8000;Database=ADMS;Username=API_ADMS;Password=parol9823");/*parol9823*/
            optionsBuilder.EnableSensitiveDataLogging();
        }
        internal DbSet<Department> Departments { get; set; }
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<EmployeeRate> EmployeeRates { get; set; }
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
