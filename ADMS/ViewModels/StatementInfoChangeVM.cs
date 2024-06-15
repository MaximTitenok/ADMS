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
        public Statement Statement { get; set; }
        public ObservableCollection<StatementMark> MarksList { get; set; }
        public string[] GroupsList { get; set; }
        public string Group { get; set; }
        public bool GroupIsEnable { get; set; }
        public string[] SubjectsList { get; set; }
        public string Subject { get; set; }
        public string[] TeachersList { get; set; }
        public string MainTeacher { get; set; }
        public string PracticeTeacher { get; set; }
        public string StatementStatus { get; set; }
        public string Title { get; set; }

        private bool shouldShowError;
        public Visibility ErrorVisibility
        {
            get { return shouldShowError ? Visibility.Visible : Visibility.Collapsed; }
        }
        public ICommand SaveStatementInfoButtonCommand { get; set; }
        bool IsStatementNew { get; set; }
        public event EventHandler OnRequestClose;

        public StatementInfoChangeVM(Statement statement)
        {
            IsStatementNew = false;
            GroupIsEnable = false;
            Title = "Зміна інформації про відомість";
            Statement = new Statement(statement);
            MainTeacher = Statement?.MainTeacher?.Surname +" "+ Statement?.MainTeacher?.Name ?? "";
            PracticeTeacher = Statement?.PracticeTeacher?.Surname + " " + Statement?.PracticeTeacher?.Name ?? "";
            SubjectsList = StructureStore.GetSubjects()
                .Where(x => x.Department.Faculty.Id == Statement.Faculty.Id)
                .Select(x => $"{x.SubjectBank.Name} ({x.Semester} семестр)" ).ToArray();
            Subject = $"{Statement.Subject.SubjectBank.Name} ({Statement.Subject.Semester} семестр)" ?? "";
            GroupsList = StructureStore.GetGroups().
                Where(x => x.Faculty.Id == Statement.Faculty.Id)
                .Select(x => x.Name).ToArray() ;
            Group = Statement.Group.Name;
            if (Statement.Status == false)
            {
                StatementStatus = "Закрита";
            }
            else
            {
                StatementStatus = "Відкрита";
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                MarksList = new ObservableCollection<StatementMark>(
                   _dbContext.StatementMarks
                   .Where(x => x.Statement == Statement)
                   .Include(x => x.Student)
                   .ToArray());
            }
            TeachersList = StructureStore.GetEmployeeRates()
                .Where(rate => rate.Department.Faculty.Id == Statement.Faculty.Id)
                .Select(rate => rate.Employee).Select(x => $"{x.Surname} {x.Name}").ToArray();
            SaveStatementInfoButtonCommand = new RelayCommand(SaveStatementInfo);
        }
        public StatementInfoChangeVM()
        {
            Statement = new Statement();
            Statement.StartDate = DateTime.UtcNow;
            Statement.ClosedDate = DateTime.UtcNow;
            Statement.EndDate = DateTime.UtcNow;
            GroupIsEnable = true;
            SubjectsList = StructureStore.GetSubjects()
                .Where(x => x.Department.Faculty.Id == StructureStore.GetFaculties().Id)
                .Select(x => $"{x.SubjectBank.Name} ({x.Semester} семестр)").ToArray();
            GroupsList = StructureStore.GetGroups().
                Where(x => x.Faculty.Id == StructureStore.GetFaculties().Id)
                .Select(x => x.Name).ToArray();
            Statement.Faculty = StructureStore.GetFaculties();
            IsStatementNew = true;
            Title = "Створити відомість";
            StatementStatus = "Відкрита";
            TeachersList = StructureStore.GetEmployeeRates()
                .Where(rate => rate.Department.Faculty.Id == StructureStore.GetFaculties().Id)
                .Select(rate => rate.Employee).Select(x => $"{x.Surname} {x.Name}").ToArray();
            SaveStatementInfoButtonCommand = new RelayCommand(SaveStatementInfo);
        }
        private void SaveStatementInfo(object obj)
        {
            Statement.Status = StatementStatus == "Відкрита" ? true : false;
            
            using (AppDBContext _dbContext = new AppDBContext())
            {
                Statement.Group = Group != null ? StructureStore.GetGroups().Where(x => x.Name == Group).FirstOrDefault() : null;
                Statement.Subject = Subject != null ? StructureStore.GetSubjects().Where(x => Subject.Contains(x.SubjectBank.Name) && Subject.Contains(x.Semester.ToString())).FirstOrDefault() : null;
                Statement.MainTeacher = MainTeacher != null ? StructureStore.GetEmployees().Where(x => MainTeacher.Contains(x.Surname) && MainTeacher.Contains(x.Name)).FirstOrDefault() : null;
                Statement.PracticeTeacher = PracticeTeacher != null ? StructureStore.GetEmployees().Where(x => PracticeTeacher.Contains(x.Surname) && PracticeTeacher.Contains(x.Name)).FirstOrDefault() : null ;
                Statement.StartDate = DateTime.SpecifyKind((DateTime)Statement.StartDate, DateTimeKind.Utc);
                Statement.ClosedDate = DateTime.SpecifyKind((DateTime)Statement.ClosedDate, DateTimeKind.Utc);
                Statement.EndDate = DateTime.SpecifyKind((DateTime)Statement.EndDate, DateTimeKind.Utc);
                Statement.Semester = Statement?.Subject?.Semester;
                if (Statement.StatementNumber == null || Statement?.Subject?.Id == null || Statement?.StartDate == null || Statement?.EndDate == null ||
                Statement.Group == null || Statement?.MainTeacher?.Id == null )
                {
                    shouldShowError = true;
                    OnPropertyChanged("ErrorVisibility");
                    return;

                }
                if (IsStatementNew)
                {
                    Statement.AddedTime = DateTime.UtcNow;

                    _dbContext.Entry(Statement.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Subject).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Group).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.MainTeacher).State = EntityState.Unchanged;
                    if(PracticeTeacher != null) {
                        _dbContext.Entry(Statement.PracticeTeacher).State = EntityState.Unchanged;
                            }
                    _dbContext.Statements.Add(Statement);
                }
                else
                {
                    _dbContext.Entry(Statement.Faculty).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Subject).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.Group).State = EntityState.Unchanged;
                    _dbContext.Entry(Statement.MainTeacher).State = EntityState.Unchanged;
                    if (PracticeTeacher != null && !string.IsNullOrWhiteSpace(PracticeTeacher) )
                    {
                        _dbContext.Entry(Statement.PracticeTeacher).State = EntityState.Unchanged;
                    }
                    _dbContext.Statements.Update(Statement);
                }
                OnRequestClose(this, new EventArgs());
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
