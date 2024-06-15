using ADMS.Models;
using ADMS.Services;
using ADMS.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Group = ADMS.Models.Group;


namespace ADMS.ViewModels
{
    internal class OrderChangeVM : INotifyPropertyChanged
    {
        public DocFile DocFile { get; set; }
        private string _saveFilesPath = Directory.GetCurrentDirectory().ToString() + "\\saves";
        public Order Order { get; set; }
        public Student SelectedStudentToRemove { get; set; }
        public Group SelectedGroupToRemove { get; set; }
        public DocFile SelectedFileToRemove { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Group> GroupsList { get; set; } = new ObservableCollection<Group>();
        public ObservableCollection<DocFile> FilesList { get; set; } = new ObservableCollection<DocFile>();
        public string?[] StatusesArray { get; set; } = { "Діє", "Не діє" };
        public string Status { get; set; }
        public string?[] TypesArray { get; set; }
        public string Type { get; set; }
        public ICommand SaveOrderInfoButtonCommand { get; set; }
        public ICommand AddStudentToOrderButtonCommand { get; set; }
        public ICommand RemoveStudentFromOrderButtonCommand { get; set; }
        public ICommand AddGroupToOrderButtonCommand { get; set; }
        public ICommand RemoveGroupFromOrderButtonCommand { get; set; }
        public ICommand AddFileToOrderButtonCommand { get; set; }
        public ICommand RemoveFileFromOrderButtonCommand { get; set; }
        bool IsOrderNew { get; set; }
        public string Title { get; set; }
        private bool shouldShowError;
        public Visibility ErrorVisibility
        {
            get { return shouldShowError ? Visibility.Visible : Visibility.Collapsed; }
        }
        public ICommand SaveStudentInfoButtonCommand { get; set; }
        public event EventHandler OnRequestClose;

        public OrderChangeVM(Order order)
        {
            IsOrderNew = false;
            Order = new Order(order);
            using (AppDBContext _dbContext = new AppDBContext())
            {
                StudentsList = new ObservableCollection<Student>(
                   _dbContext.Students
                   .Where(x => Order.Students.Contains(x.Id))
                   .ToArray());
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                GroupsList = new ObservableCollection<Group>(
                   _dbContext.Groups
                   .Where(x => Order.Groups.Contains(x.Id))
                   .ToArray());
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                FilesList = new ObservableCollection<DocFile>(
                   _dbContext.DocFiles
                   .Where(x => Order.File.Contains(x.Id))
                   .ToArray());
            }
            
            TypesArray = StructureStore.GetOrderTypes();
            Type = StructureStore.GetOrderTypes()[(int)Order.Type];
            Status = Order.Status == 1 ? "Діє" : "Не діє";
            SaveOrderInfoButtonCommand = new RelayCommand(SaveOrderInfo);
            AddStudentToOrderButtonCommand = new RelayCommand(AddStudentToOrderInfo);
            RemoveStudentFromOrderButtonCommand = new RelayCommand(RemoveStudentFromOrderInfo);
            AddGroupToOrderButtonCommand = new RelayCommand(AddGroupToOrderInfo);
            RemoveGroupFromOrderButtonCommand = new RelayCommand(RemoveGroupFromOrderInfo);
            AddFileToOrderButtonCommand = new RelayCommand(AddFileToOrderInfo);
            RemoveFileFromOrderButtonCommand = new RelayCommand(RemoveFileFromOrderInfo);
            Title = "Зміна інформації про наказ";
            shouldShowError = false;
        }
        public OrderChangeVM()
        {

            IsOrderNew = true;
            Order = new Order();
            TypesArray = StructureStore.GetOrderTypes();
            Status = "Діє";
            SaveOrderInfoButtonCommand = new RelayCommand(SaveOrderInfo);
            AddStudentToOrderButtonCommand = new RelayCommand(AddStudentToOrderInfo);
            RemoveStudentFromOrderButtonCommand = new RelayCommand(RemoveStudentFromOrderInfo);
            AddGroupToOrderButtonCommand = new RelayCommand(AddGroupToOrderInfo);
            RemoveGroupFromOrderButtonCommand = new RelayCommand(RemoveGroupFromOrderInfo);
            AddFileToOrderButtonCommand = new RelayCommand(AddFileToOrderInfo);
            RemoveFileFromOrderButtonCommand = new RelayCommand(RemoveFileFromOrderInfo);
            shouldShowError = false;
            Title = "Додати наказ";
        }
        private void SaveOrderInfo(object obj)
        {
            Order.Type = Array.IndexOf(StructureStore.GetOrderTypes(), Type);
            Order.Status = Status == "Діє" ? 1 : 0;
            if (Order.Number == string.Empty || Order.Name == string.Empty || Order?.Type == null || 
                Order?.Status == null || Order.StartDate == null)
            {
                shouldShowError = true;
                OnPropertyChanged("ErrorVisibility");
                return;

            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Order.Groups = GroupsList.Select(x => x.Id).ToArray();
                Order.Students = StudentsList.Select(x => x.Id).ToArray();
                Order.File = FilesList.Select(x => x.Id).ToArray();
                if (IsOrderNew)
                {

                    Order.StartDate = DateTime.SpecifyKind((DateTime)Order.StartDate, DateTimeKind.Utc);
                    Order.EndDate = DateTime.SpecifyKind((DateTime)Order.EndDate, DateTimeKind.Utc);
                    if(Order.AddedEmplyoee != null)
                    { 
                        _dbContext.Entry(Order.AddedEmplyoee).State = EntityState.Unchanged;
                    }
                    _dbContext.Orders.Add(Order);
                }
                else
                {
                    _dbContext.Entry(Order.AddedEmplyoee).State = EntityState.Unchanged;
                    _dbContext.Orders.Update(Order);
                }

                OnRequestClose(this, new EventArgs());
                _dbContext.SaveChanges();

            }

        }
        private void AddStudentToOrderInfo(object obj)
        {
            SearchStudentView searchView = new(this);
            searchView.Show();
        }

        private void RemoveStudentFromOrderInfo(object obj)
        {
            if(SelectedStudentToRemove?.Surname != null)
            { 
                StudentsList.Remove(SelectedStudentToRemove);
                OnPropertyChanged("StudentsList");
            }
            else
            {
                MessageBox.Show("Виберіть рядок!", "Помилка");
            }

        }
        private void AddGroupToOrderInfo(object obj)
        {
            SearchGroupView searchView = new(this);
            searchView.Show();
        }

        private void RemoveGroupFromOrderInfo(object obj)
        {
            if (SelectedGroupToRemove?.Name != null)
            {
                GroupsList.Remove(SelectedGroupToRemove);
                OnPropertyChanged("GroupsList");
            }
            else
            {
                MessageBox.Show("Виберіть рядок!", "Помилка");
            }

        }
        private void AddFileToOrderInfo(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(filePath);
                using (AppDBContext _dbContext = new AppDBContext())
                {
                    DocFile docFile = new DocFile();
                    docFile.AddedTime = DateTime.UtcNow;
                    docFile.File = fileBytes;
                    _dbContext.DocFiles.Add(docFile);
                    _dbContext.SaveChanges();
                    FilesList.Add(docFile);

                }
            }
            
        }

        private void RemoveFileFromOrderInfo(object obj)
        {
            if (SelectedFileToRemove?.Id != null)
            {
                FilesList.Remove(SelectedFileToRemove);
                OnPropertyChanged("FilesList");
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
