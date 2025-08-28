USE [StoreMVC]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Xóa các bảng nếu chúng đã tồn tại để tránh lỗi
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
    DROP TABLE dbo.Products;

IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL
    DROP TABLE dbo.Categories;

-- Tạo bảng Categories (Danh mục)
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Tạo bảng Products (Sản phẩm)
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[IsFeatured] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Thêm khóa ngoại cho bảng Products
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId]
GO

-- Chèn dữ liệu mẫu cho Category (5 danh mục)
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Áo Khoác')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Quần Jeans')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Áo Thun')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Váy Đầm')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Giày Thể Thao')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO

-- Chèn 20 sản phẩm mẫu về thời trang
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
-- Đánh dấu 4 sản phẩm đầu tiên là nổi bật (IsFeatured = 1, tương đương True)
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (1, N'Áo Khoác Da Biker', 1200000.00, N'1.jpg', 1, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (2, N'Áo Khoác Bomber Kẻ', 850000.00, N'2.jpg', 1, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (3, N'Áo Khoác Jeans Trắng', 950000.00, N'3.jpg', 1, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (4, N'Quần Jeans Bó Gối', 520000.00, N'4.jpg', 2, 1)
GO
-- Các sản phẩm còn lại không nổi bật (IsFeatured = 0, tương đương False)
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (5, N'Quần Jeans Xanh Cổ Điển', 480000.00, N'5.jpg', 0, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (6, N'Quần Jeans Rách Cá Tính', 550000.00, N'6.jpg', 0, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (7, N'Áo Thun Trơn Basic', 250000.00, N'7.jpg', 0, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (8, N'Áo Thun In Họa Tiết', 300000.00, N'8.jpg', 0, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (9, N'Áo Thun Crop Top', 280000.00, N'9.jpg', 0, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (10, N'Váy Đầm Maxi Hoa Nhí', 650000.00, N'10.jpg', 0, 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (11, N'Váy Đầm Ôm Body Đen', 720000.00, N'11.jpg', 0, 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (12, N'Váy Đầm Xòe Công Chúa', 800000.00, N'12.jpg', 0, 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (13, N'Giày Thể Thao Phản Quang', 980000.00, N'13.jpg', 0, 5)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (14, N'Giày Sneaker Cổ Thấp Trắng', 850000.00, N'14.jpg', 0, 5)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (15, N'Giày Chạy Bộ Đệm Khí', 1250000.00, N'15.jpg', 0, 5)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (16, N'Quần Jeans Culottes', 580000.00, N'16.jpg', 0, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (17, N'Áo Khoác Gió Hai Lớp', 750000.00, N'17.jpg', 0, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (18, N'Áo Thun Tay Dài', 320000.00, N'18.jpg', 0, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (19, N'Váy Đầm Dáng Suông', 680000.00, N'19.jpg', 0, 4)
GO
INSERT [dbo].[Products] ([Id], [Name], [Price], [ImageUrl], [IsFeatured], [CategoryId]) VALUES (20, N'Giày Sandal Thể Thao', 650000.00, N'20.jpg', 0, 5)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
