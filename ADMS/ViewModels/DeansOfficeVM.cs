using ADMS.Models;
using ADMS.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ADMS.ViewModels
{
    internal class DeansOfficeVM : INotifyPropertyChanged
    {
        private readonly AppDBContext _dbContext = new AppDBContext();
        public List<Group> Groups { get; set; }
        public List<Speciality> Specialities { get; set; }
        public List<Student> Students { get; set; }

        public string FindConditionSurname { get; set; }
        public string FindConditionName { get; set; }
        public string FindConditionSecondname { get; set; }
        public string FindConditionPassport { get; set; }
        public string FindConditionGroup { get; set; }
        public string FindConditionSpeciality { get; set; }
        public string FindConditionGender { get; set; }
        public string FindConditionStudentId { get; set; }
        public string StudyLevel { get; set; }
        public string StudyForm { get; set; }

        public string[] StudyLevels { get; set; }
        public string[] StudyForms { get; set; }

        public ICommand FindButtonCommand { get; set; }
        public ICommand ClearButtonCommand { get; set; }

        public DeansOfficeVM(int facultyId) 
        {
            Groups = _dbContext.Groups.Where(x => x.Faculty.Id == facultyId).ToList();
            Groups.Insert(0,new Group { Name = "" });
            Specialities = _dbContext.Specialities.Where(x => x.Faculty.Id == facultyId).ToList();
            Specialities.Insert(0,new Speciality { ShortName = "" });

            StudyLevels = GetStudyFormAndLevelNames.StudLevels;
            StudyForms = GetStudyFormAndLevelNames.StudForms;

            FindButtonCommand = new RelayCommand(FindStudentsByConditions);
            ClearButtonCommand = new RelayCommand(ClearConditionFields);

        }

        private void FindStudentsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(FindConditionSurname) && String.IsNullOrEmpty(FindConditionName) 
                && String.IsNullOrEmpty(FindConditionSecondname) && String.IsNullOrEmpty(FindConditionGroup) 
                && String.IsNullOrEmpty(FindConditionSpeciality) && String.IsNullOrEmpty(FindConditionGender)
                && String.IsNullOrEmpty(FindConditionStudentId) && String.IsNullOrEmpty(FindConditionPassport)
                && String.IsNullOrEmpty(StudyLevel) && String.IsNullOrEmpty(StudyForm))
            {
                MessageBox.Show("All the fields is empty!","Error");
                return;
            }

            var query = _dbContext.Students.AsQueryable();

            if (!string.IsNullOrEmpty(FindConditionSurname))
            {
                query = query.Where(student => student.Surname.Contains(FindConditionSurname));
            }

            if (!string.IsNullOrEmpty(FindConditionName))
            {
                query = query.Where(student => student.Name.Contains(FindConditionName));
            }

            if (!string.IsNullOrEmpty(FindConditionSecondname))
            {
                query = query.Where(student => student.Secondname.Contains(FindConditionSecondname));
            }
            if (!string.IsNullOrEmpty(FindConditionPassport))
            {
                query = query.Where(student => student.PassportId.Contains(FindConditionPassport));
            }
            if (!string.IsNullOrEmpty(FindConditionGroup))
            {
                query = query.Where(student => student.Group.Name.Contains(FindConditionGroup));
            }
            if (!string.IsNullOrEmpty(FindConditionSpeciality))
            {
                query = query.Where(student => student.Speciality.Name.Contains(FindConditionSpeciality));
            }
            if (!string.IsNullOrEmpty(FindConditionGender))
            {
                bool gender = false;
                if(FindConditionGender == "Male")
                {
                    gender = false;
                }
                else if(FindConditionGender == "Female")
                {
                    gender = true;
                }
                query = query.Where(student => student.Gender == gender);
            }

            if (!string.IsNullOrEmpty(FindConditionStudentId))
            {
                if(FindConditionStudentId.All(char.IsDigit))
                {
                    query = query.Where(student => student.StudentId.ToString() == FindConditionStudentId);
                }
                else
                {
                    MessageBox.Show("Student ID contains letter!", "Error");
                } 
            }
            if (!string.IsNullOrEmpty(FindConditionSpeciality))
            {
                query = query.Where(student => student.Speciality.Name.Contains(FindConditionSpeciality));
            }
            if (!string.IsNullOrEmpty(StudyLevel))
            {
                query = query.Where(student => student.StudyLevel == Array.IndexOf(StudyLevels, StudyLevel));
            }
            if (!string.IsNullOrEmpty(StudyForm))
            {
                query = query.Where(student => student.StudyForm == Array.IndexOf(StudyForms, StudyForm));
            }

            Students = query
                .Include(x => x.Faculty)
                .Include(x => x.Group)
                .Include(x => x.Speciality)
                .ToList();
            OnPropertyChanged("Students");
        }

        private void ClearConditionFields(object obj)
        {
            FindConditionSurname = "";
            OnPropertyChanged("FindConditionSurname");
            FindConditionName = "";
            OnPropertyChanged("FindConditionName");
            FindConditionSecondname = "";
            OnPropertyChanged("FindConditionSecondname");
            FindConditionGroup = "";
            OnPropertyChanged("FindConditionGroup");
            FindConditionSpeciality = "";
            OnPropertyChanged("FindConditionSpeciality");
            FindConditionGender = "";
            OnPropertyChanged("FindConditionGender");
            FindConditionStudentId = "";
            OnPropertyChanged("FindConditionStudentId");
        }







        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
