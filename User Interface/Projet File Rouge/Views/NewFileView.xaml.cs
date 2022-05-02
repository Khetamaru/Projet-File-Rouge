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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for NewFileView.xaml
    /// </summary>
    public partial class NewFileView : UserControl
    {
        public NewFileView()
        {
            InitializeComponent();
        }

        public void CMSaleDocument(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) 
            {
                string[] strTab = { "CM" };
                (DataContext as NewFileViewModel).ChargeSaleDocument(sender, e, strTab); 
            }
        }

        public void SupportModel(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as NewFileViewModel).SupportModel = (sender as TextBox).Text; }
        }

        public void SupportState(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as NewFileViewModel).SupportState = (sender as TextBox).Text; }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as NewFileViewModel).Box_checked((sender as CheckBox).Content.ToString()); }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as NewFileViewModel).Box_unchecked((sender as CheckBox).Content.ToString()); }
        }

        private void RedWireCreation(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as NewFileViewModel).RedWireCreation(); }
        }
    }
}
