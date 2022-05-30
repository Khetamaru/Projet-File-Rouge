using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for RedWireView.xaml
    /// </summary>
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();
        }

        private void HyperlinkRequest(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.AbsoluteUri.ToString();

            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else
                {
                    throw;
                }
            }
        }

        private void OpenCancelCommandPopUp(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).OpenCancelCommandPopUp(); }
        private void CancelCommandYesButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CancelCommandYesButton(); }
        private void CancelCommandNoButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CancelCommandNoButton(); }

        private void OpenCommandDonePopUp(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).OpenCommandDonePopUp(); }
        private void CommandDoneYesButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CommandDoneYesButton(); }
        private void CommandDoneNoButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CommandDoneNoButton(); }

        private void OpenCommandArrivedPopUp(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).OpenCommandArrivedPopUp(); }
        private void CommandArrivedYesButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CommandArrivedYesButton(); }
        private void CommandArrivedNoButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).CommandArrivedNoButton(); }

        private void OpenDeliveryDateUpdatePopUp(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).OpenDeliveryDateUpdatePopUp(); }
        private void DeliveryDateUpdateYesButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).DeliveryDateUpdateYesButton(); }
        private void DeliveryDateUpdateNoButton(object sender, RoutedEventArgs e) { (DataContext as CommandViewModel).DeliveryDateUpdateNoButton(); }
    }
}
