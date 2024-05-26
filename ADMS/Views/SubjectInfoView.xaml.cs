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
    public partial class SubjectInfoView : Window
    {
        internal SubjectInfoView(Subject subject)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            SubjectInfoVM subjectInfoVM = new(subject);
            DataContext = subjectInfoVM;
        }
    }
}
