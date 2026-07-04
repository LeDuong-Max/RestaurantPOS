using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                // Quản lý: Hiển thị full chức năng (Không cần code gì thêm nếu XAML mặc định là hiện)
            }
            else if (user.Role == 2)
            {
                // Thu ngân: Chỗ này sau mày có nút nào cần giấu thì gọi tên nó ra. 
                // Ví dụ: btnQuanLyNhanVien.Visibility = Visibility.Collapsed;
            }
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