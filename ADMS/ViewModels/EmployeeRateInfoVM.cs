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
    internal class EmployeeRateInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public EmployeeRate EmployeeRate { get; set; }
        public ICommand ChangeRateInfoButtonCommand { get; set; }

        public EmployeeRateInfoVM(EmployeeRate rate) 
        {
            using (AppDBContext _dbContext = new AppDBContext())
            {
                EmployeeRate = _dbContext.EmployeeRates
                    .Where(x => x.Id == rate.Id)
                    .Include(x => x.Employee)
                    .Include(x => x.Position)
                    .Include(x => x.Department)
                    .FirstOrDefault() ?? new EmployeeRate();
            }
            ChangeRateInfoButtonCommand = new RelayCommand(ChangeRateInfo);
        }
        private void ChangeRateInfo(object obj)
        {
            EmployeeRateInfoChangeView changeView = new (EmployeeRate);
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
