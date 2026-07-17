using BusinessObject;
using Microsoft.Win32;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Restaurant
{
    public partial class ReportWindow : Window
    {
        private IReportService reportService = new ReportService();

        public ReportWindow()
        {
            InitializeComponent();
            // Mặc định chọn thống kê 7 ngày gần nhất
            dpFromDate.SelectedDate = DateTime.Today.AddDays(-7);
            dpToDate.SelectedDate = DateTime.Today;
            
            LoadData();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (dpFromDate.SelectedDate == null || dpToDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn khoảng thời gian hợp lệ!", "Cảnh báo");
                return;
            }

            DateTime fromDate = dpFromDate.SelectedDate.Value;
            DateTime toDate = dpToDate.SelectedDate.Value;

            if (fromDate > toDate)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo");
                return;
            }

            // 1. Tải dữ liệu Doanh thu
            var revenueData = reportService.GetRevenueReport(fromDate, toDate);
            dgRevenue.ItemsSource = revenueData;

            // Tính tổng tiền
            decimal total = revenueData.Sum(o => o.TotalPrice ?? 0);
            txtTotalRevenue.Text = total.ToString("N0") + " VNĐ";

            // 2. Tải dữ liệu Top-sellers
            var topSellingData = reportService.GetTopSellingFoods(fromDate, toDate);
            dgTopSelling.ItemsSource = topSellingData;
        }

        private void btnExportCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV File|*.csv";
                sfd.Title = "Lưu báo cáo thống kê";
                sfd.FileName = "BaoCaoThongKe_" + DateTime.Now.ToString("ddMMyyyy");

                if (sfd.ShowDialog() == true)
                {
                    // Hỗ trợ hiển thị Tiếng Việt có dấu khi mở bằng Excel
                    StringBuilder csv = new StringBuilder();
                    
                    // Kiểm tra xem người dùng đang đứng ở Tab nào để xuất dữ liệu tab đó
                    if (tabMain.SelectedIndex == 0) 
                    {
                        // Tab Doanh thu
                        csv.AppendLine("Ma HD,Ban,Thu ngan,Gio vao,Gio ra,Giam gia,Thanh tien");
                        var data = dgRevenue.ItemsSource as List<Order>;
                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                string outTime = item.CheckoutDate?.ToString("dd/MM/yyyy HH:mm") ?? "";
                                csv.AppendLine($"{item.OrderId},{item.Table?.TableName},{item.Account?.FullName},{item.OrderDate:dd/MM/yyyy HH:mm},{outTime},{item.Discount},{item.TotalPrice}");
                            }
                        }
                    }
                    else 
                    {
                        // Tab Món bán chạy
                        csv.AppendLine("Ma Mon,Ten Mon An,So Luong Da Ban,Tong Tien Thu Ve");
                        var data = dgTopSelling.ItemsSource as List<TopSellingFoodDTO>;
                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                csv.AppendLine($"{item.FoodId},{item.FoodName},{item.TotalQuantitySold},{item.TotalRevenue}");
                            }
                        }
                    }

                    // WriteAllText với UTF8 BOM để Excel có thể đọc được tiếng Việt
                    File.WriteAllText(sfd.FileName, csv.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuất file báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
