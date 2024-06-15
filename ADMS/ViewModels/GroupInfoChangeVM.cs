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
    internal class GroupInfoChangeVM : INotifyPropertyChanged
    {
        //TODO: Add requirements and checks for fields to accept the changes in thw BD
        public Group Group { get; set; }
        public Student Student { get; set; }
        public Student SelectedStudentToRemove { get; set; }
        public List<Student> StudentsToRemove { get; set; } = new List<Student>();
        public ObservableCollection<Student> StudentsList { get; set; } = new ObservableCollection<Student>();
        public string?[] DepartmentsArray { get; set; }
        private string department;
        public string Department {
            get
            {
                return department;
            }
            set 
            { 
                department = value; UpdateSpecialityList();
            } }

        public string?[] SpecialitiesArray { get; set; }
        public string Speciality { get; set; }
        public ICommand SaveGroupInfoButtonCommand { get; set; }
        public ICommand AddStudentToGroupButtonCommand { get; set; }
        public ICommand RemoveStudentFromGroupButtonCommand { get; set; }
        bool IsGroupNew { get; set; }
        public string Title { get; set; }
        //public bool ErrorVisibility { get; set; } = false;
        private bool shouldShowError;
        public Visibility ErrorVisibility
        {
            get { return shouldShowError ? Visibility.Visible : Visibility.Collapsed; }
        }
        public ICommand SaveStudentInfoButtonCommand { get; set; }
        public event EventHandler OnRequestClose;

        public GroupInfoChangeVM(Group group)
        {
            IsGroupNew = false;
            Group = new Group(group);
            using (AppDBContext _dbContext = new AppDBContext())
            {
                StudentsList = new ObservableCollection<Student>(
                   _dbContext.Students
                   .Where(x => x.Group == Group)
                   .ToArray());
            }
            DepartmentsArray = StructureStore.GetDepartments().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            Department = Group.Department.ShortName;
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            Speciality = Group.Speciality.ShortName;
            SaveGroupInfoButtonCommand = new RelayCommand(SaveGroupInfo);
            AddStudentToGroupButtonCommand = new RelayCommand(AddStudentToGroupInfo);
            RemoveStudentFromGroupButtonCommand = new RelayCommand(RemoveStudentFromGroupInfo);
            Title = "Зміна інформації про групу";
            shouldShowError = false;
        }
        public GroupInfoChangeVM()
        {

            IsGroupNew = true;
            Group = new Group();
            Group.Faculty = StructureStore.GetFaculties();
            DepartmentsArray = StructureStore.GetDepartments().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            SaveGroupInfoButtonCommand = new RelayCommand(SaveGroupInfo);
            AddStudentToGroupButtonCommand = new RelayCommand(AddStudentToGroupInfo);
            RemoveStudentFromGroupButtonCommand = new RelayCommand(RemoveStudentFromGroupInfo);
            shouldShowError = false;
            Title = "Додати групу";
        }
        private void UpdateSpecialityList()
        {
            SpecialitiesArray = StructureStore.GetSpecialities().Where(x => x.Faculty?.Id == StructureStore.GetDepartments().Where(x => x.ShortName == Department).Select(x => x.Id).FirstOrDefault()).Select(x => x.ShortName).ToArray();
            Speciality = SpecialitiesArray.FirstOrDefault();
            OnPropertyChanged("SpecialitiesArray");
            OnPropertyChanged("Speciality");
        }
        private void SaveGroupInfo(object obj)
        {
            Group.Department = StructureStore.GetDepartments().Where(x => x.ShortName == Department).FirstOrDefault();
            Group.Speciality = StructureStore.GetSpecialities().Where(x => x.ShortName == Speciality).FirstOrDefault();
            if (Group.Name == string.Empty || Group?.Faculty?.Id == null || Group?.Department?.Id == null ||
                Group.StartEducation == null || Group?.Speciality?.Id == null)
            {
                shouldShowError = true;
                OnPropertyChanged("ErrorVisibility");
                return;

            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                if (IsGroupNew)
                {
                    Group.IsActive = true;
                    Group.AddedTime = DateTime.UtcNow;
                    Group.StartEducation = DateTime.SpecifyKind((DateTime)Group.StartEducation, DateTimeKind.Utc);
                    _dbContext.Entry(Group.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Department).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Speciality).State = EntityState.Unchanged;
                    _dbContext.Groups.Add(Group);
                }
                else
                {
                    _dbContext.Entry(Group.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Department).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Speciality).State = EntityState.Unchanged;
                    _dbContext.Groups.Update(Group);
                }
                var students = _dbContext.Students.Where(x => StudentsList.Contains(x)).ToList();
                foreach(var student in students)
                {
                    student.Group = Group;
                    student.Speciality = Group.Speciality;
                    student.Faculty = Group.Faculty;
                    _dbContext.Entry(student.Faculty).State = EntityState.Unchanged;
                }
                var studentsToRemove = _dbContext.Students.Where(x => StudentsToRemove.Contains(x));
                foreach (var student in studentsToRemove)
                {
                    student.Group = null;
                }
                OnRequestClose(this, new EventArgs());
                _dbContext.SaveChanges();

            }

        }
        private void AddStudentToGroupInfo(object obj)
        {
            Student = new Student();
            SearchStudentView searchView = new(this);
            searchView.Show();
        }

        private void RemoveStudentFromGroupInfo(object obj)
        {
            if(SelectedStudentToRemove?.Surname != null)
            { 
                StudentsList.Remove(SelectedStudentToRemove);
                StudentsToRemove.Add(SelectedStudentToRemove);
                OnPropertyChanged("StudentsList");
            }
            else
            {
                MessageBox.Show("Виберіть рядок!", "Помилка");
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
