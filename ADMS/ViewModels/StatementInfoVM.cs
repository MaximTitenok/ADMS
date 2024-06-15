using ADMS.Models;
using ADMS.Services;
using ADMS.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ADMS.ViewModels
{
    internal class StatementInfoVM : INotifyPropertyChanged
    {
        //TODO: Add information in orders and statements grids
        public Statement Statement { get; set; }
        public ObservableCollection<Statement> StatementList { get; set; }
        public ObservableCollection<StatementMark> MarksList { get; set; }
        public ICommand ChangeStatementButtonCommand { get; set; }
        public ICommand InfoAboutSubjectButtonCommand { get; set; }

        public StatementInfoVM(Statement statement) 
        {
            Statement = statement;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Statement = _dbContext.Statements
                    .Where(x => x.Id == Statement.Id)
                    .Include(x => x.Faculty)
                    .Include(x => x.Group)
                    .Include(x => x.MainTeacher)
                    .Include(x => x.PracticeTeacher)
                    .Include(x => x.Subject)
                    .Include(x => x.Subject.Department)
                    .Include(x => x.Subject.SubjectBank)
                    .FirstOrDefault() ?? new Statement();
                var marksList = _dbContext
                    .StatementMarks.Where(x => x.Student!= null && x.Statement.Id == Statement.Id)
                    .Include(x => x.Student)
                    .Include(x => x.Statement)
                    .Select(x => new StatementMark { Student = new Student { Id = x.Student.Id, Surname = x.Student.Surname, Name = x.Student.Name }, Mark = x.Mark })
                    .ToList();
                MarksList = new ObservableCollection<StatementMark>(marksList) ?? new ObservableCollection<StatementMark>();
            }
            ChangeStatementButtonCommand = new RelayCommand(ChangeStatement);
            InfoAboutSubjectButtonCommand = new RelayCommand(OpenInfoSubject);
        }
        private void ChangeStatement(object obj)
        {
            StatementInfoChangeView changeView = new (Statement);
            changeView.Show();
        }
        private void OpenInfoSubject(object obj)
        {
            SubjectInfoView infoView = new(Statement.Subject);
            infoView.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
