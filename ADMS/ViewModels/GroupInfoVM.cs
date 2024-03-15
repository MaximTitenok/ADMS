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
    internal class GroupInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public Group Group { get; set; }
        public ObservableCollection<Student> StudentsList { get; set; }
        public ICommand ChangeGroupInfoButtonCommand { get; set; }

        public GroupInfoVM(Group group) 
        {
            Group = group;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Group = _dbContext.Groups
                    .Where(x => x.Id == Group.Id)
                    .Include(x => x.Faculty)
                    .Include(x => x.Department)
                    .FirstOrDefault() ?? new Group();
                var studentList = _dbContext.Students
                    .Where(x => x.Group == Group)
                    .ToList();
                StudentsList = new ObservableCollection<Student>(studentList) ?? new ObservableCollection<Student>();
            }
            ChangeGroupInfoButtonCommand = new RelayCommand(ChangeGroup);
        }
        private void ChangeGroup(object obj)
        {
            //TODO: Create changegroupview
           /* StatementChangeView changeView = new (Statement);
            changeView.Show();*/

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
