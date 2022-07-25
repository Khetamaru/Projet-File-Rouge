using Projet_File_Rouge.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Projet_File_Rouge.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) { ((dynamic)DataContext).PasswordField = ((PasswordBox)sender).Password; }
        }

        private void KeyPressedEvent(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Enter:
                    ((dynamic)DataContext as LoginViewModel).ConnectionCommand.Execute(null);
                    break;
            }
        }
    }
}
