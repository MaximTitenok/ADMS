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
    internal class SearchGroupVM : INotifyPropertyChanged
    {
        public List<Group> Groups { get; set; }
        public Group Group { get; set; }
        public string GroupFindConditionName { get; set; }
        public string GroupFindConditionDepartment { get; set; }
        public string[] GroupDepartments { get; set; }
        public DateTime GroupStartEducation { get; set; }
        public ICommand SearchGroupButtonCommand { get; set; }
        public ICommand SelectGroupButtonCommand { get; set; }
        public OrderChangeVM OrderChangeVM { get; set; }

        public SearchGroupVM(OrderChangeVM orderChangeVM) 
        {
            OrderChangeVM = orderChangeVM;

            var departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == StructureStore.GetFaculties().Id).ToList();
            departments.Insert(0, new Department { ShortName = "" });
            GroupDepartments = departments.Select(x => x.ShortName).ToArray();

            SearchGroupButtonCommand = new RelayCommand(SearchGroupsByConditions);
            SelectGroupButtonCommand = new RelayCommand(SelectGroupToAdd);
        }

        private void SearchGroupsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(GroupFindConditionName) && String.IsNullOrEmpty(GroupFindConditionDepartment)
               && GroupStartEducation == DateTime.MinValue)
            {
                MessageBox.Show("Всі поля пусті!", "Помилка");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Groups.AsQueryable();

                if (!string.IsNullOrEmpty(GroupFindConditionName))
                {
                    query = query.Where(group => group.Name.Contains(GroupFindConditionName));
                }

                if (!string.IsNullOrEmpty(GroupFindConditionDepartment))
                {
                    query = query.Where(group => group.Department.ShortName == GroupFindConditionDepartment);
                }

                if (GroupStartEducation != DateTime.MinValue)
                {
                    query = query.Where(group => group.StartEducation == GroupStartEducation);
                }
                Groups = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Department)
                    .Include(x => x.Speciality)
                    .ToList();
            }
            OnPropertyChanged("Groups");
        }
        private void SelectGroupToAdd(object obj)
        {
            if (Group == null)
            {
                MessageBox.Show("Select the row!", "Error");
                return;
            }
            if (OrderChangeVM != null)
            {
                OrderChangeVM.GroupsList.Add(Group);
                OrderChangeVM.OnPropertyChanged("OrdersList");
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
