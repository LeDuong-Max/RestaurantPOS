using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Services;

namespace WPF
{
    public partial class ManagerAccountWindow : Window
    {
        private readonly IAccountService accountService;
        private readonly IRoleService roleService;

        public ManagerAccountWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
            roleService = new RoleService();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                dgAccounts.ItemsSource = accountService.GetAllAccount();
                cbRole.ItemsSource = roleService.GetAllRole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dgAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAccounts.SelectedItem is Account acc)
            {
                txtUsername.Text = acc.Username;
                txtPassword.Password = acc.Password;
                txtFullName.Text = acc.FullName;
                txtEmail.Text = acc.Email;
                txtStatus.Text = acc.Status.ToString();
                cbRole.SelectedValue = acc.Role;

                txtUsername.IsEnabled = false;
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtStatus.Text = "1";
            cbRole.SelectedIndex = -1;

            txtUsername.IsEnabled = true;
            dgAccounts.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Vui lòng nhập đủ Username và Password!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Account newAcc = new Account
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Password,
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Role = Convert.ToInt32(cbRole.SelectedValue),
                    Status = 1
                };
                accountService.CreateAccount(newAcc);

                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                btnClear_Click(this, new RoutedEventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm mới (Có thể trùng Username): " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản từ danh sách để cập nhật!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                selectedAcc.FullName = txtFullName.Text.Trim();
                selectedAcc.Email = txtEmail.Text.Trim();
                selectedAcc.Role = Convert.ToInt32(cbRole.SelectedValue);

                if (int.TryParse(txtStatus.Text, out int status))
                    selectedAcc.Status = status;

                if (!string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    selectedAcc.Password = txtPassword.Password;
                }

                accountService.UpdateAccount(selectedAcc);

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để Khóa/Mở!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn thay đổi trạng thái của tài khoản {selectedAcc.Username}?",
                                          "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    selectedAcc.Status = (selectedAcc.Status == 1) ? 0 : 1;
                    accountService.UpdateAccount(selectedAcc);

                    string mess = selectedAcc.Status == 1 ? "Đã MỞ KHÓA tài khoản!" : "Đã KHÓA tài khoản!";
                    MessageBox.Show(mess, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadData();
                    btnClear_Click(this, new RoutedEventArgs());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đổi trạng thái: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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