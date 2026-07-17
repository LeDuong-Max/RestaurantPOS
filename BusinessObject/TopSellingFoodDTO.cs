using System;

namespace BusinessObject
{
    // Lớp này không ánh xạ với một bảng nào dưới DB cả (Không phải là Entity).
    // Nó được gọi là DTO (Data Transfer Object) dùng để chứa dữ liệu gộp từ nhiều bảng (JOIN)
    // phục vụ riêng cho việc hiển thị Món ăn bán chạy trên DataGrid.
    public class TopSellingFoodDTO
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; } = null!;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
