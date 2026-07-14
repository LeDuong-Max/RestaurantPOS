using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Services; // Gọi tầng Service vào đây

namespace WPF
{
    public partial class DiningTableWindow : Window
    {
        private readonly IDiningTableService tableService;

        public DiningTableWindow()
        {
            InitializeComponent();
            tableService = new DiningTableService();
            LoadTables();
        }
        private void LoadTables()
        {
            try
            {
                icTables.ItemsSource = tableService.GetAllDiningTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;

            if (clickedButton?.Tag is DiningTable selectedTable)
            {
                OrderWindow orderWindow = new OrderWindow(selectedTable);
                orderWindow.Owner = this;
                orderWindow.ShowDialog();
                LoadTables();
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