# FlashCartApi

**FlashCartApi** là backend cho ứng dụng học từ vựng [Rememvoca](https://github.com/Rainbow-113/rememvoca), xây dựng bằng ASP.NET Core Web API, sử dụng MongoDB Atlas làm database.

Đây là repo **backend**. Repo frontend (Flutter) nằm tại: [Rainbow-113/rememvoca](https://github.com/Rainbow-113/rememvoca).

## Giới thiệu

FlashCartApi cung cấp các API RESTful phục vụ việc xác thực người dùng, quản lý folder và quản lý từ vựng (word/flashcard) cho ứng dụng Rememvoca.

## Tính năng chính

- Xác thực người dùng (đăng ký, đăng nhập) với JWT
- Quản lý người dùng (User)
- Quản lý folder từ vựng: tạo, sửa, xóa, tìm kiếm
- Quản lý từ vựng/flashcard (Word): tạo, sửa, xóa, tìm kiếm trong folder
- Tài liệu API tự động qua Swagger UI

## Công nghệ sử dụng

- **Framework:** ASP.NET Core Web API (C#)
- **Database:** MongoDB Atlas
- **Authentication:** JWT (JSON Web Token)
- **API Documentation:** Swashbuckle (Swagger / OpenAPI)

## Kiến trúc

Dự án tổ chức theo kiến trúc phân lớp, tách biệt rõ trách nhiệm từng tầng:

```
Repository → Service → Controller
```

- **Repository**: truy cập dữ liệu trực tiếp với MongoDB
- **Service**: xử lý logic nghiệp vụ, gọi Repository
- **Controller**: nhận request, trả response, gọi Service

### Cấu trúc thư mục

```
FlashCartApi/
├── Controllers/        # Endpoint API (AuthController, FolderController, WordController, UserController...)
├── Services/           # Logic nghiệp vụ
├── Repositories/        # Truy cập dữ liệu MongoDB
├── Models/              # Định nghĩa entity (User, Folder, Word...)
├── DTOs/                # Data Transfer Objects cho request/response
├── Shares/              # Thành phần dùng chung (helper, middleware, constants...)
├── appsettings.json
└── Program.cs
```

## Entity chính

| Entity | Mô tả |
|---|---|
| **User** | Thông tin người dùng, dùng cho đăng ký/đăng nhập |
| **Auth** | Xử lý đăng ký, đăng nhập, sinh và xác thực JWT token |
| **Folder** | Nhóm/thư mục chứa các từ vựng theo chủ đề |
| **Word** | Từ vựng/flashcard thuộc một folder (từ, nghĩa, ví dụ...) |

## Yêu cầu

- .NET SDK (phiên bản tương ứng với dự án)
- Tài khoản MongoDB Atlas (hoặc MongoDB local) và connection string
- IDE: Visual Studio / VS Code / Rider

## Cài đặt và chạy

1. Clone repo:
   ```bash
   git clone https://github.com/Rainbow-113/FlashCartApi.git
   cd FlashCartApi
   ```

2. Cấu hình `appsettings.json` (hoặc `appsettings.Development.json`) với connection string MongoDB và secret key cho JWT:
   ```json
   {
     "MongoDbSettings": {
       "ConnectionString": "your-mongodb-atlas-connection-string",
       "DatabaseName": "FlashCartDb"
     },
     "JwtSettings": {
       "SecretKey": "your-secret-key",
       "Issuer": "FlashCartApi",
       "Audience": "FlashCartClient",
       "ExpiryMinutes": 60
     }
   }
   ```

3. Restore packages:
   ```bash
   dotnet restore
   ```

4. Chạy ứng dụng:
   ```bash
   dotnet run
   ```

5. Mở Swagger UI để xem và test API (mặc định thường là `https://localhost:<port>/swagger`).

## Xác thực (Authentication)

API sử dụng JWT để bảo vệ các endpoint. Sau khi đăng nhập thành công qua `/api/auth/login`, client nhận về access token và gửi kèm trong header của các request cần xác thực:

```
Authorization: Bearer <token>
```

## Tài liệu API

API được mô tả đầy đủ qua Swagger UI, tự động sinh từ các Controller — bao gồm danh sách endpoint, request/response schema và khả năng test trực tiếp trên trình duyệt.

## Kết nối với Frontend

Ứng dụng frontend [Rememvoca](https://github.com/Rainbow-113/rememvoca) (Flutter) sử dụng API này để xử lý đăng nhập, lưu trữ và đồng bộ dữ liệu folder/từ vựng. Cần chạy backend trước (local hoặc deploy) và cấu hình đúng base URL ở phía frontend.

## Trạng thái dự án

Dự án đang trong quá trình phát triển, phục vụ mục tiêu xây dựng portfolio cá nhân và làm dự án full-stack hoàn chỉnh.

## Tác giả

Phát triển bởi [Rainbow-113](https://github.com/Rainbow-113).
