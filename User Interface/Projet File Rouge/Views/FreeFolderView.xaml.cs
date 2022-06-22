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
    public partial class FreeFolderView : UserControl
    {
        public FreeFolderView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            RedWire redWire = (sender as DataGrid).SelectedItem as RedWire;
            if (DataContext != null && redWire != null)
            {
                ((dynamic)DataContext as FreeFolderViewModel).NavigateRedWireCommand.LoadRedWire(redWire, PageNameEnum.FreeFolder);
            }
        }

        private void ArrowLeft(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as FreeFolderViewModel).LeftArrow();
            }
        }

        private void ArrowRight(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as FreeFolderViewModel).RightArrow();
            }
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as FreeFolderViewModel).Filter();
        }

        private void AntiFilter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as FreeFolderViewModel).AntiFilter();
        }
    }
}
