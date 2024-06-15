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
    public partial class OrderChangeView : Window
    {
        internal OrderChangeVM OrderInfoChangeVM { get; set; }
        internal OrderChangeView(Order order)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            OrderChangeVM orderInfoChangeVM = new(order);
            OrderInfoChangeVM = orderInfoChangeVM;
            orderInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = orderInfoChangeVM;
        }
        internal OrderChangeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            OrderChangeVM orderInfoChangeVM = new();
            OrderInfoChangeVM = orderInfoChangeVM;
            orderInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = orderInfoChangeVM;
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
        private void GroupRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Group selectedItem = GroupsGrid.SelectedItem as Group;
                if (selectedItem != null)
                {
                    GroupInfoView studentInfoView = new(selectedItem);
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
                OrderInfoChangeVM.SelectedStudentToRemove = selectedItem;
            }
        }
        private void GroupSelectedClick(object sender, SelectedCellsChangedEventArgs e)
        {
            Group selectedItem = GroupsGrid.SelectedItem as Group;
            if (selectedItem != null && selectedItem.Name != null)
            {
                OrderInfoChangeVM.SelectedGroupToRemove = selectedItem;
            }
        }
        private void OrderSelectedClick(object sender, SelectedCellsChangedEventArgs e)
        {
            DocFile selectedItem = FilesGrid.SelectedItem as DocFile;
            if (selectedItem != null && selectedItem.Id != null)
            {
                OrderInfoChangeVM.SelectedFileToRemove = selectedItem;
            }
        }
        }
    }
