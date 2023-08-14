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
    /// Interaction logic for DeansOfficeView.xaml
    /// </summary>
    public partial class DeansOfficeView : Window
    {
        private static int _facultyId = 1;//FIT
        DeansOfficeVM deansOfficeVM = new(_facultyId);

       

        public DeansOfficeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            DataContext = deansOfficeVM;

           
            // = new RelayCommand(new Action<object>(FindStudentsByConditions));
        }

       

    }
}
