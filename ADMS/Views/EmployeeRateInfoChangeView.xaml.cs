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
    public partial class EmployeeRateInfoChangeView : Window
    {
        internal EmployeeRateInfoChangeView(EmployeeRate rate)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            EmployeeRateInfoChangeVM employeeRateInfoChangeVM = new(rate);
            employeeRateInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = employeeRateInfoChangeVM;
        }
        internal EmployeeRateInfoChangeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            EmployeeRateInfoChangeVM employeeRateInfoChangeVM = new();
            employeeRateInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = employeeRateInfoChangeVM;
        }

    }
}
