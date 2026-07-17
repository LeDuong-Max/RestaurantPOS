using BusinessObject; 
using Services;      
using System;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public partial class OrderWindow : Window
    {
        private DiningTable currentTable;
        private Order currentOrder;
        private readonly IFoodItemService foodItemService;
        private readonly ICategoryService categoryService;
        private readonly IDiningTableService ingoutTableService;
        private readonly IOrderService orderService;
        private readonly IOrderDetailService orderDetailService;
        public OrderWindow(DiningTable table)
        {
            InitializeComponent();
            currentTable = table;
            txtTableName.Text = $"HÃ³a Ä‘Æ¡n - {currentTable.TableName}";
            categoryService = new CategoryService();
            foodItemService = new FoodItemService();
            ingoutTableService = new DiningTableService();
            orderService = new OrderService();
            orderDetailService = new OrderDetailService();
            if (currentTable.Status == 0)
            {
                ingoutTableService.UpdateStatus(currentTable.TableId, 1);
            }

            LoadMenu();
            SetupOrder();
            LoadCategory();
        }
        private void LoadCategory()
        {
            try
            {
                var listCategory = categoryService.GetAllCategory();
                listCategory.Insert(0, new Category { CategoryId = 0, CategoryName = "--- Táº¥t cáº£ ---" });
                cboCategory.ItemsSource = listCategory;
                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i táº£i category: " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Lá»—i táº£i thá»±c Ä‘Æ¡n: " + ex.Message, "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetupOrder()
        {
            currentOrder = orderService.GetOrder(currentTable.TableId,0);

            if (currentOrder == null)
            {
                currentOrder = new Order
                {
                    TableId = currentTable.TableId,
                    Status = 0,
                    AccountId = AppSession.CurrentUser.AccountId,
                    OrderDate = DateTime.Now,
                    TotalPrice = 0
                };
                orderService.AddOrder(currentOrder);
            }
            else
            {
                LoadOrderDetails();
            }
        }
        private void LoadOrderDetails()
        {
            var displayList = orderDetailService.GetOrderDetailsDisplay(currentOrder.OrderId);
            dgOrderDetails.ItemsSource = displayList;
            decimal total = orderDetailService.CalculateOrderTotal(currentOrder.OrderId);
            txtTotalPrice.Text = $"{total:N0} VNÄ";
        }
        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show($"Báº¡n cÃ³ cháº¯c cháº¯n muá»‘n thanh toÃ¡n hÃ³a Ä‘Æ¡n cho {currentTable.TableName} khÃ´ng?", 
                    "XÃ¡c nháº­n thanh toÃ¡n", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    decimal total = orderDetailService.CalculateOrderTotal(currentOrder.OrderId);
                    currentOrder.TotalPrice = total;
                    currentOrder.Status = 1;

                    orderService.UpdateOrder(currentOrder);
                    ingoutTableService.UpdateStatus(currentTable.TableId, 0);

                    MessageBox.Show("Thanh toÃ¡n thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lá»—i trong quÃ¡ trÃ¬nh thanh toÃ¡n: {ex.Message}", "Lá»—i", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnFoodItem_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;
            if (clickedButton?.Tag is FoodItem selectedFood)
            {
                orderDetailService.AddFoodItem(currentOrder.OrderId, selectedFood);
                LoadOrderDetails();
            }
        }
    }
}
