using System.Windows;
using System.Windows.Controls;

using BusinessObject;
using Restaurant;
using Services;

namespace WPF
{
    public partial class ManagerAccountWindow : Window
    {
        private readonly IAccountService accountService;

        public ManagerAccountWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
            LoadData();
        }

        // 1. HÀM TẢI DỮ LIỆU LÊN BẢNG VÀ COMBOBOX
        private void LoadData()
        {
            try
            {
                // Đổ dữ liệu vào DataGrid
                dgAccounts.ItemsSource = accountService.GetAllAccount();

                // Đổ dữ liệu Chức vụ vào ComboBox
                using var db = new RestaurantPosContext();
                cbRole.ItemsSource = db.Roles.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 2. SỰ KIỆN CLICK VÀO BẢNG -> BẮN DỮ LIỆU SANG FORM
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

                // Khóa ô Username lại không cho sửa (Vì Username là Khóa chính/Định danh)
                txtUsername.IsEnabled = false;
            }
        }

        // 3. NÚT LÀM MỚI FORM (XÓA TRẮNG CÁC Ô NHẬP)
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtStatus.Text = "1"; // Mặc định tài khoản mới là 1 (Hoạt động)
            cbRole.SelectedIndex = -1;

            // Mở lại ô Username cho phép nhập mới
            txtUsername.IsEnabled = true;

            // Bỏ chọn dưới DataGrid
            dgAccounts.SelectedItem = null;
        }

        // 4. NÚT THÊM MỚI TÀI KHOẢN
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Vui lòng nhập đủ Username và Password!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tạo một object Account mới để nhét dữ liệu từ Form vào
                Account newAcc = new Account
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Password,
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Role = Convert.ToInt32(cbRole.SelectedValue), // Lấy ID của Role từ ComboBox
                    Status = 1 // Mặc định tạo mới là đang hoạt động
                };

                // Gọi DAO/Service để lưu xuống DB (Chỗ này mày phải đảm bảo trong AccountService đã có hàm AddAccount)
                using var db = new RestaurantPosContext();
                db.Accounts.Add(newAcc);
                db.SaveChanges();

                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(); // Tải lại bảng
                btnClear_Click(null, null); // Xóa trắng form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm mới (Có thể trùng Username): " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 5. NÚT CẬP NHẬT THÔNG TIN
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản từ danh sách để cập nhật!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Cập nhật lại các trường dữ liệu
                selectedAcc.FullName = txtFullName.Text.Trim();
                selectedAcc.Email = txtEmail.Text.Trim();
                selectedAcc.Role = Convert.ToInt32(cbRole.SelectedValue);

                // Trạng thái ép kiểu an toàn
                if (int.TryParse(txtStatus.Text, out int status))
                    selectedAcc.Status = status;

                // Nếu có gõ pass mới thì cập nhật, không thì giữ pass cũ
                if (!string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    selectedAcc.Password = txtPassword.Password;
                }

                accountService.UpdateAccount(selectedAcc); // Hàm update nãy mày viết rồi

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 6. NÚT KHÓA / MỞ TÀI KHOẢN (SOFT DELETE)
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccounts.SelectedItem is not Account selectedAcc)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để Khóa/Mở!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Hỏi lại cho chắc chắn
            var confirm = MessageBox.Show($"Bạn có chắc muốn thay đổi trạng thái của tài khoản {selectedAcc.Username}?",
                                          "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    // Đảo trạng thái: Nếu đang 1 thì thành 0, đang 0 thì thành 1
                    selectedAcc.Status = (selectedAcc.Status == 1) ? 0 : 1;

                    accountService.UpdateAccount(selectedAcc);

                    string mess = selectedAcc.Status == 1 ? "Đã MỞ KHÓA tài khoản!" : "Đã KHÓA tài khoản!";
                    MessageBox.Show(mess, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadData();
                    btnClear_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đổi trạng thái: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner is MainWindow mainWindow)
            {
                mainWindow.Show();
                this.Close();
            }
        }
    }
}