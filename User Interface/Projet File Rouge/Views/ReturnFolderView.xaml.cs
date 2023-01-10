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
    public partial class ReturnFolderView : UserControl
    {
        public ReturnFolderView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            RedWire redWire = (sender as DataGrid).SelectedItem as RedWire;
            if (DataContext != null && redWire != null)
            {
                ((dynamic)DataContext as ReturnFolderViewModel).NavigateRedWireCommand.LoadRedWire(redWire, PageNameEnum.ReturnFolder);
                ((dynamic)DataContext as ReturnFolderViewModel).NavigateRedWireCommand.Execute(null);
            }
        }

        private void ArrowLeft(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as ReturnFolderViewModel).LeftArrow();
            }
        }

        private void ArrowRight(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as ReturnFolderViewModel).RightArrow();
            }
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as ReturnFolderViewModel).Filter();
        }

        private void AntiFilter(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext as ReturnFolderViewModel).AntiFilter();
        }
    }
}
