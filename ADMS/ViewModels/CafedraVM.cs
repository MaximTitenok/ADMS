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
using MessageBox = System.Windows.MessageBox;

namespace ADMS.ViewModels
{
    internal class CafedraVM : INotifyPropertyChanged
    {
        public List<Employee> Employees { get; set; }

        public string EmployeeFindConditionSurname { get; set; }
        public string EmployeeFindConditionName { get; set; }
        public string EmployeeFindConditionSecondname { get; set; }
        public string EmployeeFindConditionPhone { get; set; }
        public string EmployeeFindConditionGender { get; set; }



        public ICommand EmployeeFindButtonCommand { get; set; }
        public ICommand EmployeeClearButtonCommand { get; set; }
       




        public List<EmployeeRate> EmployeeRates { get; set; }
        public string[] RateDepartments { get; set; }
        public string[] RatePositions { get; set; }

        public string RateFindConditionSurname { get; set; }
        public string RateFindConditionName { get; set; }
        public string RateFindConditionDepartment { get; set; }
        public string RateFindConditionRateFrom { get; set; }
        public string RateFindConditionRateTo { get; set; }
        public string RateFindConditionPosition { get; set; }
        public DateTime RateFindConditionStartWork { get; set; }
        public DateTime RateFindConditionFinishedWork { get; set; }

        public ICommand RateFindButtonCommand { get; set; }
        public ICommand RateClearButtonCommand { get; set; }
        public ICommand RateAddButtonCommand { get; set; }




        public List<Statement> Statements { get; set; }
        public List<Subject> Subjects { get; set; }
        public string[] SubjectDepartments { get; set; }
        public string[] StatementSubjects { get; set; }

        public string StatementFindConditionSemester { get; set; }
        private string statementFindConditionDepartment;
        public string StatementFindConditionDepartment 
        { 
            get { return statementFindConditionDepartment; } 
            set 
            { 
                statementFindConditionDepartment = value;
                StatementUpdateSubjects();
            }
        }
        public string StatementFindConditionSubject { get; set; }
        public string StatementFindConditionECTS { get; set; }
        public string StatementFindConditionExam { get; set; }
        public string StatementFindConditionCredit { get; set; }
        public string StatementFindConditionCP { get; set; }
        public string StatementFindConditionCGW { get; set; }
        public string StatementFindConditionDiploma { get; set; }

        public ICommand SubjectFindButtonCommand { get; set; }
        public ICommand SubjectClearButtonCommand { get; set; }
        public ICommand SubjectAddButtonCommand { get; set; }


        public CafedraVM(int facultyId) 
        {
            EmployeeFindButtonCommand = new RelayCommand(FindEmployeesByConditions);
            EmployeeClearButtonCommand = new RelayCommand(EmployeeClearConditionFields);

            var departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == facultyId).ToList();
            departments.Insert(0, new Department { ShortName = "" });
            RateDepartments = departments.Select(x => x.ShortName).ToArray();

            var positions = StructureStore.GetPositions();
            positions.Insert(0, new Position { Name = "" });
            RatePositions = positions.Select(x => x.Name).ToArray();
            

            RateFindButtonCommand = new RelayCommand(FindRatesByConditions);
            RateClearButtonCommand = new RelayCommand(RateClearConditionFields);
            RateAddButtonCommand = new RelayCommand(AddNewRate);

            SubjectDepartments = departments.Select(x => x.ShortName).ToArray();

            SubjectFindButtonCommand = new RelayCommand(FindSubjectsByConditions);
            SubjectClearButtonCommand = new RelayCommand(SubjectClearConditionFields);
            SubjectAddButtonCommand = new RelayCommand(AddNewSubject);
        }

        private void FindEmployeesByConditions(object obj)
        {
            if (String.IsNullOrEmpty(EmployeeFindConditionSurname) && String.IsNullOrEmpty(EmployeeFindConditionName) 
                && String.IsNullOrEmpty(EmployeeFindConditionSecondname) && String.IsNullOrEmpty(EmployeeFindConditionPhone) 
                && String.IsNullOrEmpty(EmployeeFindConditionGender))
            {
                MessageBox.Show("All the fields is empty!","Error");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            { 
                var query = _dbContext.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(EmployeeFindConditionSurname))
                {
                    query = query.Where(employee => employee.Surname.Contains(EmployeeFindConditionSurname));
                }

                if (!string.IsNullOrEmpty(EmployeeFindConditionName))
                {
                    query = query.Where(employee => employee.Name.Contains(EmployeeFindConditionName));
                }

                if (!string.IsNullOrEmpty(EmployeeFindConditionSecondname))
                {
                    query = query.Where(employee => employee.Secondname.Contains(EmployeeFindConditionSecondname));
                }
                if (!string.IsNullOrEmpty(EmployeeFindConditionPhone))
                {
                    query = query.Where(employee => employee.PassportId.Contains(EmployeeFindConditionPhone));
                }
                if (!string.IsNullOrEmpty(EmployeeFindConditionGender))
                {
                    bool gender = false;
                    if (EmployeeFindConditionGender == "Чоловіча")
                    {
                        gender = false;
                    }
                    else if (EmployeeFindConditionGender == "Жіноча")
                    {
                        gender = true;
                    }
                    query = query.Where(student => student.Gender == gender);
                }

                Employees = query.Where(x => _dbContext.EmployeeRates.Where(x => x.Department.Id == StructureStore.GetDepartment().Id).Select(x => x.Employee).Contains(x)).ToList();
            }
            OnPropertyChanged("Employees");
        }

