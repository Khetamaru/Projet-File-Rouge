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
    public partial class CommandCenterView : UserControl
    {
        public CommandCenterView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            CommandList commandList = (sender as DataGrid).SelectedItem as CommandList;
            if (DataContext != null && commandList != null)
            {
                ((dynamic)DataContext as CommandCenterViewModel).NavigateCommandCommand.LoadCommand(commandList);
            }
        }

        private void ArrowLeft(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as CommandCenterViewModel).LeftArrow();
            }
        }

        private void ArrowRight(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic)DataContext as CommandCenterViewModel).RightArrow();
            }
        }
    }
}
