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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace ADMS.ViewModels
{
    internal class DeansOfficeVM : INotifyPropertyChanged
    {
        public List<Group> StudentGroups { get; set; }
        public List<Speciality> StudentSpecialities { get; set; }
        public List<Student> Students { get; set; }

        public string StudentFindConditionSurname { get; set; }
        public string StudentFindConditionName { get; set; }
        public string StudentFindConditionSecondname { get; set; }
        public string StudentFindConditionPassport { get; set; }
        public string StudentFindConditionGroup { get; set; }
        public string StudentFindConditionSpeciality { get; set; }
        public string StudentFindConditionGender { get; set; }
        public string StudentFindConditionStudentId { get; set; }
        public string StudentFindConditionStudyLevel { get; set; }
        public string StudentFindConditionStudyForm { get; set; }

        public string[] StudentStudyLevels { get; set; }
        public string[] StudentStudyForms { get; set; }

        public ICommand StudentFindButtonCommand { get; set; }
        public ICommand StudentClearButtonCommand { get; set; }
        public ICommand StudentAddButtonCommand { get; set; }
       




        public List<Group> Groups { get; set; }
        public string[] GroupDepartments { get; set; }

        public string GroupFindConditionName { get; set; }
        public string GroupFindConditionDepartment { get; set; }
        public DateTime GroupStartEducation { get; set; }
        public string GroupFindConditionStudentSurname { get; set; }
        public string GroupFindConditionStudentName { get; set; }

        public ICommand GroupFindButtonCommand { get; set; }
        public ICommand GroupClearButtonCommand { get; set; }
        public ICommand GroupAddButtonCommand { get; set; }


        public DeansOfficeVM(int facultyId) 
        {
            StudentGroups = StructureStore.GetGroups().Where(x => x?.Faculty?.Id == facultyId).ToList();
            StudentGroups.Insert(0,new Group { Name = "" });
            StudentSpecialities = StructureStore.GetSpecialities().Where(x => x.Faculty.Id == facultyId).ToList();
            StudentSpecialities.Insert(0,new Speciality { ShortName = "" });

            StudentStudyLevels = StructureStore.GetStudyLevels();
            StudentStudyForms = StructureStore.GetStudyForms();

            StudentFindButtonCommand = new RelayCommand(FindStudentsByConditions);
            StudentClearButtonCommand = new RelayCommand(StudentClearConditionFields);
            StudentAddButtonCommand = new RelayCommand(AddNewStudent);
            


            var departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == facultyId).ToList();
            departments.Insert(0, new Department { ShortName = "" });
            GroupDepartments = departments.Select(x => x.ShortName).ToArray();

            GroupFindButtonCommand = new RelayCommand(FindGroupsByConditions);
            GroupClearButtonCommand = new RelayCommand(GroupClearConditionFields);
            GroupAddButtonCommand = new RelayCommand(AddNewGroup);
        }

        private void FindStudentsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(StudentFindConditionSurname) && String.IsNullOrEmpty(StudentFindConditionName) 
                && String.IsNullOrEmpty(StudentFindConditionSecondname) && String.IsNullOrEmpty(StudentFindConditionGroup) 
                && String.IsNullOrEmpty(StudentFindConditionSpeciality) && String.IsNullOrEmpty(StudentFindConditionGender)
                && String.IsNullOrEmpty(StudentFindConditionStudentId) && String.IsNullOrEmpty(StudentFindConditionPassport)
                && String.IsNullOrEmpty(StudentFindConditionStudyLevel) && String.IsNullOrEmpty(StudentFindConditionStudyForm))
            {
                MessageBox.Show("All the fields is empty!","Error");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            { 
                var query = _dbContext.Students.AsQueryable();

                if (!string.IsNullOrEmpty(StudentFindConditionSurname))
                {
                    query = query.Where(student => student.Surname.Contains(StudentFindConditionSurname));
                }

                if (!string.IsNullOrEmpty(StudentFindConditionName))
                {
                    query = query.Where(student => student.Name.Contains(StudentFindConditionName));
                }

                if (!string.IsNullOrEmpty(StudentFindConditionSecondname))
                {
                    query = query.Where(student => student.Secondname.Contains(StudentFindConditionSecondname));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionPassport))
                {
                    query = query.Where(student => student.PassportId.Contains(StudentFindConditionPassport));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionGroup))
                {
                    query = query.Where(student => student.Group.Name.Contains(StudentFindConditionGroup));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionSpeciality))
                {
                    query = query.Where(student => student.Speciality.Name.Contains(StudentFindConditionSpeciality));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionGender))
                {
                    bool gender = false;
                    if (StudentFindConditionGender == "Male")
                    {
                        gender = false;
                    }
                    else if (StudentFindConditionGender == "Female")
                    {
                        gender = true;
                    }
                    query = query.Where(student => student.Gender == gender);
                }

                if (!string.IsNullOrEmpty(StudentFindConditionStudentId))
                {
                    if (StudentFindConditionStudentId.All(char.IsDigit))
                    {
                        query = query.Where(student => student.StudentId.ToString() == StudentFindConditionStudentId);
                    }
                    else
                    {
                        MessageBox.Show("Student ID contains letter!", "Error");
                    }
                }
                if (!string.IsNullOrEmpty(StudentFindConditionSpeciality))
                {
                    query = query.Where(student => student.Speciality.Name.Contains(StudentFindConditionSpeciality));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionStudyLevel))
                {
                    query = query.Where(student => student.StudyLevel == Array.IndexOf(StudentStudyLevels, StudentFindConditionStudyLevel));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionStudyForm))
                {
                    query = query.Where(student => student.StudyForm == Array.IndexOf(StudentStudyForms, StudentFindConditionStudyForm));
                }

                Students = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Group)
                    .Include(x => x.Speciality)
                    .ToList();
            }
            OnPropertyChanged("Students");
        }

        private void StudentClearConditionFields(object obj)
        {
            StudentFindConditionSurname = "";
            OnPropertyChanged("StudentFindConditionSurname");
            StudentFindConditionName = "";
            OnPropertyChanged("StudentFindConditionName");
            StudentFindConditionSecondname = "";
            OnPropertyChanged("StudentFindConditionSecondname");
            StudentFindConditionGroup = "";
            OnPropertyChanged("StudentFindConditionGroup");
            StudentFindConditionSpeciality = "";
            OnPropertyChanged("StudentFindConditionSpeciality");
            StudentFindConditionGender = "";
            OnPropertyChanged("StudentFindConditionGender");
            StudentFindConditionStudentId = "";
            OnPropertyChanged("StudentFindConditionStudentId");
            StudentFindConditionPassport = "";
            OnPropertyChanged("StudentFindConditionPassport");
            StudentFindConditionStudyLevel = "";
            OnPropertyChanged("StudentFindConditionStudyLevel");
            StudentFindConditionStudyForm = "";
            OnPropertyChanged("StudentFindConditionStudyForm");
        }

        private void AddNewStudent(object obj)
        {
            StudentInfoChangeView changeView = new();
            changeView.Show();
        }

        private void FindGroupsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(GroupFindConditionName) && GroupFindConditionDepartment == null
                && GroupStartEducation == DateTime.MinValue && String.IsNullOrEmpty(GroupFindConditionStudentSurname)
                && String.IsNullOrEmpty(GroupFindConditionStudentName))
            {
                MessageBox.Show("All the fields is empty!", "Error");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Groups.AsQueryable();

                if (!string.IsNullOrEmpty(GroupFindConditionName))
                {
                    query = query.Where(group => group.Name.Contains(GroupFindConditionName));
                }

                if (!string.IsNullOrEmpty(GroupFindConditionDepartment))//TODO: Add the space check
                {
                    query = query.Where(group => group.Department.ShortName == GroupFindConditionDepartment);
                }

                if (GroupStartEducation != DateTime.MinValue)
                {
                    query = query.Where(group => group.StartEducation == GroupStartEducation);
                }
                if (!string.IsNullOrEmpty(GroupFindConditionStudentSurname))
                {
                    query = _dbContext.Students
                        .Where(x => x.Surname.Contains(GroupFindConditionStudentSurname))
                        .Select(x => x.Group)
                        .Intersect(query).AsQueryable();
                }
                if (!string.IsNullOrEmpty(GroupFindConditionStudentName))
                {
                    query = _dbContext.Students
                        .Where(x => x.Name.Contains(GroupFindConditionStudentName))
                        .Select(x => x.Group)
                        .Intersect(query).AsQueryable();
                }

                Groups = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Department)
                    .ToList();
            }
            OnPropertyChanged("Groups");
        }

        private void GroupClearConditionFields(object obj)
        {
            GroupFindConditionName = "";
            OnPropertyChanged("GroupFindConditionName");
            GroupFindConditionDepartment = "";
            OnPropertyChanged("GroupFindConditionDepartments");
            GroupStartEducation = DateTime.MinValue;
            OnPropertyChanged("GroupStartEducation");
            GroupFindConditionStudentSurname = "";
            OnPropertyChanged("GroupFindConditionStudentSurname");
            GroupFindConditionStudentName = "";
            OnPropertyChanged("GroupFindConditionStudentName");
        }
        private void AddNewGroup(object obj)
        {
            GroupInfoChangeView changeView = new();
            changeView.Show();
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
