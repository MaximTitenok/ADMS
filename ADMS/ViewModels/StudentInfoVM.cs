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
    internal class StudentInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public Student Student { get; set; }
        public ObservableCollection<Statement> StudentStatements { get; set; }
        public ObservableCollection<Order> StudentOrders { get; set; }
        public ICommand ChangeStudentInfoButtonCommand { get; set; }

        public StudentInfoVM(Student student) 
        {
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Student = _dbContext.Students
                    .Where(x => x.Id == student.Id)
                    .Include(x => x.Faculty)
                    .Include(x => x.Speciality)
                    .Include(x => x.Group)
                    .FirstOrDefault() ?? new Student();
                StudentStatements = new ObservableCollection<Statement>(_dbContext.Statements.Where(x => x.Group == Student.Group).Include(x => x.MainTeacher)) ?? new ObservableCollection<Statement>();
                StudentOrders = new ObservableCollection<Order>(_dbContext.Orders.Where(x => x.Students.ToArray().Contains(Student.Id) || x.Groups.ToArray().Contains(Student.Group.Id)).ToList());
                //StudentOrders = orders.Where(order => order.Students.ToArray().Contains(student.Id) || order.Groups.ToArray().Contains(student.Group.Id)));
            }
            ChangeStudentInfoButtonCommand = new RelayCommand(ChangeStudentInfo);
        }
        private void ChangeStudentInfo(object obj)
        {
            StudentInfoChangeView changeView = new (Student);
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
