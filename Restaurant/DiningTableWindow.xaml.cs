using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public partial class DiningTableWindow : Window
    {
        private IDiningTableService tableService = new DiningTableService();

        public DiningTableWindow()
        {
            InitializeComponent();
            LoadTables();
        }

        private void LoadTables()
        {
            dgTables.ItemsSource = tableService.GetDiningTables();
        }

        // Hàm hỗ trợ lấy Status (0 hoặc 1) từ ComboBox
        private int GetSelectedStatus()
        {
            if (cbStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                return int.Parse(selectedItem.Tag.ToString());
            }
            return 0; // Mặc định là 0 (Bàn trống)
        }

        // Hàm hỗ trợ gán Status lên ComboBox
        private void SetSelectedStatus(int status)
        {
            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (int.Parse(item.Tag.ToString()) == status)
                {
                    cbStatus.SelectedItem = item;
                    break;
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableName.Text) || cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập tên bàn và chọn trạng thái!", "Cảnh báo");
                return;
            }

            DiningTable newTable = new DiningTable
            {
                TableName = txtTableName.Text,
                Status = GetSelectedStatus()
            };

            tableService.AddDiningTable(newTable);
            MessageBox.Show("Thêm bàn mới thành công!");
            LoadTables();
            btnClear_Click(null, null);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableId.Text) || string.IsNullOrWhiteSpace(txtTableName.Text) || cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn bàn và điền đầy đủ thông tin!", "Cảnh báo");
                return;
            }

            DiningTable updateTable = new DiningTable
            {
                TableId = int.Parse(txtTableId.Text),
                TableName = txtTableName.Text,
                Status = GetSelectedStatus()
            };

            tableService.UpdateDiningTable(updateTable);
            MessageBox.Show("Cập nhật bàn thành công!");
            LoadTables();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableId.Text))
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Cảnh báo");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bàn này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                tableService.DeleteDiningTable(int.Parse(txtTableId.Text));
                MessageBox.Show("Xóa thành công!");
                LoadTables();
                btnClear_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtTableId.Clear();
            txtTableName.Clear();
            cbStatus.SelectedItem = null;
            dgTables.SelectedItem = null;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTables.SelectedItem is DiningTable selectedTable)
            {
                txtTableId.Text = selectedTable.TableId.ToString();
                txtTableName.Text = selectedTable.TableName;
                SetSelectedStatus(selectedTable.Status);
            }
        }
    }
}
