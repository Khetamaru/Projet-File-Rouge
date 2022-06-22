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
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void OpenChangeNamePopUp(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).OpenChangeNamePopUp(); }
        private void ChangeNameYesButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ChangeNameYesButton(); }
        private void ChangeNameNoButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ChangeNameNoButton(); }

        private void OpenResetPasswordPopUp(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).OpenResetPasswordPopUp(); }
        private void ResetPasswordYesButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ResetPasswordYesButton(); }
        private void ResetPasswordNoButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ResetPasswordNoButton(); }

        private void OpenChangeAccessLevelPopUp(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).OpenChangeAccessLevelPopUp(); }
        private void ChangeAccessLevelYesButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ChangeAccessLevelYesButton(); }
        private void ChangeAccessLevelNoButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).ChangeAccessLevelNoButton(); }

        private void OpenDesablePopUp(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).OpenDesablePopUp(); }
        private void DesableYesButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).DesableYesButton(); }
        private void DesableNoButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).DesableNoButton(); }

        private void OpenUnablePopUp(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).OpenUnablePopUp(); }
        private void UnableYesButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).UnableYesButton(); }
        private void UnableNoButton(object sender, RoutedEventArgs e) { (DataContext as UserViewModel).UnableNoButton(); }
    }
}
