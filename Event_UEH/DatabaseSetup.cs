using System;
using System.Data.SqlClient;

namespace Event_UEH
{
    public static class DatabaseSetup
    {
        // Phương thức thiết lập cơ sở dữ liệu
        public static void SetupDatabase()
        {
            // Kết nối tới cơ sở dữ liệu EventManagementDB
            using (SqlConnection connection = DatabaseConnection.GetConnection())
            {
                if (connection != null)
                {
                    // Tạo bảng Users
                    string createUsersTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE Users
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                Username NVARCHAR(50) NOT NULL UNIQUE,  -- Tên người dùng
                                Password NVARCHAR(255) NOT NULL,        -- Mật khẩu
                                Email NVARCHAR(100) NOT NULL UNIQUE,    -- Địa chỉ email
                                FullName NVARCHAR(100) NOT NULL,        -- Tên đầy đủ
                                RoleId INT NOT NULL,                     -- Khóa ngoại từ bảng Roles
                                FOREIGN KEY(RoleId) REFERENCES Roles(Id) -- Ràng buộc khóa ngoại
                            );
                            PRINT 'Bảng ''Users'' đã được tạo.';
                        END
                    ";

                    // Tạo bảng Roles
                    string createRolesTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE Roles
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                RoleName NVARCHAR(50) NOT NULL UNIQUE    -- Tên vai trò
                            );
                            PRINT 'Bảng ''Roles'' đã được tạo.';
                        END
                    ";

                    // Tạo bảng UserRole
                    string createUserRoleTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE UserRole
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                UserId INT NOT NULL,                    -- Khóa ngoại từ bảng Users
                                RoleId INT NOT NULL,                    -- Khóa ngoại từ bảng Roles
                                FOREIGN KEY(UserId) REFERENCES Users(Id), -- Ràng buộc khóa ngoại
                                FOREIGN KEY(RoleId) REFERENCES Roles(Id)  -- Ràng buộc khóa ngoại
                            );
                            PRINT 'Bảng ''UserRole'' đã được tạo.';
                        END
                    ";

                    // Tạo bảng Events
                    string createEventsTable = @"
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
    BEGIN
        CREATE TABLE Events
        (
            Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
            Title NVARCHAR(100) NOT NULL,           -- Tiêu đề sự kiện
            Description NVARCHAR(500),               -- Mô tả sự kiện
            Location NVARCHAR(200),                  -- Địa điểm sự kiện
            StartDate DATETIME NOT NULL,             -- Ngày bắt đầu
            EndDate DATETIME NOT NULL,               -- Ngày kết thúc
            CreatedBy INT,                           -- ID của người tạo sự kiện
            CreatedDate DATETIME DEFAULT GETDATE(),  -- Ngày tạo sự kiện
            IsActive BIT DEFAULT 1,                  -- Trạng thái sự kiện
            OrganizerId INT NOT NULL,                -- Khóa ngoại từ bảng Users (tổ chức)
            FOREIGN KEY(OrganizerId) REFERENCES Users(Id) -- Ràng buộc khóa ngoại
        );
        PRINT 'Bảng ''Events'' đã được tạo.';
    END
";


                    // Tạo bảng RegisteredEvents
                    string createRegisteredEventsTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegisteredEvents]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE RegisteredEvents
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                UserId INT NOT NULL,                    -- Khóa ngoại từ bảng Users
                                EventId INT NOT NULL,                   -- Khóa ngoại từ bảng Events
                                FOREIGN KEY(UserId) REFERENCES Users(Id), -- Ràng buộc khóa ngoại
                                FOREIGN KEY(EventId) REFERENCES Events(Id) -- Ràng buộc khóa ngoại
                            );
                            PRINT 'Bảng ''RegisteredEvents'' đã được tạo.';
                        END
                    ";

                    // Tạo bảng Trash
                    string createTrashTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Trash]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE Trash
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                EventId INT NOT NULL,                   -- Khóa ngoại từ bảng Events
                                UserId INT NOT NULL,                    -- Khóa ngoại từ bảng Users
                                FOREIGN KEY(EventId) REFERENCES Events(Id), -- Ràng buộc khóa ngoại
                                FOREIGN KEY(UserId) REFERENCES Users(Id) -- Ràng buộc khóa ngoại
                            );
                            PRINT 'Bảng ''Trash'' đã được tạo.';
                        END
                    ";

                    // Tạo bảng Rate
                    string createRateTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rate]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE Rate
                            (
                                Id INT PRIMARY KEY IDENTITY(1,1),       -- ID tự động tăng
                                UserId INT NOT NULL,                    -- Khóa ngoại từ bảng Users
                                EventId INT NOT NULL,                   -- Khóa ngoại từ bảng Events
                                Rating INT CHECK (Rating >= 1 AND Rating <= 5), -- Đánh giá (1-5)
                                FOREIGN KEY(UserId) REFERENCES Users(Id), -- Ràng buộc khóa ngoại
                                FOREIGN KEY(EventId) REFERENCES Events(Id) -- Ràng buộc khóa ngoại
                            );
                            PRINT 'Bảng ''Rate'' đã được tạo.';
                        END
                    ";

                    // Thực thi các lệnh tạo bảng
                    ExecuteNonQuery(connection, createRolesTable);
                    ExecuteNonQuery(connection, createUsersTable);
                    ExecuteNonQuery(connection, createUserRoleTable);
                    ExecuteNonQuery(connection, createEventsTable);
                    ExecuteNonQuery(connection, createRegisteredEventsTable);
                    ExecuteNonQuery(connection, createTrashTable);
                    ExecuteNonQuery(connection, createRateTable);
                    SeedRoles(connection);
                }
            }
        }
        // Phương thức thêm bản ghi mẫu vào bảng Roles
        private static void SeedRoles(SqlConnection connection)
        {
            string insertRoles = @"
                INSERT INTO Roles (RoleName)
                VALUES
                ('Admin'),
                ('Organizer'),
                ('Student')
            ";

            try
            {
                ExecuteNonQuery(connection, insertRoles);
                Console.WriteLine("Bản ghi mẫu cho bảng 'Roles' đã được thêm.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra khi thêm bản ghi mẫu: " + ex.Message);
            }
        }

        // Phương thức để thực hiện các câu lệnh SQL
        private static void ExecuteNonQuery(SqlConnection connection, string commandText)
        {
            using (SqlCommand command = new SqlCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
