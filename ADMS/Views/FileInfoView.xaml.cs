using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
/*using Windows.Storage.AccessCache;*/
using Windows.Storage.Pickers;
using System.Windows.Threading; // For Dispatcher.

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class FileInfoView : Window
    {
       // private Windows.Storage.StorageFolder _fileAccess = null;
        internal FileInfoView(DocFile file)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            FileInfoVM fileInfoVM = new(file);
            DataContext = fileInfoVM;

        }
      
    }
}
