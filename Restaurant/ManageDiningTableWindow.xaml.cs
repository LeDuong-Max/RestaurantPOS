using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant
{
    public partial class ManageDiningTableWindow : Window
    {
        private IDiningTableService tableService = new DiningTableService();

        public ManageDiningTableWindow()
        {
            InitializeComponent();
            LoadTables();
        }

        private void LoadTables()
        {
            dgTables.ItemsSource = tableService.GetDiningTables();
        }

        // HÃ m há»— trá»£ láº¥y Status (0 hoáº·c 1) tá»« ComboBox
        private int GetSelectedStatus()
        {
            if (cbStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                return int.Parse(selectedItem.Tag.ToString());
            }
            return 0; // Máº·c Ä‘á»‹nh lÃ  0 (BÃ n trá»‘ng)
        }

        // HÃ m há»— trá»£ gÃ¡n Status lÃªn ComboBox
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
                MessageBox.Show("Vui lÃ²ng nháº­p tÃªn bÃ n vÃ  chá»n tráº¡ng thÃ¡i!", "Cáº£nh bÃ¡o");
                return;
            }

            DiningTable newTable = new DiningTable
            {
                TableName = txtTableName.Text,
                Status = GetSelectedStatus()
            };

            tableService.AddDiningTable(newTable);
            MessageBox.Show("ThÃªm bÃ n má»›i thÃ nh cÃ´ng!");
            LoadTables();
            btnClear_Click(null, null);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableId.Text) || string.IsNullOrWhiteSpace(txtTableName.Text) || cbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lÃ²ng chá»n bÃ n vÃ  Ä‘iá»n Ä‘áº§y Ä‘á»§ thÃ´ng tin!", "Cáº£nh bÃ¡o");
                return;
            }

            DiningTable updateTable = new DiningTable
            {
                TableId = int.Parse(txtTableId.Text),
                TableName = txtTableName.Text,
                Status = GetSelectedStatus()
            };

            tableService.UpdateDiningTable(updateTable);
            MessageBox.Show("Cáº­p nháº­t bÃ n thÃ nh cÃ´ng!");
            LoadTables();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableId.Text))
            {
                MessageBox.Show("Vui lÃ²ng chá»n bÃ n cáº§n xÃ³a!", "Cáº£nh bÃ¡o");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Báº¡n cÃ³ cháº¯c cháº¯n muá»‘n xÃ³a bÃ n nÃ y?", "XÃ¡c nháº­n", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                tableService.DeleteDiningTable(int.Parse(txtTableId.Text));
                MessageBox.Show("XÃ³a thÃ nh cÃ´ng!");
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
