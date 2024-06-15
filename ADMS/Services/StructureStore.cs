using ADMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace ADMS.Services
{
    internal static class StructureStore
    {
        private static List<Speciality> Specialities {  get; set; }
        private static List<Group> Groups { get; set; }
        private static List<Department> Departments { get; set; }
        private static List<Subject> Subjects { get; set; }
        private static List<Employee> Employees { get; set; }
        private static List<EmployeeRate> EmployeeRates { get; set; }
        private static List<Position> Positions { get; set; }
        private static Faculty Faculty { get; set; }
        private static Department Department { get; set; }

        private readonly static string[] StudyForms = {
            "очна",
            "вечірня",
            "заочна",
        };

        private readonly static string[] StudLevels = {
            "бакалавр",
            "спеціаліст",
            "магістр",
            "канд. наук"
        };
        private readonly static string[] OrderTypes = {
            "наказ",
            "службова записка",
            "заява на призначення",
        };
        internal static Faculty GetFaculties()
        {
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Faculty = _dbContext
                    .Faculties
                    .Where(x => x.Id == 1)//FIT
                    .AsNoTracking()
                    .FirstOrDefault();
                        
            }
            return Faculty ?? new Faculty();
        }
        internal static Department GetDepartment()
        {
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Department = _dbContext
                    .Departments
                    .Where(x => x.ShortName == "АКНТ")//АКНТ
                    .AsNoTracking()
                    .FirstOrDefault();

            }
            return Department ?? new Department();
        }
        internal static List<Speciality> GetSpecialities()
        {
            using (AppDBContext _dbContext = new AppDBContext()) 
            { 
                Specialities = _dbContext
                    .Specialities
                    .Include(t => t.Faculty)
                    .AsNoTracking()
                    .ToList();
            }
            return Specialities ?? new List<Speciality>();
        }
        internal static List<Group> GetGroups()
        {
            if (Groups == null || Groups.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {

                    Groups = _dbContext
                    .Groups
                    .AsNoTracking()
                    .Include(t => t.Faculty)
                    .AsNoTracking()
                    .Include(t => t.Department)
                    .ToList();
                }
            }
            return Groups ?? new List<Group>();
        }
        internal static List<Department> GetDepartments()
        {
            if (Departments == null || Departments.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    Departments = _dbContext
                        .Departments
                        .Include(t => t.Faculty)
                        .AsNoTracking()
                        .ToList();
                }
            }
            return Departments ?? new List<Department>();
        }
        internal static List<Subject> GetSubjects()
        {
            if (Subjects == null || Subjects.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    Subjects = _dbContext
                        .Subjects
                        .Include(x => x.Department)
                        .Include(x => x.Department.Faculty)
                        .Include(x => x.SubjectBank)
                        .AsNoTracking()
                        .ToList();
                }
            }
            return Subjects ?? new List<Subject>();
        }

        internal static List<Employee> GetEmployees()
        {
            if (Employees == null || Employees.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    Employees = _dbContext
                        .Employees
                        .GroupJoin(
                            _dbContext.EmployeeRates,
                            employee => employee.Id,
                            employeeRate => employeeRate.Employee.Id,
                            (employee, employeeRate) => new
                            {
                                Employee = employee,
                                EmployeeRates = employeeRate
                            })
                        .AsNoTracking()
                        .ToList()
                        .Select(x =>
                        {
                            return x.Employee;
                        })
                        .ToList();
                }
            }
            return Employees ?? new List<Employee>();
        }
        internal static List<EmployeeRate> GetEmployeeRates()
        {
            if (EmployeeRates == null || EmployeeRates.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    EmployeeRates = _dbContext
                        .EmployeeRates
                        .Include(x => x.Department)
                        .Include(x => x.Department.Faculty)
                        .Include(x => x.Employee)
                        .AsNoTracking()
                        .ToList();
                }
            }
            return EmployeeRates ?? new List<EmployeeRate>();
        }
        internal static List<Position> GetPositions()
        {
            if (Positions == null || Positions.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    Positions = _dbContext
                        .Positions
                        .AsNoTracking()
                        .ToList();
                }
            }
            return Positions ?? new List<Position>();
        }

        internal static string GetStudyFormName(int type)
        {
            switch (type)
            {
                case 0:
                    {
                        return StudyForms[0];
                    }
                case 1:
                    {
                        return StudyForms[1];
                    }
                case 2:
                    {
                        return StudyForms[2];
                    }
                default:
                    {
                        return "Unknown";
                    }
            }
        }
        internal static ushort GetStudyFormIndex(string nameOfForm)
        {
            switch (nameOfForm)
            {
                case "очна":
                    {
                        return 0;
                    }
                case "вечірня":
                    {
                        return 1;
                    }
                case "заочна":
                    {
                        return 2;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        internal static string[] GetOrderTypes()
        {
            return OrderTypes;
        }

        internal static string GetStudyLevelName(int type)
        {
            switch (type)
            {

                case 0:
                    {
                        return StudLevels[0];
                    }
                case 1:
                    {
                        return StudLevels[1];
                    }
                case 2:
                    {
                        return StudLevels[2];
                    }
                case 3:
                    {
                        return StudLevels[3];
                    }
                default:
                    {
                        return "Unknown";
                    }
            }
        }
            
        internal static ushort GetStudyLevelIndex(string nameOfLevel)
        {
            switch (nameOfLevel)
            {

                case "бакалавр":
                    {
                        return 0;
                    }
                case "спеціаліст":
                    {
                        return 1;
                    }
                case "магістр":
                    {
                        return 2;
                    }
                case "канд. наук":
                    {
                        return 3;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        internal static string GetStaffing(short? number)
        {
            switch (number)
            {
                case 0:
                    {
                        return "штатний";
                    }
                case 1:
                    {
                        return "позаштатний";
                    }
                case 2:
                    {
                        return "сумісник";
                    }
            }
            return string.Empty;
        }
        internal static short GetStaffing(string? name)
        {
            switch (name)
            {
                case "штатний":
                    {
                        return 0;
                    }
                case "позаштатний":
                    {
                        return 1;
                    }
                case "сумісник":
                    {
                        return 2;
                    }
            }
            return -1;
        }
        internal static string[] GetStaffing()
        {
            return new string[] { "штатний", "позаштатний", "сумісник" };

        }
        internal static string GetSpecialityName(int type)
        {
            return StructureStore.GetSpecialities().Where(x => x.Id == type).Select(x => x.Name).ToString() ?? "";
        }
        internal static string[] GetStudyLevels()
        {
            return StudLevels;
        }
        internal static string[] GetStudyForms()
        {
            return StudyForms;
        }
    }
}
