using System.Windows;
using System.Windows.Controls;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for RedWireView.xaml
    /// </summary>
    public partial class FolderPurgeView : UserControl
    {
        public FolderPurgeView()
        {
            InitializeComponent();
        }

        private void PasswordBox_FTPPasswordField(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { ((dynamic)DataContext as FolderPurgeViewModel).FTPPasswordField = ((PasswordBox)sender).Password; }
        }

        private void LaunchPurge(object sender, RoutedEventArgs e)
        {
            (DataContext as FolderPurgeViewModel).LaunchPurge();
        }

        private void FTPYesButton(object sender, RoutedEventArgs e) { (DataContext as FolderPurgeViewModel).FTPYesButton(); }
        private void FTPNoButton(object sender, RoutedEventArgs e) { (DataContext as FolderPurgeViewModel).FTPNoButton(); }
    }
}
