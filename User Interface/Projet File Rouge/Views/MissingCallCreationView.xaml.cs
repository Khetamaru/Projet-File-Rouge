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
    public partial class MissingCallCreationView : UserControl
    {
        public MissingCallCreationView()
        {
            InitializeComponent();
        }

        public void Caller(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as MissingCallCreationViewModel).Caller = (sender as TextBox).Text; }
        }

        public void Message(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as MissingCallCreationViewModel).Message = (sender as TextBox).Text; }
        }

        private void MissingCallCreation(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as MissingCallCreationViewModel).MissingCallCreation(); }
        }
    }
}
