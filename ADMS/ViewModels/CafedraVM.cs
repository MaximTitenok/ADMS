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
        public string[] GroupDepartments { get; set; }

        public string GroupFindConditionName { get; set; }
        public string GroupFindConditionDepartment { get; set; }
        public DateTime GroupStartEducation { get; set; }
        public string GroupFindConditionStudentSurname { get; set; }
        public string GroupFindConditionStudentName { get; set; }

        public ICommand GroupFindButtonCommand { get; set; }
        public ICommand GroupClearButtonCommand { get; set; }
        public ICommand GroupAddButtonCommand { get; set; }




        public List<Statement> Statements { get; set; }
        public List<Subject> Subjects { get; set; }
        public string[] StatementDepartments { get; set; }
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

        public ICommand StatementFindButtonCommand { get; set; }
        public ICommand StatementClearButtonCommand { get; set; }
        public ICommand StatementAddButtonCommand { get; set; }


        public CafedraVM(int facultyId) 
        {
            EmployeeFindButtonCommand = new RelayCommand(FindEmployeesByConditions);
            EmployeeClearButtonCommand = new RelayCommand(EmployeeClearConditionFields);

            var departments = StructureStore.GetDepartments().Where(x => x.Faculty.Id == facultyId).ToList();
            departments.Insert(0, new Department { ShortName = "" });
            GroupDepartments = departments.Select(x => x.ShortName).ToArray();

            GroupFindButtonCommand = new RelayCommand(FindGroupsByConditions);
            GroupClearButtonCommand = new RelayCommand(GroupClearConditionFields);
            GroupAddButtonCommand = new RelayCommand(AddNewGroup);

            StatementDepartments = departments.Select(x => x.ShortName).ToArray();

            StatementFindButtonCommand = new RelayCommand(FindStatementsByConditions);
            StatementClearButtonCommand = new RelayCommand(StatementClearConditionFields);
            StatementAddButtonCommand = new RelayCommand(AddNewStatement);
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
                    if (EmployeeFindConditionGender == "Male")
                    {
                        gender = false;
                    }
                    else if (EmployeeFindConditionGender == "Female")
                    {
                        gender = true;
                    }
                    query = query.Where(student => student.Gender == gender);
                }

                Employees = query.ToList();
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

        private void FindGroupsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(GroupFindConditionName) && GroupFindConditionDepartment == null
                && GroupStartEducation == DateTime.MinValue && String.IsNullOrEmpty(GroupFindConditionStudentSurname)
                && String.IsNullOrEmpty(GroupFindConditionStudentName))
            {
                MessageBox.Show("All the fields is empty!", "Error");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Groups.AsQueryable();

                if (!string.IsNullOrEmpty(GroupFindConditionName))
                {
                    query = query.Where(group => group.Name.Contains(GroupFindConditionName));
                }

                if (!string.IsNullOrEmpty(GroupFindConditionDepartment))//TODO: Add the space check
                {
                    query = query.Where(group => group.Department.ShortName == GroupFindConditionDepartment);
                }

                if (GroupStartEducation != DateTime.MinValue)
                {
                    query = query.Where(group => group.StartEducation == GroupStartEducation);
                }
                if (!string.IsNullOrEmpty(GroupFindConditionStudentSurname))
                {
                    query = _dbContext.Students
                        .Where(x => x.Surname.Contains(GroupFindConditionStudentSurname))
                        .Select(x => x.Group)
                        .Intersect(query).AsQueryable();
                }
                if (!string.IsNullOrEmpty(GroupFindConditionStudentName))
                {
                    query = _dbContext.Students
                        .Where(x => x.Name.Contains(GroupFindConditionStudentName))
                        .Select(x => x.Group)
                        .Intersect(query).AsQueryable();
                }

               /* Groups = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Department)
                    .ToList();*/
            }
            OnPropertyChanged("Groups");
        }

        private void GroupClearConditionFields(object obj)
        {
            GroupFindConditionName = "";
            OnPropertyChanged("GroupFindConditionName");
            GroupFindConditionDepartment = "";
            OnPropertyChanged("GroupFindConditionDepartments");
            GroupStartEducation = DateTime.MinValue;
            OnPropertyChanged("GroupStartEducation");
            GroupFindConditionStudentSurname = "";
            OnPropertyChanged("GroupFindConditionStudentSurname");
            GroupFindConditionStudentName = "";
            OnPropertyChanged("GroupFindConditionStudentName");
        }
        private void AddNewGroup(object obj)
        {
            GroupInfoChangeView changeView = new();
            changeView.Show();
        }


        private void FindStatementsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(StatementFindConditionSemester) && StatementFindConditionDepartment == null
                && String.IsNullOrEmpty(StatementFindConditionSubject) && String.IsNullOrEmpty(StatementFindConditionECTS)
                && String.IsNullOrEmpty(StatementFindConditionExam) && String.IsNullOrEmpty(StatementFindConditionCredit)
                && String.IsNullOrEmpty(StatementFindConditionCP) && String.IsNullOrEmpty(StatementFindConditionCGW)
                && String.IsNullOrEmpty(StatementFindConditionDiploma))
            {
                MessageBox.Show("All the fields is empty!", "Error");
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

                if (!string.IsNullOrEmpty(GroupFindConditionDepartment))//TODO: Add the space check
                {
                    query = query.Where(statements => statements.SubjectId.Department.ShortName == StatementFindConditionDepartment);
                }

                if (!string.IsNullOrEmpty(StatementFindConditionSubject))
                {
                    query = query.Where(statements => statements.SubjectId.SubjectBankId.ShortName == StatementFindConditionSubject);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionECTS))
                { 
                    int.TryParse(StatementFindConditionECTS, out int ECTS);
                    query = query.Where(statements => statements.SubjectId.ECTS == ECTS);
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
                    query = query.Where(statements => statements.SubjectId.Exam == exam);
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
                    query = query.Where(statements => statements.SubjectId.Credit == credit);
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
                    query = query.Where(statements => statements.SubjectId.CourseProject == cp);
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
                    query = query.Where(statements => statements.SubjectId.ComputationalGraphicWork == cgw);
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
                    query = query.Where(statements => statements.SubjectId.Diploma == diploma);
                }

                Statements = query
                    .Include(x => x.Faculty)
                    .Include(x => x.SubjectId)
                    .Include(x => x.SubjectId.Department)
                    .Include(x => x.SubjectId.SubjectBankId)
                    .ToList();
            }
            OnPropertyChanged("Statements");
        }

        private void StatementClearConditionFields(object obj)
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
        private void AddNewStatement(object obj)
        {
            StatementInfoChangeView changeView = new();
            changeView.Show();
        }

        private void StatementUpdateSubjects()
        {
            if (StatementFindConditionDepartment == null || StatementFindConditionDepartment == "") return;
            Department department = StructureStore.GetDepartments().Where(x => x.ShortName == StatementFindConditionDepartment).FirstOrDefault();
            StatementSubjects = StructureStore.GetSubjects().Where(x => x.Department.Id == department.Id).Select(x => x.SubjectBankId.ShortName).ToArray();
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
