# 🍽️ Restaurant POS (Quản Lý Nhà Hàng)

Dự án phần mềm Quản lý Nhà hàng / Quán ăn được xây dựng trên nền tảng WPF (Windows Presentation Foundation) áp dụng chuẩn kiến trúc 3 tầng (3-Tier Architecture). Đồ án thực hành môn học PRN212.

## 🚀 Công nghệ sử dụng
* **Ngôn ngữ:** C# (.NET 8.0)
* **Giao diện:** WPF (Windows Presentation Foundation)
* **Cơ sở dữ liệu:** SQL Server
* **ORM:** Entity Framework Core 8.0 (Database First)
* **Kiến trúc:** 3-Tier (Business Object, Data Access Layer, Repositories, Services, UI)

## 📁 Cấu trúc thư mục (Solution Structure)
* `BusinessObject`: Chứa các class Models (POCO) được tạo tự động từ Database.
* `DataAccessLayer`: Chứa `RestaurantPosContext` và các DAO thao tác trực tiếp với SQL Server.
* `Repositories`: Tầng trung gian (Interface & Implementation) ẩn giấu logic của DAL.
* `Services`: Tầng xử lý nghiệp vụ (Business Logic) trước khi đẩy lên UI.
* `WPF`: Project UI khởi chạy ứng dụng (Views & Code-behind).

## ⚙️ Hướng dẫn cài đặt (Setup Instructions)

**Bước 1: Khởi tạo Database**
1. Mở SQL Server Management Studio (SSMS).
2. Chạy toàn bộ file script `RestaurantPOS.sql` (nằm trong thư mục dự án) để tạo cơ sở dữ liệu và dữ liệu mẫu.

**Bước 2: Cấu hình chuỗi kết nối (Connection String)**
1. Mở project trên Visual Studio.
2. Tìm đến file `RestaurantPosContext.cs` trong project `DataAccessLayer` (Hoặc cấu hình `appsettings.json` bên WPF nếu có).
3. Đổi Server Name (`Server=...`) và Thông tin đăng nhập (`Uid=...;Pwd=...`) cho khớp với SQL Server của máy tính bạn.

**Bước 3: Chạy ứng dụng**
1. Set project `WPF` làm **Startup Project**.
2. Nhấn `F5` để Build và chạy chương trình.

## 👥 Phân quyền hệ thống (Roles)
* **Admin (Role = 1):** Toàn quyền quản lý hệ thống, nhân viên, thực đơn, doanh thu.
* **Thu ngân (Role = 2):** Quản lý bàn ăn, tạo/thanh toán hóa đơn.
* **Phục vụ (Role = 3):** Xem sơ đồ bàn, order món, không có quyền thanh toán.
