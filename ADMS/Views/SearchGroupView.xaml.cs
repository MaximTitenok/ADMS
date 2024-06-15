using ADMS.Models;
using ADMS.ViewModels;
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

namespace ADMS.Views
{
    /// <summary>
    /// Interaction logic for SearchStudentView.xaml
    /// </summary>
    public partial class SearchGroupView : Window
    {
        internal SearchGroupVM SearchGroupVM { get; set; }
        internal SearchGroupView(OrderChangeVM orderChangeVM)
        {
            InitializeComponent();
            SearchGroupVM VM = new(orderChangeVM);
            SearchGroupVM = VM;
            DataContext = VM;
        }
        private void GroupFindRowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Group selectedItem = GroupsGrid.SelectedItem as Group;
                if (selectedItem != null)
                {
                    GroupInfoView groupInfo = new(selectedItem);
                    groupInfo.Show();
                }
                else
                {
                    MessageBox.Show("Invalid row", "Error");
                }
            }
        }
        private void GroupSelectedClick(object sender, SelectedCellsChangedEventArgs e)
        {
            Group selectedItem = GroupsGrid.SelectedItem as Group;
            if (selectedItem != null)
            {
                SearchGroupVM.Group = selectedItem;
            }
            else
            {
                MessageBox.Show("Invalid row", "Error");
            }
        }
        private void SelectButtonClicked(object sender, RoutedEventArgs e)
        {
            if(SearchGroupVM?.Group?.Name != null)
            {
                this.Hide();
            }
        }
        

    }
}
