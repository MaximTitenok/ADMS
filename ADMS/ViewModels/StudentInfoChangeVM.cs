using ADMS.Models;
using ADMS.Services;
using ADMS.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Group = ADMS.Models.Group;


namespace ADMS.ViewModels
{
    internal class StudentInfoChangeVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        //TODO: Add requirements and checks for fields to accept the changes in thw BD
        public Student Student { get; set; }
        public List<Group> Groups { get; set; }
        public List<Speciality> Specialities { get; set; }
        public string[] SpecialitiesArray { get; set; }
        public string[] GroupsArray { get; set; }
        public string[] StudyFormArray { get; set; } = StructureStore.GetStudyForms();
        public string[] StudyLevelArray { get; set; } = StructureStore.GetStudyLevels();
        public string Group {  get; set; }
        public string Speciality { get; set; }
        public string StudyForm { get; set; }
        public string StudyLevel { get; set; }
        public string StudentGender { get; set; }
        public ICommand SaveStudentInfoButtonCommand { get; set; }

        public StudentInfoChangeVM(Student student)
        {
            Student = new Student(student);
            using (AppDBContext _dbContext = new AppDBContext())
            { 
                Groups = _dbContext.Groups.Where(x => x.Faculty.Id == Student.Faculty.Id).AsNoTracking().ToList();
                Specialities = _dbContext.Specialities.Where(x => x.Faculty.Id == Student.Faculty.Id).AsNoTracking().ToList();

            }
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty.Id == Student.Faculty.Id).Select(x => x.ShortName).ToArray();
            GroupsArray = StructureStore.GetGroups().Where(x => x.Faculty.Id == Student.Faculty.Id).Select(x => x.Name).ToArray();
            Group = Student.Group.Name;
            Speciality = Student.Speciality.ShortName;
            StudyForm = StructureStore.GetStudyFormName((int)Student.StudyForm);
            StudyLevel = StructureStore.GetStudyLevelName((int)Student.StudyLevel);
            if (Student.Gender  == false)
            {
                StudentGender = "Male";
            }
            else
            {
                StudentGender = "Female";
            }
            SaveStudentInfoButtonCommand = new RelayCommand(SaveStudentInfo);
        }
        private void SaveStudentInfo(object obj)
        {
            if(StudentGender == "Male")
            {
                Student.Gender = false;
            }
            else
            {
                Student.Gender = true;
            }
            Student.StudyForm = StructureStore.GetStudyFormIndex(StudyForm);
            Student.StudyLevel = StructureStore.GetStudyLevelIndex(StudyLevel);
            Student.Group = Groups.Where(x =>  x.Name == Group).FirstOrDefault();
            Student.Speciality = Specialities.Where(x => x.Name == Speciality).FirstOrDefault();
            using (AppDBContext _dbContext = new AppDBContext())
            {
                _dbContext.Students.Update(Student);
                 _dbContext.SaveChanges();
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
