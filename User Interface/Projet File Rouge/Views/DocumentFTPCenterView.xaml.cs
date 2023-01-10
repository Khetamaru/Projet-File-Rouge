using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Projet_File_Rouge.Object;
using Projet_File_Rouge.ViewModel;

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for GlobalCenterView.xaml
    /// </summary>
    public partial class DocumentFTPCenterView : UserControl
    {
        public DocumentFTPCenterView()
        {
            InitializeComponent();
        }

        private void HyperlinkRequest(object sender, RequestNavigateEventArgs e)
        {
            (DataContext as DocumentFTPCenterViewModel).HyperlinkRequest(e.Uri.ToString());
        }

        private void PasswordBox_FTPPasswordField(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { ((dynamic)DataContext as DocumentFTPCenterViewModel).FTPPasswordField = ((PasswordBox)sender).Password; }
        }

        private void DeleteDocumentFTP(object sender, RoutedEventArgs e)
        {
            (DataContext as DocumentFTPCenterViewModel).DeleteDocumentFTP(((DocumentFTP)(sender as Button).DataContext).Id);
        }

        private void FTPYesButton(object sender, RoutedEventArgs e) { (DataContext as DocumentFTPCenterViewModel).FTPYesButton(); }
        private void FTPNoButton(object sender, RoutedEventArgs e) { (DataContext as DocumentFTPCenterViewModel).FTPNoButton(); }
    }
}
