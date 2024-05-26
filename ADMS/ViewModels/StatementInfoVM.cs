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

        public StatementInfoVM(Statement statement) 
        {
            Statement = statement;
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Statement = _dbContext.Statements
                    .Where(x => x.Id == Statement.Id)
                    .Include(x => x.Faculty)
                    .Include(x => x.Group)
                    .Include(x => x.Teacher)
                    .Include(x => x.SubjectId)
                    .Include(x => x.SubjectId.Department)
                    .Include(x => x.SubjectId.SubjectBankId)
                    .FirstOrDefault() ?? new Statement();
                var marksList = _dbContext
                    .StatementMarks.Where(x => x.Student!= null)
                    .Include(x => x.Student)
                    .Include(x => x.Statement)
                    .Select(x => new StatementMark { Student = new Student { Id = x.Student.Id, Surname = x.Student.Surname, Name = x.Student.Name }, Mark = x.Mark })
                    .ToList();
                MarksList = new ObservableCollection<StatementMark>(marksList) ?? new ObservableCollection<StatementMark>();
            }
            ChangeStatementButtonCommand = new RelayCommand(ChangeStatement);
        }
        private void ChangeStatement(object obj)
        {
            StatementInfoChangeView changeView = new (Statement);
            changeView.Show();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
