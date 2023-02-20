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
    public partial class ParamConfigView : UserControl
    {
        public ParamConfigView()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as ParamConfigViewModel).Box_checked(); }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as ParamConfigViewModel).Box_unchecked(); }
        }

        private void RedWireCreation(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { (DataContext as ParamConfigViewModel).SaveSettings(); }
        }
    }
}
