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
                btnDiningTable.Visibility = Visibility.Visible;
            }
            else
            {
                btnManagerAccount.Visibility = Visibility.Collapsed;
                btnDiningTable.Visibility= Visibility.Collapsed;
            }
        }

        private void btnManagerAccount_Click(object sender, RoutedEventArgs e)
        {
            ManagerAccountWindow manageWindow = new ManagerAccountWindow();
            manageWindow.Owner = this;
            this.Hide();
            manageWindow.Show();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWin = new ProfileWindow();
            profileWin.Owner = this;
            this.Hide();
            profileWin.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            AppSession.CurrentUser = null;
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
        private void btnDiningTable_Click(object sender, RoutedEventArgs e)
        {
            DiningTableWindow diningTableWindow = new DiningTableWindow();
            diningTableWindow.Owner = this;
            this.Hide();
            diningTableWindow.Show();
        }
    }
}