using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Services;

namespace Restaurant
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
                MessageBox.Show("Lá»—i táº£i dá»¯ liá»‡u: " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Vui lÃ²ng nháº­p Ä‘á»§ Username vÃ  Password!", "Cáº£nh bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                MessageBox.Show("ThÃªm tÃ i khoáº£n thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
                btnClear_Click(this, new RoutedEventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i thÃªm má»›i (CÃ³ thá»ƒ trÃ¹ng Username): " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lÃ²ng chá»n má»™t tÃ i khoáº£n tá»« danh sÃ¡ch Ä‘á»ƒ cáº­p nháº­t!", "Cáº£nh bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                MessageBox.Show("Cáº­p nháº­t thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i cáº­p nháº­t: " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lÃ²ng chá»n má»™t tÃ i khoáº£n Ä‘á»ƒ KhÃ³a/Má»Ÿ!", "Cáº£nh bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Báº¡n cÃ³ cháº¯c muá»‘n thay Ä‘á»•i tráº¡ng thÃ¡i cá»§a tÃ i khoáº£n {selectedAcc.Username}?",
                                          "XÃ¡c nháº­n", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    selectedAcc.Status = (selectedAcc.Status == 1) ? 0 : 1;
                    accountService.UpdateAccount(selectedAcc);

                    string mess = selectedAcc.Status == 1 ? "ÄÃ£ Má»ž KHÃ“A tÃ i khoáº£n!" : "ÄÃ£ KHÃ“A tÃ i khoáº£n!";
                    MessageBox.Show(mess, "ThÃ´ng bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadData();
                    btnClear_Click(this, new RoutedEventArgs());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lá»—i khi Ä‘á»•i tráº¡ng thÃ¡i: " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
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
