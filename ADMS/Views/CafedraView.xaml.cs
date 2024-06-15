using ADMS.Models;
using ADMS.Services;
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
    public partial class CafedraView : Window
    {
        CafedraVM cafedraVM = new(StructureStore.GetFaculties().Id);

        public CafedraView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            DataContext = cafedraVM;
        }
        

        private void EmployeeFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Employee selectedItem = EmployeesGrid.SelectedItem as Employee;
                if (selectedItem != null)
                {
                    EmployeeInfoView employeeInfo = new(selectedItem);
                    employeeInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
        private void RateFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                EmployeeRate selectedItem = RatesGrid.SelectedItem as EmployeeRate;
                if (selectedItem != null)
                {
                    EmployeeRateInfoView rateInfo = new(selectedItem);
                    rateInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
        private void StatementFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Statement selectedItem = StatementGrid.SelectedItem as Statement;
                if (selectedItem != null)
                {
                    StatementInfoView statementInfo = new(selectedItem);
                    statementInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }

        
    }
}
