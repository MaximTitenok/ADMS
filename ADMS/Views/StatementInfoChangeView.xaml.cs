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

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class StatementInfoChangeView : Window
    {
        internal GroupInfoChangeVM GroupInfoChangeVM { get; set; }
        internal StatementInfoChangeView(Statement statement)
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            StatementInfoChangeVM statementInfoChangeVM = new(statement);
            statementInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = statementInfoChangeVM;
        }
        internal StatementInfoChangeView()
        {
            this.ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            StatementInfoChangeVM statementInfoChangeVM = new();
            statementInfoChangeVM.OnRequestClose += (s, e) => this.Close();
            DataContext = statementInfoChangeVM;
        }
        private void StudentRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                StatementMark selectedItem = MarksGrid.SelectedItem as StatementMark;
                if (selectedItem != null)
                {
                    StudentInfoView studentInfoView = new(selectedItem.Student);
                    studentInfoView.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
    }
}
