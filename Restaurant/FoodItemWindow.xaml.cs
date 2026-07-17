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
            LoadCategories(); // Pháº£i load danh má»¥c vÃ o ComboBox trÆ°á»›c
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
                MessageBox.Show("Vui lÃ²ng Ä‘iá»n Ä‘á»§ thÃ´ng tin vÃ  chá»n danh má»¥c!", "Cáº£nh bÃ¡o");
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
                MessageBox.Show("ThÃªm mÃ³n Äƒn thÃ nh cÃ´ng!");
                LoadFoodItems();
                btnClear_Click(null, null); // Tá»± Ä‘á»™ng lÃ m má»›i Ã´ nháº­p
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i: GiÃ¡ tiá»n pháº£i lÃ  sá»‘ há»£p lá»‡. " + ex.Message, "Lá»—i");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodId.Text) || string.IsNullOrWhiteSpace(txtFoodName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) || cbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lÃ²ng chá»n mÃ³n Äƒn vÃ  Ä‘iá»n Ä‘áº§y Ä‘á»§ thÃ´ng tin!", "Cáº£nh bÃ¡o");
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
                MessageBox.Show("Cáº­p nháº­t thÃ nh cÃ´ng!");
                LoadFoodItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i: GiÃ¡ tiá»n pháº£i lÃ  sá»‘ há»£p lá»‡. " + ex.Message, "Lá»—i");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFoodId.Text))
            {
                MessageBox.Show("Vui lÃ²ng chá»n mÃ³n Äƒn cáº§n xÃ³a!", "Cáº£nh bÃ¡o");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Báº¡n cÃ³ cháº¯c cháº¯n muá»‘n xÃ³a mÃ³n nÃ y?", "XÃ¡c nháº­n", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foodService.DeleteFoodItem(int.Parse(txtFoodId.Text));
                MessageBox.Show("XÃ³a thÃ nh cÃ´ng!");
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
            chkIsAvailable.IsChecked = true; // Tráº£ vá» máº·c Ä‘á»‹nh
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
                txtPrice.Text = selectedItem.Price.ToString("0.##"); // Bá» sá»‘ 0 vÃ´ nghÄ©a á»Ÿ Ä‘uÃ´i
                cbCategory.SelectedValue = selectedItem.CategoryId; // Tá»± Ä‘á»™ng chá»n Ä‘Ãºng Danh má»¥c trÃªn ComboBox
                chkIsAvailable.IsChecked = selectedItem.IsAvailable;
            }
        }
    }
}
