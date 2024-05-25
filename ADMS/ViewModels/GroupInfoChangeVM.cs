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
        public List<Department> Departments { get; set; }
        public Student Student { get; set; }
        public Student SelectedStudentToRemove { get; set; }
        public List<Student> StudentsToRemove { get; set; } = new List<Student>();
        public ObservableCollection<Student> StudentsList { get; set; } = new ObservableCollection<Student>();
        public string?[] DepartmentsArray { get; set; }
        public string Department { get; set; }
        public ICommand SaveGroupInfoButtonCommand { get; set; }
        public ICommand AddStudentToGroupButtonCommand { get; set; }
        public ICommand RemoveStudentFromGroupButtonCommand { get; set; }
        bool IsGroupNew { get; set; }

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
            Departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == Group.Faculty.Id).ToList();
            DepartmentsArray = StructureStore.GetDepartments().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            Department = Group.Department.ShortName;
            SaveGroupInfoButtonCommand = new RelayCommand(SaveGroupInfo);
            AddStudentToGroupButtonCommand = new RelayCommand(AddStudentToGroupInfo);
            RemoveStudentFromGroupButtonCommand = new RelayCommand(RemoveStudentFromGroupInfo);
        }
        public GroupInfoChangeVM()
        {

            IsGroupNew = true;
            Group = new Group();
            Group.Faculty = StructureStore.GetFaculty();
            Departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == Group.Faculty.Id).ToList();
            DepartmentsArray = StructureStore.GetDepartments().Where(x => x.Faculty?.Id == Group?.Faculty?.Id).Select(x => x.ShortName).ToArray();
            SaveGroupInfoButtonCommand = new RelayCommand(SaveGroupInfo);
            AddStudentToGroupButtonCommand = new RelayCommand(AddStudentToGroupInfo);
            RemoveStudentFromGroupButtonCommand = new RelayCommand(RemoveStudentFromGroupInfo);
        }
        private void SaveGroupInfo(object obj)
        {
            Group.Department = StructureStore.GetDepartments().Where(x => x.ShortName == Department).FirstOrDefault();
            Group.StartEducation = Group.StartEducation.Value.ToUniversalTime();
            using (AppDBContext _dbContext = new AppDBContext())
            {
                if (IsGroupNew)
                {
                    Group.AddedTime = DateTime.UtcNow;
                    _dbContext.Entry(Group.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Department).State = EntityState.Unchanged;
                    _dbContext.Groups.Add(Group);
                }
                else
                {
                    _dbContext.Entry(Group.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Group.Department).State = EntityState.Unchanged;
                    _dbContext.Groups.Update(Group);
                }
                var students = _dbContext.Students.Where(x => StudentsList.Contains(x)).ToList();
                foreach(var student in students)
                {
                    student.Group = Group;
                }
                var studentsToRemove = _dbContext.Students.Where(x => StudentsToRemove.Contains(x));
                foreach (var student in studentsToRemove)
                {
                    student.Group = null;
                }

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
                MessageBox.Show("Select a row!", "Error");
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
