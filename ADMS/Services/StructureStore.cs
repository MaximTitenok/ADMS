using ADMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private static Faculty Faculty { get; set; }

        private readonly static string[] StudyForms = {
            "FT OC (Full-time on-campuse)",
            "FT E (Full-time evening)",
            "C E (Correspondence education)",
        };

        private readonly static string[] StudLevels = {
            "Bachelor",
            "Top-up Bachelor",
            "Master",
            "PhD"
        };
        internal static Faculty GetFaculty()
        {
            if (Faculty == null)
            {
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    Faculty = _dbContext
                        .Faculties
                        .Where(x => x.Id == 1)//FIT
                        .AsNoTracking()
                        .FirstOrDefault();
                        
                }
            }
            return Faculty ?? new Faculty();
        }
        internal static List<Speciality> GetSpecialities()
        {
            if (Specialities == null || Specialities.Count == 0)
            {
                using (AppDBContext _dbContext = new AppDBContext()) 
                { 
                    Specialities = _dbContext
                        .Specialities
                        .Include(t => t.Faculty)
                        .AsNoTracking()
                        .ToList();
                }
            }
            return Specialities ?? new List<Speciality>();
        }
        internal static List<Group> GetGroups()
        {
            List<Group> GroupsList = new List<Group>();
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
            GroupsList = Groups ?? new List<Group>();
            return GroupsList;
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
                        .Include(x => x.SubjectBankId)
                        .AsNoTracking()
                        .ToList();
                }
            }
            return Subjects ?? new List<Subject>();
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
                case "FT OC (Full-time on-campuse)":
                    {
                        return 0;
                    }
                case "FT E (Full-time evening)":
                    {
                        return 1;
                    }
                case "C E (Correspondence education)":
                    {
                        return 2;
                    }
                default:
                    {
                        return 0;
                    }
            }
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

                case "Bachelor":
                    {
                        return 0;
                    }
                case "Top-up Bachelor":
                    {
                        return 1;
                    }
                case "Master":
                    {
                        return 2;
                    }
                case "PhD":
                    {
                        return 3;
                    }
                default:
                    {
                        return 0;
                    }
            }
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
