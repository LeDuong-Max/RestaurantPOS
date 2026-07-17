using System;
using System.Windows;

namespace Restaurant
{
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
            txtEmail.Text = user.Email;
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
            user.Email = txtEmail.Text.Trim();
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
        private void Window_Closed(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }
    }
}
