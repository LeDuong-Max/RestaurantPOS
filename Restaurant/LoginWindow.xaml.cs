using Restaurant;
using Services;
using System.Windows;
using BusinessObject;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IAccountService iAccountService;
        public Login()
        {
            InitializeComponent();
            iAccountService = new AccountService();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                    return;
                }

                Account account = iAccountService.GetAccount(username);
                if (account != null && account.Password != null && account.Password.Equals(password)&&account.Status==1)
                {
                    AppSession.CurrentUser = account;
                    this.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    if (account.Status == 0)
                    {
                        MessageBox.Show("Bạn không còn quyền đăng nhập!");
                    }
                    else MessageBox.Show("Sai tài khoản hoặc mật khẩu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu hoặc hệ thống:\n{ex.Message}");
            }
        }
    }
}
