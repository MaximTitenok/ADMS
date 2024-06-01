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
    internal class EmployeeInfoVM : INotifyPropertyChanged
    {
        //TODO: Add inserting new employee by admin
        public Employee Employee { get; set; }
        public ObservableCollection<EmployeeRate> EmployeeRates { get; set; }

        public EmployeeInfoVM(Employee employee) 
        {
           using (AppDBContext _dbContext = new AppDBContext())
            {
                Employee = _dbContext.Employees
                    .Where(x => x.Id == employee.Id)
                    .Include(x => x.СorrectiveEmployee)
                    .FirstOrDefault() ?? new Employee();
                EmployeeRates = new ObservableCollection<EmployeeRate>(
                    _dbContext.EmployeeRates
                    .Where(x => x.Employee.Id == Employee.Id)
                    .Include(x => x.Department)
                    .Include(x => x.Position)) 
                    ?? new ObservableCollection<EmployeeRate>();
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
