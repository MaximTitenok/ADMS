using ADMS.Models;
using ADMS.Services;
using ADMS.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Windows.Storage.Pickers;

namespace ADMS.ViewModels
{
    internal class FileInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public DocFile DocFile { get; set; }
        private string _saveFilesPath = Directory.GetCurrentDirectory().ToString() + "\\saves";
        public ICommand ChangeFileInfoButtonCommand { get; set; }
        public ICommand DownloadFileButtonCommand { get; set; }
        public ICommand OpenFolderButtonCommand { get; set; }
        //private Windows.Storage.StorageFolder _fileAccess = null;

        public FileInfoVM(DocFile docFile) 
        {
            DocFile = docFile;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                DocFile = _dbContext.DocFiles
                    .Where(x => x.Id == DocFile.Id)
                    .Include(x => x.AddedEmployee)
                    .FirstOrDefault() ?? new DocFile();
            }
            ChangeFileInfoButtonCommand = new RelayCommand(ChangeFile);
            DownloadFileButtonCommand = new RelayCommand(DownloadFile);
            OpenFolderButtonCommand = new RelayCommand(OpenFolder);
        }
        private void ChangeFile(object obj)
        {
            //TODO: Create changestatementview
           /* StatementChangeView changeView = new (Statement);
            changeView.Show();*/

        }
        private async void DownloadFile(object obj)
        {
            if (!Directory.Exists(_saveFilesPath))
            {
                Directory.CreateDirectory(_saveFilesPath);
            }
            if(!File.Exists(_saveFilesPath+"\\"+DocFile.Name))
            {
                FileStream file = File.Create(_saveFilesPath + "\\" + DocFile.Name);
                file.Close();
            }
            File.WriteAllBytes(_saveFilesPath + "\\" + DocFile.Name, DocFile.File);

        }
        private void OpenFolder(object obj)
        {
            if (!Directory.Exists(_saveFilesPath))
            {
                Directory.CreateDirectory(_saveFilesPath);
            }
            Process.Start("explorer.exe", _saveFilesPath);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