        private void EmployeeClearConditionFields(object obj)
        {
            EmployeeFindConditionSurname = "";
            OnPropertyChanged("EmployeeFindConditionSurname");
            EmployeeFindConditionName = "";
            OnPropertyChanged("EmployeeFindConditionName");
            EmployeeFindConditionSecondname = "";
            OnPropertyChanged("EmployeeFindConditionSecondname");
            EmployeeFindConditionPhone = "";
            OnPropertyChanged("EmployeeFindConditionPhone");
            EmployeeFindConditionGender = "";
            OnPropertyChanged("EmployeeFindConditionGender");
        }

        private void FindRatesByConditions(object obj)
        {
            if (String.IsNullOrEmpty(RateFindConditionSurname) && String.IsNullOrEmpty(RateFindConditionName)
                && RateFindConditionDepartment == null && String.IsNullOrEmpty(RateFindConditionPosition)
                && String.IsNullOrEmpty(RateFindConditionRateFrom) && String.IsNullOrEmpty(RateFindConditionRateTo)
                && RateFindConditionStartWork == DateTime.MinValue && RateFindConditionFinishedWork == DateTime.MinValue)
            {
                MessageBox.Show("Всі поля пусті!", "Помилка");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.EmployeeRates.AsQueryable();

                if (!string.IsNullOrEmpty(RateFindConditionSurname))
                {
                    query = query.Where(rate => rate.Employee.Surname.Contains(RateFindConditionSurname));
                }
                if (!string.IsNullOrEmpty(RateFindConditionName))
                {
                    query = query.Where(rate => rate.Employee.Name.Contains(RateFindConditionName));
                }

                if (!string.IsNullOrEmpty(RateFindConditionDepartment))//TODO: Add the space check
                {
                    query = query.Where(rate => rate.Department.ShortName == RateFindConditionDepartment);
                }
                if (!string.IsNullOrEmpty(RateFindConditionPosition))
                {
                    query = query.Where(rate => rate.Position.Name == RateFindConditionPosition);
                }
                if(!string.IsNullOrEmpty(RateFindConditionRateFrom) && !string.IsNullOrEmpty(RateFindConditionRateTo))
                {
                    query = query.Where(rate => rate.Rate >= Convert.ToDouble(RateFindConditionRateFrom)
                    && Convert.ToDouble(RateFindConditionRateTo) >= rate.Rate);
                }
                else if (!string.IsNullOrEmpty(RateFindConditionRateFrom))
                {
                    query = query.Where(rate => rate.Rate >= Convert.ToDouble(RateFindConditionRateFrom));
                }
                else if (!string.IsNullOrEmpty(RateFindConditionRateTo))
                {
                    query = query.Where(rate => rate.Rate >= Convert.ToDouble(RateFindConditionRateTo));
                }

                if (RateFindConditionStartWork != DateTime.MinValue)
                {
                    query = query.Where(rate => rate.StartWork == RateFindConditionStartWork);
                }
                if (RateFindConditionStartWork != DateTime.MinValue)
                {
                    query = query.Where(rate => rate.FinishedWork == RateFindConditionFinishedWork);
                }
                EmployeeRates = query.Where(x => x.Department.Id == StructureStore.GetDepartment().Id)
                    .Include(x => x.Employee)
                    .Include(x => x.Department)
                    .Include(x => x.Position)
                    .ToList();
            }
            OnPropertyChanged("EmployeeRates");
        }

        private void RateClearConditionFields(object obj)
        {
            RateFindConditionSurname = "";
            OnPropertyChanged("RateFindConditionSurname");
            RateFindConditionName = "";
            OnPropertyChanged("RateFindConditionName");
            RateFindConditionDepartment = "";
            OnPropertyChanged("RateFindConditionDepartment");
            RateFindConditionRateFrom = "";
            OnPropertyChanged("RateFindConditionRateFrom");
            RateFindConditionRateTo = "";
            OnPropertyChanged("RateFindConditionRateTo");
            RateFindConditionPosition = "";
            OnPropertyChanged("RateFindConditionPosition");
            RateFindConditionStartWork = DateTime.MinValue;
            OnPropertyChanged("RateFindConditionStartWork");
            RateFindConditionFinishedWork = DateTime.MinValue;
            OnPropertyChanged("RateFindConditionFinishedWork");
        }
        private void AddNewRate(object obj)
        {
            EmployeeRateInfoChangeView changeView = new();
            changeView.Show();
        }


