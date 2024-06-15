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
    internal class DeansOfficeVM : INotifyPropertyChanged
    {
        public List<Group> StudentGroups { get; set; }
        public List<Speciality> StudentSpecialities { get; set; }
        public List<Student> Students { get; set; }

        public string StudentFindConditionSurname { get; set; }
        public string StudentFindConditionName { get; set; }
        public string StudentFindConditionSecondname { get; set; }
        public string StudentFindConditionPassport { get; set; }
        public string StudentFindConditionGroup { get; set; }
        public string StudentFindConditionSpeciality { get; set; }
        public string StudentFindConditionGender { get; set; }
        public string StudentFindConditionStudentId { get; set; }
        public string StudentFindConditionStudyLevel { get; set; }
        public string StudentFindConditionStudyForm { get; set; }

        public string[] StudentStudyLevels { get; set; }
        public string[] StudentStudyForms { get; set; }

        public ICommand StudentFindButtonCommand { get; set; }
        public ICommand StudentClearButtonCommand { get; set; }
        public ICommand StudentAddButtonCommand { get; set; }
       




        public List<Group> Groups { get; set; }
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

        public string OrderFindConditionNumber { get; set; }
        public string OrderFindConditionType { get; set; }
        public string OrderFindConditionStatus { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }

        public List<Order> Orders { get; set; }
        public string[] OrderTypes { get; set; }
        public ICommand OrderFindButtonCommand { get; set; }
        public ICommand OrderClearButtonCommand { get; set; }
        public ICommand OrderAddButtonCommand { get; set; }


        public DeansOfficeVM(int facultyId) 
        {
            StudentGroups = StructureStore.GetGroups().Where(x => x?.Faculty?.Id == facultyId).ToList();
            StudentGroups.Insert(0,new Group { Name = "" });
            StudentSpecialities = StructureStore.GetSpecialities().Where(x => x.Faculty.Id == facultyId).ToList();
            StudentSpecialities.Insert(0,new Speciality { ShortName = "" });

            StudentStudyLevels = StructureStore.GetStudyLevels();
            StudentStudyForms = StructureStore.GetStudyForms();

            StudentFindButtonCommand = new RelayCommand(FindStudentsByConditions);
            StudentClearButtonCommand = new RelayCommand(StudentClearConditionFields);
            StudentAddButtonCommand = new RelayCommand(AddNewStudent);
            


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


            OrderTypes = StructureStore.GetOrderTypes();
            OrderFindButtonCommand = new RelayCommand(FindOrdersByConditions);
            OrderClearButtonCommand = new RelayCommand(OrderClearConditionFields);
            OrderAddButtonCommand = new RelayCommand(AddNewOrder);


        }

        private void FindStudentsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(StudentFindConditionSurname) && String.IsNullOrEmpty(StudentFindConditionName) 
                && String.IsNullOrEmpty(StudentFindConditionSecondname) && String.IsNullOrEmpty(StudentFindConditionGroup) 
                && String.IsNullOrEmpty(StudentFindConditionSpeciality) && String.IsNullOrEmpty(StudentFindConditionGender)
                && String.IsNullOrEmpty(StudentFindConditionStudentId) && String.IsNullOrEmpty(StudentFindConditionPassport)
                && String.IsNullOrEmpty(StudentFindConditionStudyLevel) && String.IsNullOrEmpty(StudentFindConditionStudyForm))
            {
                MessageBox.Show("All the fields is empty!","Error");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            { 
                var query = _dbContext.Students.AsQueryable();

                if (!string.IsNullOrEmpty(StudentFindConditionSurname))
                {
                    query = query.Where(student => student.Surname.Contains(StudentFindConditionSurname));
                }

                if (!string.IsNullOrEmpty(StudentFindConditionName))
                {
                    query = query.Where(student => student.Name.Contains(StudentFindConditionName));
                }

                if (!string.IsNullOrEmpty(StudentFindConditionSecondname))
                {
                    query = query.Where(student => student.Secondname.Contains(StudentFindConditionSecondname));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionPassport))
                {
                    query = query.Where(student => student.PassportId.Contains(StudentFindConditionPassport));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionGroup))
                {
                    query = query.Where(student => student.Group.Name.Contains(StudentFindConditionGroup));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionSpeciality))
                {
                    query = query.Where(student => student.Speciality.ShortName.Contains(StudentFindConditionSpeciality));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionGender))
                {
                    bool gender = false;
                    if (StudentFindConditionGender == "Чоловіча")
                    {
                        gender = false;
                    }
                    else if (StudentFindConditionGender == "Жіноча")
                    {
                        gender = true;
                    }
                    query = query.Where(student => student.Gender == gender);
                }

                if (!string.IsNullOrEmpty(StudentFindConditionStudentId))
                {
                    if (StudentFindConditionStudentId.All(char.IsDigit))
                    {
                        query = query.Where(student => student.StudentId.ToString() == StudentFindConditionStudentId);
                    }
                    else
                    {
                        MessageBox.Show("Student ID contains letter!", "Error");
                    }
                }
                if (!string.IsNullOrEmpty(StudentFindConditionStudyLevel))
                {
                    query = query.Where(student => student.StudyLevel == Array.IndexOf(StudentStudyLevels, StudentFindConditionStudyLevel));
                }
                if (!string.IsNullOrEmpty(StudentFindConditionStudyForm))
                {
                    query = query.Where(student => student.StudyForm == Array.IndexOf(StudentStudyForms, StudentFindConditionStudyForm));
                }

                Students = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Group)
                    .Include(x => x.Speciality)
                    .ToList();
            }
            OnPropertyChanged("Students");
        }

        private void StudentClearConditionFields(object obj)
        {
            StudentFindConditionSurname = "";
            OnPropertyChanged("StudentFindConditionSurname");
            StudentFindConditionName = "";
            OnPropertyChanged("StudentFindConditionName");
            StudentFindConditionSecondname = "";
            OnPropertyChanged("StudentFindConditionSecondname");
            StudentFindConditionGroup = "";
            OnPropertyChanged("StudentFindConditionGroup");
            StudentFindConditionSpeciality = "";
            OnPropertyChanged("StudentFindConditionSpeciality");
            StudentFindConditionGender = "";
            OnPropertyChanged("StudentFindConditionGender");
            StudentFindConditionStudentId = "";
            OnPropertyChanged("StudentFindConditionStudentId");
            StudentFindConditionPassport = "";
            OnPropertyChanged("StudentFindConditionPassport");
            StudentFindConditionStudyLevel = "";
            OnPropertyChanged("StudentFindConditionStudyLevel");
            StudentFindConditionStudyForm = "";
            OnPropertyChanged("StudentFindConditionStudyForm");
        }

        private void AddNewStudent(object obj)
        {
            StudentInfoChangeView changeView = new();
            changeView.Show();
        }

        private void FindGroupsByConditions(object obj)
        {
            if (String.IsNullOrEmpty(GroupFindConditionName) && GroupFindConditionDepartment == null
                && GroupStartEducation == DateTime.MinValue && String.IsNullOrEmpty(GroupFindConditionStudentSurname)
                && String.IsNullOrEmpty(GroupFindConditionStudentName))
            {
                MessageBox.Show("Всі поля пусті!", "Помилка");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Groups.AsQueryable();

                if (!string.IsNullOrEmpty(GroupFindConditionName))
                {
                    query = query.Where(group => group.Name.Contains(GroupFindConditionName));
                }

                if (!string.IsNullOrEmpty(GroupFindConditionDepartment))
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

                Groups = query
                    .Include(x => x.Faculty)
                    .Include(x => x.Department)
                    .ToList();
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
                    if (StatementFindConditionExam == "Так")
                    {
                        exam = true;
                    }
                    else if (StatementFindConditionExam == "Ні")
                    {
                        exam = false;
                    }
                    query = query.Where(statements => statements.Subject.Exam == exam);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCredit))
                {
                    bool credit = false;
                    if (StatementFindConditionCredit == "Так")
                    {
                        credit = true;
                    }
                    else if (StatementFindConditionCredit == "Ні")
                    {
                        credit = false;
                    }
                    query = query.Where(statements => statements.Subject.Credit == credit);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCP))
                {
                    bool cp = false;
                    if (StatementFindConditionCP == "Так")
                    {
                        cp = true;
                    }
                    else if (StatementFindConditionCP == "Ні")
                    {
                        cp = false;
                    }
                    query = query.Where(statements => statements.Subject.CourseProject == cp);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionCGW))
                {
                    bool cgw = false;
                    if (StatementFindConditionCGW == "Так")
                    {
                        cgw = true;
                    }
                    else if (StatementFindConditionCGW == "Ні")
                    {
                        cgw = false;
                    }
                    query = query.Where(statements => statements.Subject.ComputationalGraphicWork == cgw);
                }
                if (!string.IsNullOrEmpty(StatementFindConditionDiploma))
                {
                    bool diploma = false;
                    if (StatementFindConditionDiploma == "Так")
                    {
                        diploma = true;
                    }
                    else if (StatementFindConditionDiploma == "Ні")
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
            StatementSubjects = StructureStore.GetSubjects().Where(x => x.Department.Id == department.Id).Select(x => x.SubjectBank.ShortName).ToArray();
            OnPropertyChanged("StatementSubjects");
        }



        private void FindOrdersByConditions(object obj)
        {
            if (String.IsNullOrEmpty(OrderFindConditionNumber) && String.IsNullOrEmpty(OrderFindConditionType)
                && String.IsNullOrEmpty(OrderFindConditionStatus) && OrderStartDate == DateTime.MinValue
                && OrderEndDate == DateTime.MinValue)
            {
                MessageBox.Show("Всі поля пусті!", "Помилка");
                return;
            }
            using (AppDBContext _dbContext = new AppDBContext())
            {
                var query = _dbContext.Orders.AsQueryable();

                if (!string.IsNullOrEmpty(OrderFindConditionNumber))
                {
                    if (OrderFindConditionNumber.All(char.IsDigit))
                    {
                        query = query.Where(order => order.Number.ToString() == OrderFindConditionNumber);
                    }
                    else
                    {
                        MessageBox.Show("Semester contains letter!", "Error");
                    }
                }

                if (!string.IsNullOrEmpty(OrderFindConditionType))//TODO: Add the space check
                {
                    query = query.Where(order => order.Type == Array.IndexOf(StructureStore.GetOrderTypes(),OrderFindConditionType));
                }

                if (!string.IsNullOrEmpty(OrderFindConditionStatus))
                {
                    int status;
                    if(OrderFindConditionStatus == "Діє")
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }
                    query = query.Where(order => order.Status == status);
                }
                if (OrderStartDate != DateTime.MinValue)
                {
                    query = query.Where(order => order.StartDate == OrderStartDate);
                }
                if (OrderEndDate != DateTime.MinValue)
                {
                    query = query.Where(order => order.EndDate == OrderEndDate);
                }

                Orders = query
                    .ToList();
            }
            OnPropertyChanged("Orders");
        }

        private void OrderClearConditionFields(object obj)
        {
            OrderFindConditionNumber = "";
            OnPropertyChanged("OrderFindConditionNumber");
            OrderFindConditionType = "";
            OnPropertyChanged("OrderFindConditionType");
            OrderFindConditionStatus = "";
            OnPropertyChanged("OrderFindConditionStatus");
            OrderStartDate = DateTime.MinValue;
            OnPropertyChanged("OrderStartDate");
            OrderEndDate = DateTime.MinValue;
            OnPropertyChanged("OrderEndDate");
            
        }
        private void AddNewOrder(object obj)
        {
           OrderChangeView changeView = new OrderChangeView();
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
