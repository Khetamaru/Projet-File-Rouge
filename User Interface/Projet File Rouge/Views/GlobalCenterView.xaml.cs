using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for GlobalCenterView.xaml
    /// </summary>
    public partial class GlobalCenterView : UserControl
    {
        public GlobalCenterView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            RedWire redWire = (sender as DataGrid).SelectedItem as RedWire;
            if (DataContext != null && redWire != null)
            {
                ((dynamic)DataContext as GlobalCenterViewModel).NavigateRedWireCommand.LoadRedWire(redWire, PageNameEnum.GlobalCenter);
            }
        }

        private void ArrowLeft(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as GlobalCenterViewModel).LeftArrow();
            }
        }

        private void ArrowRight(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as GlobalCenterViewModel).RightArrow();
            }
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as GlobalCenterViewModel).Filter();
        }

        private void AntiFilter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as GlobalCenterViewModel).AntiFilter();
        }
    }
}