        private void FindSubjectsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(StatementFindConditionSemester) && StatementFindConditionDepartment == null
                && String.IsNullOrEmpty(StatementFindConditionSubject) && String.IsNullOrEmpty(StatementFindConditionECTS)
                && String.IsNullOrEmpty(StatementFindConditionExam) && String.IsNullOrEmpty(StatementFindConditionCredit)
                && String.IsNullOrEmpty(StatementFindConditionCP) && String.IsNullOrEmpty(StatementFindConditionCGW)
                && String.IsNullOrEmpty(StatementFindConditionDiploma))
            {
                MessageBox.Show("Всі поля пусті!", "Помилка");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Statements.AsQueryable();

                if (!string.IsNullOrEmpty(StatementFindConditionSemester))
                {
                    if (StatementFindConditionSemester.All(char.IsDigit))
                    {
                        query = query.Where(statements => statements.Semester.ToString() == StatementFindConditionSemester);
                    }
                    else
                    {
                        MessageBox.Show("Semester contains letter!", "Error");
                    }
                }

                if (!string.IsNullOrEmpty(StatementFindConditionDepartment))//TODO: Add the space check
                {
                    query = query.Where(statements => statements.Subject.Department.ShortName == StatementFindConditionDepartment);
                }

                if (!string.IsNullOrEmpty(StatementFindConditionSubject))
                {
                    query = query.Where(statements => statements.Subject.SubjectBank.ShortName == StatementFindConditionSubject);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionECTS))
                { 
                    int.TryParse(StatementFindConditionECTS, out int ECTS);
                    query = query.Where(statements => statements.Subject.ECTS == ECTS);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionExam))
                {
                    bool exam = false;
                    if (StatementFindConditionExam == "Yes")
                    {
                        exam = true;
                    }
                    else if (StatementFindConditionExam == "No")
                    {
                        exam = false;
                    }
                    query = query.Where(statements => statements.Subject.Exam == exam);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCredit))
                {
                    bool credit = false;
                    if (StatementFindConditionCredit == "Yes")
                    {
                        credit = true;
                    }
                    else if (StatementFindConditionCredit == "No")
                    {
                        credit = false;
                    }
                    query = query.Where(statements => statements.Subject.Credit == credit);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCP))
                {
                    bool cp = false;
                    if (StatementFindConditionCP == "Yes")
                    {
                        cp = true;
                    }
                    else if (StatementFindConditionCP == "No")
                    {
                        cp = false;
                    }
                    query = query.Where(statements => statements.Subject.CourseProject == cp);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCGW))
                {
                    bool cgw = false;
                    if (StatementFindConditionCGW == "Yes")
                    {
                        cgw = true;
                    }
                    else if (StatementFindConditionCGW == "No")
                    {
                        cgw = false;
                    }
                    query = query.Where(statements => statements.Subject.ComputationalGraphicWork == cgw);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionDiploma))
                {
                    bool diploma = false;
                    if (StatementFindConditionDiploma == "Yes")
                    {
                        diploma = true;
                    }
                    else if (StatementFindConditionDiploma == "No")
                    {
                        diploma = false;
                    }
                    query = query.Where(statements => statements.Subject.Diploma == diploma);
                }

                Statements = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Subject)
                    .Include(x => x.Subject.Department)
                    .Include(x => x.Subject.SubjectBank)
                    .ToList();
            }
            OnPropertyChanged("Statements");
        }

        private void SubjectClearConditionFields(object obj)
        {
            StatementFindConditionSemester = "";
            OnPropertyChanged("StatementFindConditionSemester");
            StatementFindConditionDepartment = "";
            OnPropertyChanged("StatementFindConditionDepartment");
            StatementFindConditionSubject = "";
            OnPropertyChanged("StatementFindConditionSubject");
            StatementFindConditionExam = "";
            OnPropertyChanged("StatementFindConditionExam");
            StatementFindConditionCredit = "";
            OnPropertyChanged("StatementFindConditionCredit");
            StatementFindConditionCP = "";
            OnPropertyChanged("StatementFindConditionCP");
            StatementFindConditionCGW = "";
            OnPropertyChanged("StatementFindConditionCGW");
            StatementFindConditionDiploma = "";
            OnPropertyChanged("StatementFindConditionDiploma");
        }
        private void AddNewSubject(object obj)
        {
            StatementInfoChangeView changeView = new();
            changeView.Show();
        }

        private void StatementUpdateSubjects()
        {
            if (StatementFindConditionDepartment == null || StatementFindConditionDepartment == "") return;
            Department department = StructureStore.GetDepartments().Where(x => x.ShortName == StatementFindConditionDepartment).FirstOrDefault();
            StatementSubjects = StructureStore.GetSubjects().Where(x => x.Department.Id == department.Id).Select(x => x.SubjectBank.ShortName).ToArray();
            OnPropertyChanged("StatementSubjects");
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
