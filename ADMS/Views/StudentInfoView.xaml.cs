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
    public partial class StudentInfoView : Window
    {
        internal StudentInfoView(Student student)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            StudentInfoVM studentInfoVM = new(student);
            DataContext = studentInfoVM;
        }
        private void StatementsdRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Statement selectedItem = StatementsGrid.SelectedItem as Statement;
                if (selectedItem != null)
                {
                    StatementView statementView = new(selectedItem);
                    statementView.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }

    }
}
