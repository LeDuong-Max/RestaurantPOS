using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public partial class FoodItemWindow : Window
    {
        private IFoodItemService foodService = new FoodItemService();
        private ICategoriesService categoryService = new CategoriesService();

        public FoodItemWindow()
        {
            InitializeComponent();
            LoadCategories(); // Phải load danh mục vào ComboBox trước
            LoadFoodItems();
        }

        private void LoadCategories()
        {
            cbCategory.ItemsSource = categoryService.GetCategories();
        }

        private void LoadFoodItems()
        {
            dgFoodItems.ItemsSource = foodService.GetFoodItems();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng điền đủ thông tin và chọn danh mục!", "Cảnh báo");
                return;
            }

            try
            {
                FoodItem newItem = new FoodItem
                {
                    FoodName = txtFoodName.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    CategoryId = (int)cbCategory.SelectedValue,
                    IsAvailable = chkIsAvailable.IsChecked ?? true
                };

                foodService.AddFoodItem(newItem);
                MessageBox.Show("Thêm món ăn thành công!");
                LoadFoodItems();
                btnClear_Click(null, null); // Tự động làm mới ô nhập
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Giá tiền phải là số hợp lệ. " + ex.Message, "Lỗi");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodId.Text) || string.IsNullOrWhiteSpace(txtFoodName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn và điền đầy đủ thông tin!", "Cảnh báo");
                return;
            }

            try
            {
                FoodItem updateItem = new FoodItem
                {
                    FoodId = int.Parse(txtFoodId.Text),
                    FoodName = txtFoodName.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    CategoryId = (int)cbCategory.SelectedValue,
                    IsAvailable = chkIsAvailable.IsChecked ?? true
                };

                foodService.UpdateFoodItem(updateItem);
                MessageBox.Show("Cập nhật thành công!");
                LoadFoodItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: Giá tiền phải là số hợp lệ. " + ex.Message, "Lỗi");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodId.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa!", "Cảnh báo");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foodService.DeleteFoodItem(int.Parse(txtFoodId.Text));
                MessageBox.Show("Xóa thành công!");
                LoadFoodItems();
                btnClear_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFoodId.Clear();
            txtFoodName.Clear();
            txtPrice.Clear();
            cbCategory.SelectedItem = null;
            chkIsAvailable.IsChecked = true; // Trả về mặc định
            dgFoodItems.SelectedItem = null;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgFoodItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFoodItems.SelectedItem is FoodItem selectedItem)
            {
                txtFoodId.Text = selectedItem.FoodId.ToString();
                txtFoodName.Text = selectedItem.FoodName;
                txtPrice.Text = selectedItem.Price.ToString("0.##"); // Bỏ số 0 vô nghĩa ở đuôi
                cbCategory.SelectedValue = selectedItem.CategoryId; // Tự động chọn đúng Danh mục trên ComboBox
                chkIsAvailable.IsChecked = selectedItem.IsAvailable;
            }
        }
    }
}
