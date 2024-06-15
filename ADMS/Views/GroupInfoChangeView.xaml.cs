using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ADMS.Models;
using ADMS.ViewModels;

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class GroupInfoChangeView : Window
    {
        internal GroupInfoChangeVM GroupInfoChangeVM { get; set; }
        internal GroupInfoChangeView(Group group)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            GroupInfoChangeVM groupInfoChangeVM = new(group);
            GroupInfoChangeVM = groupInfoChangeVM;
            groupInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = groupInfoChangeVM;
        }
        internal GroupInfoChangeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            GroupInfoChangeVM groupInfoChangeVM = new();
            GroupInfoChangeVM = groupInfoChangeVM;
            groupInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = groupInfoChangeVM;
        }
        private void StudentRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Student selectedItem = StudentsGrid.SelectedItem as Student;
                if (selectedItem != null)
                {
                    StudentInfoView studentInfoView = new(selectedItem);
                    studentInfoView.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
        private void StudentSelectedClick(object sender, SelectedCellsChangedEventArgs e)
        {
            Student selectedItem = StudentsGrid.SelectedItem as Student;
            if (selectedItem != null && selectedItem.Surname != null)
            {
                GroupInfoChangeVM.SelectedStudentToRemove = selectedItem;
            }
        }
    }
}
