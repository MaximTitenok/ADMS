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
        
        public Student Student { get; set; }
        public List<Group> Groups { get; set; }
        public List<Speciality> Specialities { get; set; }
        public string?[] SpecialitiesArray { get; set; }
        public string?[] GroupsArray { get; set; }
        public string?[] StudyFormArray { get; set; } = StructureStore.GetStudyForms();
        public string?[] StudyLevelArray { get; set; } = StructureStore.GetStudyLevels();
        public string Group {  get; set; }
        public string Speciality { get; set; }
        public string StudyForm { get; set; }
        public string StudyLevel { get; set; }
        public string StudentGender { get; set; }
        bool IsStudentNew { get; set; }
        public string Title { get; set; }
        private bool shouldShowError;
        public Visibility ErrorVisibility
        {
            get { return shouldShowError ? Visibility.Visible : Visibility.Collapsed; }
        }
        public ICommand SaveStudentInfoButtonCommand { get; set; }
        public event EventHandler OnRequestClose;



        public StudentInfoChangeVM(Student student)
        {
            IsStudentNew = false;
            Student = new Student(student);
            Groups = StructureStore.GetGroups().Where(x => x.Faculty.Id == Student.Faculty.Id).ToList();
            Specialities = StructureStore.GetSpecialities().Where(x => x.Faculty.Id == Student.Faculty.Id).ToList();
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty?.Id == Student?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            GroupsArray = StructureStore.GetGroups().Where(x => x.Faculty?.Id == Student?.Faculty?.Id).Select(x => x.Name).ToArray();
            Group = Student.Group.Name;
            Speciality = Student.Speciality.ShortName;
            StudyForm = StructureStore.GetStudyFormName((int)Student.StudyForm);
            StudyLevel = StructureStore.GetStudyLevelName((int)Student.StudyLevel);
            if (Student.Gender  == false)
            {
                StudentGender = "Чоловіча";
            }
            else
            {
                StudentGender = "Жіноча";
            }
            Title = "Зміна інформації про студента";
            shouldShowError = false;
            SaveStudentInfoButtonCommand = new RelayCommand(SaveStudentInfo);
        }
        public StudentInfoChangeVM()
        {

            IsStudentNew = true;
            Student = new Student();
            Student.Faculty = StructureStore.GetFaculties();
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty.Id == Student.Faculty.Id).Select(x => x.ShortName).ToArray();
            GroupsArray = StructureStore.GetGroups().Where(x => x.Faculty.Id == Student.Faculty.Id).Select(x => x.Name).ToArray();
            SaveStudentInfoButtonCommand = new RelayCommand(SaveStudentInfo);
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Groups = _dbContext.Groups.Where(x => x.Faculty.Id == Student.Faculty.Id).AsNoTracking().ToList();
                Specialities = _dbContext.Specialities.Where(x => x.Faculty.Id == Student.Faculty.Id).AsNoTracking().ToList();

            }
            shouldShowError = false;
            Title = "Додати студента";
        }
        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        private void SaveStudentInfo(object obj)
        {
            if(StudentGender == "Чоловіча")
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
            Student.Speciality = Specialities.Where(x => x.ShortName == Speciality).FirstOrDefault();
            Student.Birthday = DateTime.SpecifyKind((DateTime)Student.Birthday, DateTimeKind.Utc);
            if (Student.Surname == string.Empty || Student.Name == string.Empty || Student.Tin == null || Student.Birthday == null ||
                Student.Gender == null || Student.PassportId == string.Empty || Student?.Speciality?.Id == null ||
                Student.StudyLevel.HasValue != true || Student.StudyForm.HasValue != true)
            {
                shouldShowError = true;
                OnPropertyChanged("ErrorVisibility");
                return;

            }
                using (AppDBContext _dbContext = new AppDBContext())
            {
                _dbContext.Entry(Student.Faculty).State = EntityState.Unchanged;
                _dbContext.Entry(Student.Group).State = EntityState.Unchanged;
                _dbContext.Entry(Student.Speciality).State = EntityState.Unchanged;
                if (IsStudentNew)
                {
                    
                    _dbContext.Students.Add(Student);
                }
                else
                {
                    _dbContext.Students.Update(Student);
                }
                OnRequestClose(this, new EventArgs());
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
