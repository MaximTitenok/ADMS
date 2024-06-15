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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Group = ADMS.Models.Group;


namespace ADMS.ViewModels
{
    internal class EmployeeRateInfoChangeVM : INotifyPropertyChanged
    {
        public EmployeeRate EmployeeRate { get; set; }
        public string?[] StaffingsArray { get; set; }
        public string?[] PositionsArray { get; set; }
        public string? Staffing {  get; set; }
        public string? Position { get; set; }
        public string[] EmployeesArray { get; set; }
        public string Employee {  get; set; }
        public ICommand SaverateInfoButtonCommand { get; set; }
        bool IsRateNew { get; set; }
        public string Title { get; set; }
        private bool shouldShowError;
        public Visibility ErrorVisibility
        {
            get { return shouldShowError ? Visibility.Visible : Visibility.Collapsed; }
        }
        public event EventHandler OnRequestClose;

        public EmployeeRateInfoChangeVM(EmployeeRate rate)
        {
            IsRateNew = false;
            Title = "Зміна інформації про тарифну ставку";
            EmployeeRate = new EmployeeRate(rate);
            StaffingsArray = StructureStore.GetStaffing();
            PositionsArray = StructureStore.GetPositions().Select(x => x.Name).ToArray();
            Staffing = StructureStore.GetStaffing(EmployeeRate.StaffingId);
            EmployeesArray = StructureStore.GetEmployees().Select(x => $"{x.Surname} {x.Name}").ToArray();
            Employee = $"{rate.Employee.Surname} {rate.Employee.Name}";
            Position = EmployeeRate.Position.Name;
            SaverateInfoButtonCommand = new RelayCommand(SaveRateInfo);
        }
        public EmployeeRateInfoChangeVM()
        {
            EmployeeRate = new EmployeeRate();
            Title = "Додати тарифну ставку";
            EmployeeRate.Department = StructureStore.GetDepartment();
            IsRateNew = true;
            EmployeesArray = StructureStore.GetEmployees().Select(x => $"{x.Surname} {x.Name}").ToArray();
            StaffingsArray = StructureStore.GetStaffing();
            PositionsArray = StructureStore.GetPositions().Select(x => x.Name).ToArray();
            SaverateInfoButtonCommand = new RelayCommand(SaveRateInfo);
        }
        private void SaveRateInfo(object obj)
        {

            EmployeeRate.StaffingId = StructureStore.GetStaffing(Staffing);
            EmployeeRate.Position = StructureStore.GetPositions().Where(x => x.Name == Position).FirstOrDefault();
            EmployeeRate.Employee = StructureStore.GetEmployees().Where(x => Employee == $"{x.Surname} {x.Name}").FirstOrDefault();
            if (EmployeeRate.Employee.Id == null || EmployeeRate?.Rate.ToString() == string.Empty|| EmployeeRate?.StaffingId == null ||
                EmployeeRate.Position.Id == null || EmployeeRate?.StartWork == null)
            {
                shouldShowError = true;
                OnPropertyChanged("ErrorVisibility");
                return;

            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                EmployeeRate.StartWork = DateTime.SpecifyKind((DateTime)EmployeeRate.StartWork, DateTimeKind.Utc);
                if(EmployeeRate.PlannedFinishWork != null)
                { 
                EmployeeRate.PlannedFinishWork = DateTime.SpecifyKind((DateTime)EmployeeRate.PlannedFinishWork, DateTimeKind.Utc);
                }
                if (EmployeeRate.FinishedWork != null)
                { 
                    EmployeeRate.FinishedWork = DateTime.SpecifyKind((DateTime)EmployeeRate.FinishedWork, DateTimeKind.Utc);
                }
                if (IsRateNew)
                {
                    _dbContext.Entry(EmployeeRate.Employee).State = EntityState.Unchanged;
                    _dbContext.Entry(EmployeeRate.Department).State = EntityState.Unchanged;
                    _dbContext.Entry(EmployeeRate.Position).State = EntityState.Unchanged;
                    _dbContext.EmployeeRates.Add(EmployeeRate);
                }
                else
                {
                    _dbContext.EmployeeRates.Update(EmployeeRate);
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
