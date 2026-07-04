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
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for UserInformationWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
            LoadUserInfo();
        }
        private void LoadUserInfo()
        {
            var user = AppSession.CurrentUser;
            if (user == null) return;
            txtUsername.Text = user.Username;
            txtFullName.Text = user.FullName;
            txtRole.Text = user.RoleNavigation?.RoleName ?? "Chưa xác định";
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var user = AppSession.CurrentUser;
            if (user == null) return;
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ và tên không được để trống!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            user.FullName = txtFullName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                user.Password = txtPassword.Password;
            }
            try
            {
                Services.IAccountService accountService = new Services.AccountService();
                accountService.UpdateAccount(user);

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật: {ex.Message}", "Lỗi hệ thống", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
