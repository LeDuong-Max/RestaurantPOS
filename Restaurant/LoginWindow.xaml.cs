using Restaurant;
using Services;
using System.Windows;
using BusinessObject;

namespace Restaurant
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
                    MessageBox.Show("Vui lÃ²ng nháº­p Ä‘áº§y Ä‘á»§ tÃªn Ä‘Äƒng nháº­p vÃ  máº­t kháº©u.");
                    return;
                }

                Account? account = iAccountService.GetAccount(username);
                if (account != null && account.Password != null && account.Password.Equals(password) && account.Status == 1)
                {
                    AppSession.CurrentUser = account;
                    MainWindow mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    if (account != null && account.Status == 0)
                    {
                        MessageBox.Show("Báº¡n khÃ´ng cÃ²n quyá»n Ä‘Äƒng nháº­p!");
                    }
                    else
                    {
                        MessageBox.Show("Sai tÃ i khoáº£n hoáº·c máº­t kháº©u.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lá»—i káº¿t ná»‘i cÆ¡ sá»Ÿ dá»¯ liá»‡u hoáº·c há»‡ thá»‘ng:\n{ex.Message}");
            }
        }
    }
}
