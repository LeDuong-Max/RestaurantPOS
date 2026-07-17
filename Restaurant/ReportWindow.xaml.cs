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
            // Máº·c Ä‘á»‹nh chá»n thá»‘ng kÃª 7 ngÃ y gáº§n nháº¥t
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
                MessageBox.Show("Vui lÃ²ng chá»n khoáº£ng thá»i gian há»£p lá»‡!", "Cáº£nh bÃ¡o");
                return;
            }

            DateTime fromDate = dpFromDate.SelectedDate.Value;
            DateTime toDate = dpToDate.SelectedDate.Value;

            if (fromDate > toDate)
            {
                MessageBox.Show("NgÃ y báº¯t Ä‘áº§u khÃ´ng Ä‘Æ°á»£c lá»›n hÆ¡n ngÃ y káº¿t thÃºc!", "Cáº£nh bÃ¡o");
                return;
            }

            // 1. Táº£i dá»¯ liá»‡u Doanh thu
            var revenueData = reportService.GetRevenueReport(fromDate, toDate);
            dgRevenue.ItemsSource = revenueData;

            // TÃ­nh tá»•ng tiá»n
            decimal total = revenueData.Sum(o => o.TotalPrice ?? 0);
            txtTotalRevenue.Text = total.ToString("N0") + " VNÄ";

            // 2. Táº£i dá»¯ liá»‡u Top-sellers
            var topSellingData = reportService.GetTopSellingFoods(fromDate, toDate);
            dgTopSelling.ItemsSource = topSellingData;
        }

        private void btnExportCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV File|*.csv";
                sfd.Title = "LÆ°u bÃ¡o cÃ¡o thá»‘ng kÃª";
                sfd.FileName = "BaoCaoThongKe_" + DateTime.Now.ToString("ddMMyyyy");

                if (sfd.ShowDialog() == true)
                {
                    // Há»— trá»£ hiá»ƒn thá»‹ Tiáº¿ng Viá»‡t cÃ³ dáº¥u khi má»Ÿ báº±ng Excel
                    StringBuilder csv = new StringBuilder();
                    
                    // Kiá»ƒm tra xem ngÆ°á»i dÃ¹ng Ä‘ang Ä‘á»©ng á»Ÿ Tab nÃ o Ä‘á»ƒ xuáº¥t dá»¯ liá»‡u tab Ä‘Ã³
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
                        // Tab MÃ³n bÃ¡n cháº¡y
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

                    // WriteAllText vá»›i UTF8 BOM Ä‘á»ƒ Excel cÃ³ thá»ƒ Ä‘á»c Ä‘Æ°á»£c tiáº¿ng Viá»‡t
                    File.WriteAllText(sfd.FileName, csv.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuáº¥t file bÃ¡o cÃ¡o thÃ nh cÃ´ng!", "ThÃ´ng bÃ¡o", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i khi xuáº¥t file: " + ex.Message, "Lá»—i");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
