using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng Bán Hàng (Module 1) đang được phát triển!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnFoodItems_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Ẩn trang chủ đi
            FoodItemWindow foodWindow = new FoodItemWindow();
            foodWindow.ShowDialog();
            this.Show(); // Hiện lại trang chủ khi đóng cửa sổ con
        }

        private void btnDiningTables_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DiningTableWindow tableWindow = new DiningTableWindow();
            tableWindow.ShowDialog();
            this.Show();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ReportWindow reportWindow = new ReportWindow();
            reportWindow.ShowDialog();
            this.Show();
        }
    }
}