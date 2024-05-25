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

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for SearchStudentView.xaml
    /// </summary>
    public partial class SearchStudentView : Window
    {
        internal SearchStudentVM SearchStudentVM { get; set; }
        internal SearchStudentView(GroupInfoChangeVM groupInfoChangeVM)
        {
            InitializeComponent();
            SearchStudentVM VM = new(groupInfoChangeVM);
            SearchStudentVM = VM;
            DataContext = VM;
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
        private void StudentSelectedClick(object sender, SelectedCellsChangedEventArgs e)
        {
            Student selectedItem = StudentsGrid.SelectedItem as Student;
            if (selectedItem != null)
            {
                SearchStudentVM.Student = selectedItem;
            }
            else
            {
                MessageBox.Show("Invalid row", "Error");
            }
        }
        private void SelectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(SearchStudentVM.Student.Surname != null)
            {
                this.Hide();
            }
        }
        

    }
}
