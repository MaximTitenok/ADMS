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
    /// Interaction logic for Common.xaml
    /// </summary>
    public partial class CommonView : Window
    {
        public CommonView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeansOfficeView deansOfficeView = new DeansOfficeView();
            deansOfficeView.Show();
        }
    }
}
