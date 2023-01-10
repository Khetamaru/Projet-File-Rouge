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
    public partial class DocumentCenterView : UserControl
    {
        public DocumentCenterView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            DocumentList Document = (sender as DataGrid).SelectedItem as DocumentList;
            ((dynamic)DataContext as DocumentCenterViewModel).NavigateDocumentCommand.SetSaleDocumentCacheInfo(Document.Document, Document.Id);
            ((dynamic)DataContext as DocumentCenterViewModel).NavigateDocumentCommand.Execute(null);
        }
    }
}
