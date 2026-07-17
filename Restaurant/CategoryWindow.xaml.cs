using BusinessObject;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private ICategoriesService service = new CategoriesService();
        public CategoryWindow()
        {
            InitializeComponent();
            CategoryLoad();
        }

        public void CategoryLoad()
        {
            List<Category> categoriesList = service.GetCategories();
            dgCategories.ItemsSource = categoriesList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Category newcategory = new Category();
            newcategory.CategoryName = txtCategoryName.Text;
            service.AddCategory(newcategory);
            MessageBox.Show("Add successfull");
            CategoryLoad();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryId.Text) || string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin ");
                return;
            }
            Category tempCategory = new Category
            {
                CategoryId = Int32.Parse(txtCategoryId.Text),
                CategoryName = txtCategoryName.Text
            };
            service.UpdateCategoryByID(tempCategory);
            MessageBox.Show("Update Category Successful");
            CategoryLoad();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryId.Text))
            {
                MessageBox.Show("Hãy chọn danh mục mà bạn muốn sửa");
                return;
            }
            int categoryId = Int32.Parse(txtCategoryId.Text);
            service.DeleteCategoryByID(categoryId);
            MessageBox.Show("Delete Category By Id" + categoryId + "Successfull");
            CategoryLoad();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCategoryId.Clear();
            txtCategoryName.Clear();
            dgCategories.SelectedItem = null;
        }

        private void dgCategories_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid banh = sender as DataGrid;
            Category category = banh.SelectedItem as Category;
            if (category != null)
            {
                txtCategoryId.Text = category.CategoryId.ToString();
                txtCategoryName.Text = category.CategoryName.ToString();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
