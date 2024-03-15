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
using MessageBox = System.Windows.MessageBox;

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        internal OrderView(Order order)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            OrderInfoVM orderVM = new(order);
            DataContext = orderVM;

        }
        
        private void GroupRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Group selectedItem = GroupsGrid.SelectedItem as Group;
                 if (selectedItem != null)
                 {
                     GroupInfoView groupInfoView = new(selectedItem);
                    groupInfoView.Show();
                 }
                 else
                 {
                     MessageBox.Show("Invalid row", "Error");
                 }
            }
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
        private void FileRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                 DocFile selectedItem = FilesGrid.SelectedItem as DocFile;
                 if (selectedItem != null)
                 {
                     FileInfoView fileInfoView = new(selectedItem);
                    fileInfoView.Show();
                 }
                 else
                 {
                     MessageBox.Show("Invalid row", "Error");
                 }
            }
        }
    }
}
