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
    public partial class MissingCallSendView : UserControl
    {
        public MissingCallSendView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            MissingCall missingCall = (sender as DataGrid).SelectedItem as MissingCall;
            if (DataContext != null)
            {
                ((dynamic)DataContext as MissingCallSendViewModel).NavigateMissingCallSendDetailsCommand.LoadMissingCall(missingCall);
                ((dynamic)DataContext as MissingCallSendViewModel).NavigateMissingCallSendDetailsCommand.Execute(null);
            }
        }
    }
}
