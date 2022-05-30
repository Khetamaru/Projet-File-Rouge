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
using Projet_File_Rouge.Object;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for PersoSpaceView.xaml
    /// </summary>
    public partial class PersoSpaceView : UserControl
    {
        public PersoSpaceView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            RedWire redWire = (sender as DataGrid).SelectedItem as RedWire;
            if (DataContext != null && redWire != null)
            {
                ((dynamic)DataContext as PersoSpaceViewModel).NavigateRedWireCommand.LoadRedWire(redWire, PageNameEnum.PersoSpace);
            }
        }

        private void ArrowLeft(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as PersoSpaceViewModel).LeftArrow();
            }
        }

        private void ArrowRight(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as PersoSpaceViewModel).RightArrow();
            }
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as PersoSpaceViewModel).Filter();
        }

        private void AntiFilter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as PersoSpaceViewModel).AntiFilter();
        }
    }
}
