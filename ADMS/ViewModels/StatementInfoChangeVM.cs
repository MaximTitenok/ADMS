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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Group = ADMS.Models.Group;


namespace ADMS.ViewModels
{
    internal class StatementInfoChangeVM : INotifyPropertyChanged
    {
        //TODO: Add requirements and checks for fields to accept the changes in thw BD
        public Statement Statement { get; set; }
        public ObservableCollection<StatementMark> MarksList { get; set; }
        public string[] TeachersList { get; set; }
        public string MainTeacher { get; set; }
        public string PracticeTeacher { get; set; }
        public string StatementStatus { get; set; }
        
        public ICommand SaveStatementInfoButtonCommand { get; set; }
        bool IsStatementNew { get; set; }

        public StatementInfoChangeVM(Statement statement)
        {
            IsStatementNew = false;
            Statement = new Statement(statement);
            MainTeacher = Statement.MainTeacher.Surname;
            PracticeTeacher = Statement.PracticeTeacher.Surname;

            if (Statement.Status == false)
            {
                StatementStatus = "Closed";
            }
            else
            {
                StatementStatus = "Open";
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                MarksList = new ObservableCollection<StatementMark>(
                   _dbContext.StatementMarks
                   .Where(x => x.Statement == Statement)
                   .Include(x => x.Student)
                   .ToArray());
            }
            var TeachersList = StructureStore.GetEmployees()
                .Where(employee => employee.EmployeeRates
                .Any(rate => rate.Department.Id == Statement.SubjectId.Department.Id))
                .Select(employee => employee.Surname)
                .ToArray();
            //TeachersList = StructureStore.GetEmployees().Where(x => x.TeacherRates.Where(x => x.Department.Id) == Statement.MainTeacher.Department.Id).Select(x => x.Surname).ToArray();
            SaveStatementInfoButtonCommand = new RelayCommand(SaveStatementInfo);
        }
        public StatementInfoChangeVM()
        {
            //TODO: Repair the adding of statement
            IsStatementNew = true;
            Statement = new Statement();
            StatementStatus = "Open";
            //TeachersList = StructureStore.GetEmployees().Where(x => x.Department.Id == Statement.Teacher.Department.Id).Select(x => x.Surname).ToArray();
            SaveStatementInfoButtonCommand = new RelayCommand(SaveStatementInfo);
        }
        private void SaveStatementInfo(object obj)
        {
            Statement.Status = StatementStatus == "Open" ? true : false;
            
            using (AppDBContext _dbContext = new AppDBContext())
            {
                if (IsStatementNew)
                {
                    Statement.AddedTime = DateTime.UtcNow;

                    _dbContext.Entry(Statement.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.SubjectId).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Group).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.MainTeacher).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.PracticeTeacher).State = EntityState.Unchanged;
                    _dbContext.Statements.Add(Statement);
                }
                else
                {
                    _dbContext.Entry(Statement.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.SubjectId).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Group).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.MainTeacher).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.PracticeTeacher).State = EntityState.Unchanged;
                    _dbContext.Statements.Update(Statement);
                }
                _dbContext.SaveChanges();

            }

        }
  
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
