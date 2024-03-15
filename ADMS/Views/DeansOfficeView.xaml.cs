using ADMS.Models;
using ADMS.ViewModels;
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
using MessageBox = System.Windows.MessageBox;

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for DeansOfficeView.xaml
    /// </summary>
    public partial class DeansOfficeView : Window
    {
        public static int _facultyId = 1;//FIT
        DeansOfficeVM deansOfficeVM = new(_facultyId);

        public DeansOfficeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            DataContext = deansOfficeVM;
        }
        

        private void StudentFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Student selectedItem = StudentsGrid.SelectedItem as Student;
                if (selectedItem != null)
                {
                    StudentInfoView studentInfo = new(selectedItem);
                    studentInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
        private void GroupFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Group selectedItem = GroupsGrid.SelectedItem as Group;
                if (selectedItem != null)
                {
                    GroupInfoView groupInfo = new(selectedItem);
                    groupInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }



    }
}
