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

namespace ADMS.ViewModels
{
    internal class SearchStudentVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public List<Student> Students { get; set; }
        public Student Student { get; set; }
        public string StudentFindConditionSurname { get; set; }
        public string StudentFindConditionName { get; set; }
        public string StudentFindConditionStudentId { get; set; }
        public ICommand SearchStudentButtonCommand { get; set; }
        public ICommand SelectStudentButtonCommand { get; set; }
        public GroupInfoChangeVM GroupInfoChangeVM { get; set; }

        public SearchStudentVM(GroupInfoChangeVM groupInfoChangeVM) 
        {
            GroupInfoChangeVM = groupInfoChangeVM;
            SearchStudentButtonCommand = new RelayCommand(SearchStudentsByConditions);
            SelectStudentButtonCommand = new RelayCommand(SelectStudentToAdd);
        }

        private void SearchStudentsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(StudentFindConditionSurname) && String.IsNullOrEmpty(StudentFindConditionName)
               && String.IsNullOrEmpty(StudentFindConditionStudentId))
            {
                MessageBox.Show("All the fields is empty!", "Error");
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
                Students = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Group)
                    .Include(x => x.Speciality)
                    .ToList();
            }
            OnPropertyChanged("Students");
        }
        private void SelectStudentToAdd(object obj)
        {
            if (Student == null)
            {
                MessageBox.Show("Select the row!", "Error");
                return;
            }
            GroupInfoChangeVM.StudentsList.Add(Student);
            GroupInfoChangeVM.OnPropertyChanged("StudentsList");
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
