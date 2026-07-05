using System.Windows;
using WPF;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckUserRole();
        }

        private void CheckUserRole()
        {
            var user = AppSession.CurrentUser;
            if (user == null) return;

            if (user.Role == 1)
            {
                btnManagerAccount.Visibility = Visibility.Visible;
            }
            else
            {
                btnManagerAccount.Visibility = Visibility.Collapsed;
            }
        }

        private void btnManagerAccount_Click(object sender, RoutedEventArgs e)
        {
            ManagerAccountWindow manageWindow = new ManagerAccountWindow();
            manageWindow.Owner = this;
            this.Hide();
            manageWindow.ShowDialog();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWin = new ProfileWindow();
            profileWin.Owner = this;
            profileWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            profileWin.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            AppSession.CurrentUser = null;
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}