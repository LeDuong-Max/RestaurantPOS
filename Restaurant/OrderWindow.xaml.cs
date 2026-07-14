using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObject; 
using Services;      

namespace WPF
{
    public partial class OrderWindow : Window
    {
        private DiningTable currentTable;
        private readonly IFoodItemService foodItemService;
        private readonly ICategoryService categoryService;

        public OrderWindow(DiningTable table)
        {
            InitializeComponent();
            currentTable = table;
            txtTableName.Text = $"Hóa đơn - Bàn {currentTable.TableName}";
            categoryService = new CategoryService();
            foodItemService = new FoodItemService();
            LoadMenu();
            SetupOrder();
            LoadCategory();
        }
        private void LoadCategory()
        {
            try
            {
                var listCategory = categoryService.GetAllCategory();
                listCategory.Insert(0, new Category { CategoryId = 0, CategoryName = "--- Tất cả ---" });
                cboCategory.ItemsSource = listCategory;
                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải category: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void cboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCategory.SelectedValue != null)
            {
                if (int.TryParse(cboCategory.SelectedValue.ToString(), out int selectedCategoryId))
                {
                    if (selectedCategoryId == 0)
                    {
                        LoadMenu();
                    }
                    else
                    {
                        icMenu.ItemsSource = foodItemService.FilterFoodIitem(selectedCategoryId);
                    }
                }
            }
        }


        private void LoadMenu()
        {
            try
            {
                var menuItems = foodItemService.ShowAllFoodItem();
                icMenu.ItemsSource = menuItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thực đơn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetupOrder()
        {
        }
        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnFoodItem_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;
            if (clickedButton?.Tag is FoodItem selectedFood)
            {
                MessageBox.Show($"Bạn vừa chọn món: {selectedFood.FoodName} - Giá: {selectedFood.Price}\n(Test thành công! Chuẩn bị viết code lưu vào DB...)");
            }
        }
    }
}