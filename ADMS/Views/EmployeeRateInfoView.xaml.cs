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
    public partial class EmployeeRateInfoView : Window
    {
        internal EmployeeRateInfoView(EmployeeRate rate)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            EmployeeRateInfoVM rateInfoVM = new(rate);
            DataContext = rateInfoVM;
        }
    }
}
