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
            Student = student;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                StudentStatements = new ObservableCollection<Statement>(_dbContext.Statements.Include(x => x.Teacher)) ?? new ObservableCollection<Statement>();
                var orders = _dbContext.Orders.Where(x => x.Groups != null || x.Students != null).ToList();
                StudentOrders = new ObservableCollection<Order>(orders.Where(order => order.Students.Contains(student.Id) || order.Groups.Contains(student.Group.Id)));
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
