using ADMS.Models;
using ADMS.Services;
using ADMS.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ADMS.ViewModels
{
    internal class OrderInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public Order Order { get; set; }
        public ObservableCollection<Group> GroupsList { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; }
        public ObservableCollection<DocFile> DocFilesList { get; set; }
        public ICommand ChangeOrderButtonCommand { get; set; }

        public OrderInfoVM(Order order) 
        {
            Order = order;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Order = _dbContext.Orders.Where(x => x.Id == Order.Id).Include(x => x.AddedEmplyoee).FirstOrDefault();
                GroupsList = new ObservableCollection<Group>(
                    _dbContext.Groups
                    .Where(x => Order.Groups
                    .ToArray()
                    .Contains(x.Id))
                    .Include(x => x.Faculty)
                    .Include(x => x.Department));
                StudentsList = new ObservableCollection<Student>(
                    _dbContext.Students
                    .Where(x => Order.Students
                    .ToArray()
                    .Contains(x.Id))
                    .Include(x => x.Group));

                DocFilesList = new ObservableCollection<DocFile>(
                    _dbContext.DocFiles
                    .Where(x => Order.File
                    .ToArray()
                    .Contains(x.Id))
                    .Include(x => x.AddedEmployee));
            }
            ChangeOrderButtonCommand = new RelayCommand(ChangeOrder);
        }
        private void ChangeOrder(object obj)
        {
           OrderChangeView orderView = new (Order);
            orderView.Show();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
