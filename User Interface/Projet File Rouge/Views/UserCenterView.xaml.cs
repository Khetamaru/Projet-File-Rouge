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
    public partial class UserCenterView : UserControl
    {
        public UserCenterView()
        {
            InitializeComponent();
        }

        private void GoDetails(object sender, MouseButtonEventArgs e)
        {
            User user = (sender as DataGrid).SelectedItem as User;
            if (DataContext != null && user != null &&
               ((int)user.UserLevel < (int)User.AccessLevel.Admin ||
               user.Id == ((dynamic)DataContext as UserCenterViewModel).ActualUser.Id))
            {
                ((dynamic)DataContext as UserCenterViewModel).NavigateUserCommand.LoadUser(user);
            }
        }
    }
}
