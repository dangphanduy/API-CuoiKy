-- =====================================================================
-- LEARNING PLATFORM - SEED DATA WITH REAL VIDEOS (UPDATED)
-- This file contains seed data using actual video files from uploads folder
-- Videos location: wwwroot/uploads/videos/2025/11/
-- Run this after creating the database schema (LearningPlatform.sql)
-- =====================================================================

USE [LearningPlatform];
GO

-- =====================================================================
-- 1) CLEAR EXISTING DATA (Optional - uncomment if needed)
-- =====================================================================
/*
PRINT 'Clearing existing data...';
DELETE FROM dbo.AttemptAnswers;
DELETE FROM dbo.TestAttempts;
DELETE FROM dbo.CourseProgress;
DELETE FROM dbo.CourseReviews;
DELETE FROM dbo.Payments;
DELETE FROM dbo.OrderItems;
DELETE FROM dbo.Orders;
DELETE FROM dbo.CoursePurchases;
DELETE FROM dbo.CartItems;
DELETE FROM dbo.ShoppingCarts;
DELETE FROM dbo.QuestionOptions;
DELETE FROM dbo.Questions;
DELETE FROM dbo.Tests;
DELETE FROM dbo.Flashcards;
DELETE FROM dbo.FlashcardSets;
DELETE FROM dbo.LessonContents;
DELETE FROM dbo.Lessons;
DELETE FROM dbo.CourseChapters;
DELETE FROM dbo.Courses;
DELETE FROM dbo.CourseCategories;
DELETE FROM dbo.UserInterests;
DELETE FROM dbo.Notifications;
DELETE FROM dbo.UserSettings;
DELETE FROM dbo.UserProfiles;
DELETE FROM dbo.Users WHERE UserId > 1;
GO
*/

-- =====================================================================
-- 2) ROLES (Assuming Roles table exists or handled separately)
-- =====================================================================
-- If Roles need to be seeded, add here

-- =====================================================================
-- 3) USERS
-- =====================================================================
PRINT 'Seeding Users...';

-- Password: password123 (hashed using SHA256)
-- Hash value: jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=

SET IDENTITY_INSERT dbo.Users ON;
GO

MERGE INTO dbo.Users AS target
USING (VALUES
    (1, 'admin', 'admin@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Quản trị viên', 1, '0909000001', 1),
    (2, 'teacher1', 'teacher1@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Trần Minh Khoa', 2, '0909000002', 1),
    (3, 'teacher2', 'teacher2@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nguyễn Thị Lan', 2, '0909000003', 1),
    (4, 'teacher3', 'teacher3@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Phạm Văn Hùng', 2, '0909000004', 1),
    (5, 'student1', 'student1@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nguyễn Lan Anh', 2, '0909000006', 1),
    (6, 'student2', 'student2@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Trần Hoàng Long', 2, '0909000007', 1),
    (7, 'student3', 'student3@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Lê Thị Hương', 2, '0909000008', 1),
    (8, 'student4', 'student4@ymedu.vn', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Phạm Minh Tâm', 2, '0909000009', 1)
) AS source (UserId, Username, Email, PasswordHash, FullName, RoleId, Phone, Status)
ON target.UserId = source.UserId
WHEN NOT MATCHED THEN
    INSERT (UserId, Username, Email, PasswordHash, FullName, RoleId, Phone, Status)
    VALUES (source.UserId, source.Username, source.Email, source.PasswordHash, source.FullName, source.RoleId, source.Phone, source.Status);
GO

SET IDENTITY_INSERT dbo.Users OFF;
GO

-- =====================================================================
-- 4) USER PROFILES & SETTINGS
-- =====================================================================
PRINT 'Seeding User Profiles...';

MERGE INTO dbo.UserProfiles AS target
USING (VALUES
    (1, '1990-05-15', N'Nam', N'Quản trị viên hệ thống học tập Ymedu', N'Đại học Bách Khoa', N'Tốt nghiệp'),
    (2, '1988-08-20', N'Nam', N'Giảng viên chuyên ngành Công nghệ thông tin với 10 năm kinh nghiệm', N'Đại học Khoa học Tự nhiên', N'Thạc sĩ'),
    (3, '1992-03-10', N'Nữ', N'Giảng viên chuyên về Cơ sở dữ liệu và SQL', N'Đại học Công nghệ', N'Tiến sĩ'),
    (4, '1985-11-25', N'Nam', N'Chuyên gia phân tích dữ liệu và Business Intelligence', N'Đại học Kinh tế', N'Thạc sĩ'),
    (5, '2003-01-15', N'Nữ', N'Sinh viên năm 3 ngành CNTT', N'Đại học Bách Khoa', N'Năm 3'),
    (6, '2002-09-20', N'Nam', N'Sinh viên năm 4 ngành CNTT', N'Đại học Khoa học Tự nhiên', N'Năm 4'),
    (7, '2003-05-12', N'Nữ', N'Sinh viên năm 2 ngành CNTT', N'Đại học Công nghệ', N'Năm 2'),
    (8, '2004-02-28', N'Nam', N'Sinh viên năm 1 ngành CNTT', N'Đại học Bách Khoa', N'Năm 1')
) AS source (UserId, DoB, Gender, Bio, SchoolName, GradeLevel)
ON target.UserId = source.UserId
WHEN NOT MATCHED THEN
    INSERT (UserId, DoB, Gender, Bio, SchoolName, GradeLevel)
    VALUES (source.UserId, source.DoB, source.Gender, source.Bio, source.SchoolName, source.GradeLevel);
GO

MERGE INTO dbo.UserSettings AS target
USING (VALUES
    (1, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 1),
    (2, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 1),
    (3, 'dark', 'vi', 'Asia/Ho_Chi_Minh', 1, 0),
    (4, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 1),
    (5, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 1),
    (6, 'dark', 'vi', 'Asia/Ho_Chi_Minh', 0, 1),
    (7, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 1),
    (8, 'light', 'vi', 'Asia/Ho_Chi_Minh', 1, 0)
) AS source (UserId, UiTheme, Language, TimeZone, EmailOptIn, PushOptIn)
ON target.UserId = source.UserId
WHEN NOT MATCHED THEN
    INSERT (UserId, UiTheme, Language, TimeZone, EmailOptIn, PushOptIn)
    VALUES (source.UserId, source.UiTheme, source.Language, source.TimeZone, source.EmailOptIn, source.PushOptIn);
GO

-- =====================================================================
-- 5) COURSE CATEGORIES
-- =====================================================================
PRINT 'Seeding Course Categories...';

SET IDENTITY_INSERT dbo.CourseCategories ON;
GO

MERGE INTO dbo.CourseCategories AS target
USING (VALUES
    (1, N'Lập trình', 'lap-trinh', N'Các khóa học về lập trình và phát triển phần mềm', 1),
    (2, N'Cơ sở dữ liệu', 'co-so-du-lieu', N'Học SQL, thiết kế và quản trị cơ sở dữ liệu', 2),
    (3, N'Phân tích dữ liệu', 'phan-tich-du-lieu', N'Học Power BI, Python cho Data Analytics', 3),
    (4, N'Phát triển Web', 'phat-trien-web', N'HTML, CSS, JavaScript, Frameworks', 4)
) AS source (CategoryId, Name, Slug, Description, DisplayOrder)
ON target.CategoryId = source.CategoryId
WHEN NOT MATCHED THEN
    INSERT (CategoryId, Name, Slug, Description, DisplayOrder)
    VALUES (source.CategoryId, source.Name, source.Slug, source.Description, source.DisplayOrder);
GO

SET IDENTITY_INSERT dbo.CourseCategories OFF;
GO

-- =====================================================================
-- 6) COURSES WITH REAL VIDEO CONTENT
-- =====================================================================
PRINT 'Seeding Courses...';

SET IDENTITY_INSERT dbo.Courses ON;
GO

MERGE INTO dbo.Courses AS target
USING (VALUES
    (1, 2, 2, N'SQL Server từ cơ bản đến nâng cao', 'sql-server-co-ban-nang-cao', 
        N'Khóa học toàn diện về SQL Server, từ SELECT cơ bản đến Stored Procedures và tối ưu hóa', 
        299000, 1, 'https://images.unsplash.com/photo-1544383835-bda2bc66a55d?w=800'),
    (2, 2, 4, N'Phát triển Web với HTML, CSS, JavaScript', 'web-development-fundamentals', 
        N'Học cơ bản về phát triển web từ HTML, CSS đến JavaScript hiện đại', 
        349000, 1, 'https://images.unsplash.com/photo-1498050108023-c5249f4df085?w=800')
) AS source (CourseId, OwnerId, CategoryId, Title, Slug, Summary, Price, IsPublished, CoverUrl)
ON target.CourseId = source.CourseId
WHEN NOT MATCHED THEN
    INSERT (CourseId, OwnerId, CategoryId, Title, Slug, Summary, Price, IsPublished, CoverUrl)
    VALUES (source.CourseId, source.OwnerId, source.CategoryId, source.Title, source.Slug, 
            source.Summary, source.Price, source.IsPublished, source.CoverUrl);
GO

SET IDENTITY_INSERT dbo.Courses OFF;
GO

-- =====================================================================
-- 7) COURSE CHAPTERS & LESSONS WITH REAL VIDEOS
-- =====================================================================
PRINT 'Seeding Course Chapters & Lessons...';

-- Course 1: SQL Server - Chapters
SET IDENTITY_INSERT dbo.CourseChapters ON;
GO

MERGE INTO dbo.CourseChapters AS target
USING (VALUES
    (1, 1, N'Chương 1: Giới thiệu SQL Server', N'Làm quen với SQL Server và các khái niệm cơ bản', 1),
    (2, 1, N'Chương 2: Truy vấn dữ liệu', N'Học các lệnh SELECT, WHERE, JOIN', 2),
    (3, 2, N'Chương 1: HTML Fundamentals', N'Học HTML từ cơ bản', 1),
    (4, 2, N'Chương 2: CSS Styling', N'Tạo giao diện đẹp với CSS', 2)
) AS source (ChapterId, CourseId, Title, Description, OrderIndex)
ON target.ChapterId = source.ChapterId
WHEN NOT MATCHED THEN
    INSERT (ChapterId, CourseId, Title, Description, OrderIndex)
    VALUES (source.ChapterId, source.CourseId, source.Title, source.Description, source.OrderIndex);
GO

SET IDENTITY_INSERT dbo.CourseChapters OFF;
GO

-- Lessons for Course 1 & 2
SET IDENTITY_INSERT dbo.Lessons ON;
GO

MERGE INTO dbo.Lessons AS target
USING (VALUES
    -- Chapter 1 lessons (SQL Server)
    (1, 1, N'Bài 1: Tổng quan về SQL Server', N'Giới thiệu SQL Server và cài đặt', 1, 'Course'),
    (2, 1, N'Bài 2: Kiến trúc cơ sở dữ liệu', N'Hiểu về tables, columns, keys', 2, 'Course'),
    -- Chapter 2 lessons (SQL Server)
    (3, 2, N'Bài 3: SELECT Statement cơ bản', N'Truy vấn dữ liệu đơn giản', 1, 'Course'),
    (4, 2, N'Bài 4: WHERE và Filtering', N'Lọc dữ liệu với điều kiện', 2, 'Course'),
    -- Chapter 3 lessons (Web Development)
    (5, 3, N'Bài 1: HTML Cơ bản', N'Các thẻ HTML cơ bản và cấu trúc trang', 1, 'Course'),
    (6, 3, N'Bài 2: HTML Forms', N'Tạo form và thu thập dữ liệu', 2, 'Course'),
    -- Chapter 4 lessons (Web Development)
    (7, 4, N'Bài 3: CSS Basics', N'Selector, Properties và Values', 1, 'Course'),
    (8, 4, N'Bài 4: CSS Layout', N'Flexbox và Grid Layout', 2, 'Course')
) AS source (LessonId, ChapterId, Title, Description, OrderIndex, Visibility)
ON target.LessonId = source.LessonId
WHEN NOT MATCHED THEN
    INSERT (LessonId, ChapterId, Title, Description, OrderIndex, Visibility)
    VALUES (source.LessonId, source.ChapterId, source.Title, source.Description, source.OrderIndex, source.Visibility);
GO

SET IDENTITY_INSERT dbo.Lessons OFF;
GO

-- =====================================================================
-- 8) LESSON CONTENTS WITH REAL VIDEO FILES
-- =====================================================================
PRINT 'Seeding Lesson Contents with Real Videos...';

-- Using actual video files from: wwwroot/uploads/videos/2025/11/
-- 8 video files available (EXACT NAMES - NO CHANGES):
-- 1. 34984d26b31e4d1997ab96a3654f5b5c.mp4 (9.4 MB)
-- 2. 4b0780d63ad140a9bdce94d278d6d59a.mp4 (716 KB)
-- 3. a48e346170174c5f9f8c49a6c4e4f426.mp4 (716 KB)
-- 4. bfb7e92a2c2e4d42bd420eb341152273.mp4 (9.6 MB)
-- 5. d3feb84c9b614b8e8d82bcf097586eda.mp4 (9.6 MB)
-- 6. d8abe973a6654aa69bbd35c5f103c31c.mp4 (27.3 MB)
-- 7. e20e46a25e6d4107bcbcf90164ed72bc.mp4 (613 KB)
-- 8. e7c217eee8314be09adf33c6a18c200e.mp4 (9.6 MB)

SET IDENTITY_INSERT dbo.LessonContents ON;
GO

-- Course 1 - SQL Server Videos
MERGE INTO dbo.LessonContents AS target
USING (VALUES
    -- Lesson 1: SQL Server Overview
    (1, 1, 'Video', N'Video giới thiệu SQL Server', NULL, '/uploads/videos/2025/11/34984d26b31e4d1997ab96a3654f5b5c.mp4', NULL, 1),
    (2, 1, 'Theory', N'Lý thuyết: SQL Server là gì?', N'<h3>SQL Server là gì?</h3><p>SQL Server là hệ quản trị cơ sở dữ liệu quan hệ (RDBMS) của Microsoft. Đây là một trong những hệ quản trị CSDL phổ biến nhất thế giới.</p><ul><li>Quản lý dữ liệu hiệu quả</li><li>Hỗ trợ transaction processing</li><li>Bảo mật cao</li><li>Khả năng mở rộng tốt</li></ul>', NULL, NULL, 2),
    
    -- Lesson 2: Database Architecture
    (3, 2, 'Video', N'Kiến trúc Database', NULL, '/uploads/videos/2025/11/4b0780d63ad140a9bdce94d278d6d59a.mp4', NULL, 1),
    (4, 2, 'Theory', N'Tables, Columns, và Relationships', N'<h3>Cấu trúc cơ sở dữ liệu</h3><p>Cơ sở dữ liệu bao gồm các bảng (tables), mỗi bảng có các cột (columns) và hàng (rows).</p><ul><li><strong>Tables:</strong> Lưu trữ dữ liệu theo dạng bảng</li><li><strong>Columns:</strong> Định nghĩa kiểu dữ liệu</li><li><strong>Primary Key:</strong> Khóa chính duy nhất</li><li><strong>Foreign Key:</strong> Liên kết giữa các bảng</li></ul>', NULL, NULL, 2),
    
    -- Lesson 3: SELECT Statement
    (5, 3, 'Video', N'SELECT Statement Tutorial', NULL, '/uploads/videos/2025/11/a48e346170174c5f9f8c49a6c4e4f426.mp4', NULL, 1),
    (6, 3, 'Theory', N'Cú pháp SELECT', N'<h3>SELECT Statement</h3><p>Lệnh SELECT dùng để truy vấn dữ liệu từ bảng.</p><pre><code>SELECT column1, column2 FROM table_name;</code></pre><p>Ví dụ:</p><pre><code>SELECT * FROM Users;\nSELECT Username, Email FROM Users;</code></pre>', NULL, NULL, 2),
    
    -- Lesson 4: WHERE Clause
    (7, 4, 'Video', N'WHERE Clause Explained', NULL, '/uploads/videos/2025/11/bfb7e92a2c2e4d42bd420eb341152273.mp4', NULL, 1),
    (8, 4, 'Theory', N'Filtering Data với WHERE', N'<h3>WHERE Clause</h3><p>WHERE được sử dụng để lọc dữ liệu theo điều kiện.</p><pre><code>SELECT * FROM Users WHERE Status = 1;\nSELECT * FROM Courses WHERE Price > 100000;</code></pre>', NULL, NULL, 2),
    
    -- Lesson 5: HTML Basics
    (9, 5, 'Video', N'HTML Cơ bản Tutorial', NULL, '/uploads/videos/2025/11/d3feb84c9b614b8e8d82bcf097586eda.mp4', NULL, 1),
    (10, 5, 'Theory', N'HTML Tags và Structure', N'<h3>HTML Tags</h3><p>HTML sử dụng các thẻ để định nghĩa cấu trúc nội dung.</p><ul><li>&lt;html&gt; - Thẻ gốc</li><li>&lt;head&gt; - Thông tin metadata</li><li>&lt;body&gt; - Nội dung trang</li><li>&lt;h1&gt;-&lt;h6&gt; - Tiêu đề</li><li>&lt;p&gt; - Đoạn văn</li></ul>', NULL, NULL, 2),
    
    -- Lesson 6: HTML Forms
    (11, 6, 'Video', N'HTML Forms Tutorial', NULL, '/uploads/videos/2025/11/d8abe973a6654aa69bbd35c5f103c31c.mp4', NULL, 1),
    (12, 6, 'Theory', N'Creating Forms in HTML', N'<h3>HTML Forms</h3><p>Forms cho phép thu thập thông tin từ người dùng.</p><pre><code>&lt;form action="/submit" method="post"&gt;\n  &lt;input type="text" name="username"&gt;\n  &lt;input type="email" name="email"&gt;\n  &lt;button type="submit"&gt;Submit&lt;/button&gt;\n&lt;/form&gt;</code></pre>', NULL, NULL, 2),
    
    -- Lesson 7: CSS Basics
    (13, 7, 'Video', N'CSS Basics Tutorial', NULL, '/uploads/videos/2025/11/e20e46a25e6d4107bcbcf90164ed72bc.mp4', NULL, 1),
    (14, 7, 'Theory', N'CSS Selectors và Properties', N'<h3>CSS Basics</h3><p>CSS dùng để tạo kiểu và định dạng cho HTML.</p><pre><code>/* Selector */\nh1 {\n  color: blue;\n  font-size: 24px;\n}\n\n.container {\n  width: 100%;\n  margin: 0 auto;\n}</code></pre>', NULL, NULL, 2),
    
    -- Lesson 8: CSS Layout
    (15, 8, 'Video', N'CSS Layout với Flexbox', NULL, '/uploads/videos/2025/11/e7c217eee8314be09adf33c6a18c200e.mp4', NULL, 1),
    (16, 8, 'Theory', N'Modern CSS Layout Techniques', N'<h3>CSS Layout</h3><p>Flexbox và Grid là hai phương pháp layout hiện đại.</p><pre><code>/* Flexbox */\n.container {\n  display: flex;\n  justify-content: center;\n  align-items: center;\n}\n\n/* Grid */\n.grid {\n  display: grid;\n  grid-template-columns: repeat(3, 1fr);\n}</code></pre>', NULL, NULL, 2)
) AS source (ContentId, LessonId, ContentType, Title, Body, VideoUrl, RefId, OrderIndex)
ON target.ContentId = source.ContentId
WHEN NOT MATCHED THEN
    INSERT (ContentId, LessonId, ContentType, Title, Body, VideoUrl, RefId, OrderIndex)
    VALUES (source.ContentId, source.LessonId, source.ContentType, source.Title, source.Body, 
            source.VideoUrl, source.RefId, source.OrderIndex);
GO

SET IDENTITY_INSERT dbo.LessonContents OFF;
GO

-- =====================================================================
-- 9) FLASHCARD SETS & FLASHCARDS
-- =====================================================================
PRINT 'Seeding Flashcard Sets...';

SET IDENTITY_INSERT dbo.FlashcardSets ON;
GO

MERGE INTO dbo.FlashcardSets AS target
USING (VALUES
    (1, 2, N'SQL Cơ bản - Từ vựng quan trọng', N'Các thuật ngữ và khái niệm quan trọng trong SQL', 'Public', 'vi', 'sql, database, basic'),
    (2, 2, N'HTML/CSS Quick Reference', N'Các thẻ HTML và CSS properties thường dùng', 'Public', 'vi', 'web, html, css')
) AS source (SetId, OwnerId, Title, Description, Visibility, Language, TagsText)
ON target.SetId = source.SetId
WHEN NOT MATCHED THEN
    INSERT (SetId, OwnerId, Title, Description, Visibility, Language, TagsText)
    VALUES (source.SetId, source.OwnerId, source.Title, source.Description, source.Visibility, 
            source.Language, source.TagsText);
GO

SET IDENTITY_INSERT dbo.FlashcardSets OFF;
GO

SET IDENTITY_INSERT dbo.Flashcards ON;
GO

MERGE INTO dbo.Flashcards AS target
USING (VALUES
    -- Set 1: SQL Basics
    (1, 1, N'SELECT là gì?', N'Lệnh truy vấn dữ liệu từ bảng', N'Dùng để đọc dữ liệu', 1),
    (2, 1, N'INSERT là gì?', N'Lệnh thêm bản ghi mới vào bảng', N'Thêm dữ liệu', 2),
    (3, 1, N'UPDATE là gì?', N'Lệnh cập nhật dữ liệu hiện có', N'Sửa dữ liệu', 3),
    (4, 1, N'DELETE là gì?', N'Lệnh xóa bản ghi khỏi bảng', N'Xóa dữ liệu', 4),
    (5, 1, N'WHERE dùng để làm gì?', N'Lọc dữ liệu theo điều kiện', N'Điều kiện lọc', 5),
    -- Set 2: HTML/CSS
    (6, 2, N'<p> tag dùng để?', N'Tạo đoạn văn bản (paragraph)', NULL, 1),
    (7, 2, N'<div> tag dùng để?', N'Tạo container phân chia nội dung', NULL, 2),
    (8, 2, N'color property trong CSS?', N'Đặt màu cho text', NULL, 3),
    (9, 2, N'display: flex là gì?', N'Kích hoạt Flexbox layout', NULL, 4)
) AS source (CardId, SetId, FrontText, BackText, Hint, OrderIndex)
ON target.CardId = source.CardId
WHEN NOT MATCHED THEN
    INSERT (CardId, SetId, FrontText, BackText, Hint, OrderIndex)
    VALUES (source.CardId, source.SetId, source.FrontText, source.BackText, source.Hint, source.OrderIndex);
GO

SET IDENTITY_INSERT dbo.Flashcards OFF;
GO

-- Link flashcard sets to lessons
INSERT INTO dbo.LessonContents (LessonId, ContentType, RefId, Title, OrderIndex)
SELECT 1, 'FlashcardSet', 1, N'Ôn tập từ vựng SQL', 3
WHERE NOT EXISTS (SELECT 1 FROM dbo.LessonContents WHERE LessonId = 1 AND ContentType = 'FlashcardSet' AND RefId = 1);

INSERT INTO dbo.LessonContents (LessonId, ContentType, RefId, Title, OrderIndex)
SELECT 5, 'FlashcardSet', 2, N'Ôn tập HTML/CSS', 3
WHERE NOT EXISTS (SELECT 1 FROM dbo.LessonContents WHERE LessonId = 5 AND ContentType = 'FlashcardSet' AND RefId = 2);
GO

-- =====================================================================
-- 10) TESTS & QUESTIONS
-- =====================================================================
PRINT 'Seeding Tests...';

SET IDENTITY_INSERT dbo.Tests ON;
GO

MERGE INTO dbo.Tests AS target
USING (VALUES
    (1, 2, N'Kiểm tra SQL cơ bản', N'Bài kiểm tra 5 câu về SQL fundamentals', 'Public', 'Auto', 600, 3, 5,
        DATEADD(DAY, -7, SYSUTCDATETIME()), DATEADD(DAY, 30, SYSUTCDATETIME())),
    (2, 2, N'Kiểm tra HTML/CSS', N'Bài kiểm tra 5 câu về HTML và CSS', 'Public', 'Auto', 600, 3, 5,
        DATEADD(DAY, -3, SYSUTCDATETIME()), DATEADD(DAY, 45, SYSUTCDATETIME()))
) AS source (TestId, OwnerId, Title, Description, Visibility, GradingMode, TimeLimitSec, MaxAttempts, MaxScore, OpenAt, CloseAt)
ON target.TestId = source.TestId
WHEN NOT MATCHED THEN
    INSERT (TestId, OwnerId, Title, Description, Visibility, GradingMode, TimeLimitSec, MaxAttempts, MaxScore, OpenAt, CloseAt)
    VALUES (source.TestId, source.OwnerId, source.Title, source.Description, source.Visibility, source.GradingMode,
            source.TimeLimitSec, source.MaxAttempts, source.MaxScore, source.OpenAt, source.CloseAt);
GO

SET IDENTITY_INSERT dbo.Tests OFF;
GO

-- Test 1: SQL Basics (5 questions)
SET IDENTITY_INSERT dbo.Questions ON;
GO

MERGE INTO dbo.Questions AS target
USING (VALUES
    (1, 1, 'MCQ_Single', N'Lệnh nào dùng để truy vấn dữ liệu?', 1, 1),
    (2, 1, 'MCQ_Single', N'Lệnh nào dùng để thêm dữ liệu?', 1, 2),
    (3, 1, 'TrueFalse', N'PRIMARY KEY có thể NULL', 1, 3),
    (4, 1, 'MCQ_Single', N'WHERE dùng để làm gì?', 1, 4),
    (5, 1, 'MCQ_Single', N'ORDER BY sắp xếp mặc định theo?', 1, 5),
    (6, 2, 'MCQ_Single', N'HTML là viết tắt của?', 1, 1),
    (7, 2, 'MCQ_Single', N'CSS là viết tắt của?', 1, 2),
    (8, 2, 'TrueFalse', N'<div> là thẻ block-level', 1, 3),
    (9, 2, 'MCQ_Single', N'Thuộc tính nào dùng để đổi màu text?', 1, 4),
    (10, 2, 'MCQ_Single', N'display: flex dùng để?', 1, 5)
) AS source (QuestionId, TestId, Type, StemText, Points, OrderIndex)
ON target.QuestionId = source.QuestionId
WHEN NOT MATCHED THEN
    INSERT (QuestionId, TestId, Type, StemText, Points, OrderIndex)
    VALUES (source.QuestionId, source.TestId, source.Type, source.StemText, source.Points, source.OrderIndex);
GO

SET IDENTITY_INSERT dbo.Questions OFF;
GO

SET IDENTITY_INSERT dbo.QuestionOptions ON;
GO

MERGE INTO dbo.QuestionOptions AS target
USING (VALUES
    -- Question 1
    (1, 1, N'SELECT', 1, 1), (2, 1, N'INSERT', 0, 2), (3, 1, N'UPDATE', 0, 3), (4, 1, N'DELETE', 0, 4),
    -- Question 2
    (5, 2, N'SELECT', 0, 1), (6, 2, N'INSERT', 1, 2), (7, 2, N'UPDATE', 0, 3), (8, 2, N'DELETE', 0, 4),
    -- Question 3
    (9, 3, N'Đúng', 0, 1), (10, 3, N'Sai', 1, 2),
    -- Question 4
    (11, 4, N'Lọc dữ liệu theo điều kiện', 1, 1), (12, 4, N'Sắp xếp dữ liệu', 0, 2), 
    (13, 4, N'Nhóm dữ liệu', 0, 3), (14, 4, N'Đếm số lượng', 0, 4),
    -- Question 5
    (15, 5, N'Ascending (Tăng dần)', 1, 1), (16, 5, N'Descending (Giảm dần)', 0, 2), 
    (17, 5, N'Random (Ngẫu nhiên)', 0, 3), (18, 5, N'Không sắp xếp', 0, 4),
    -- Question 6
    (19, 6, N'HyperText Markup Language', 1, 1), (20, 6, N'High Tech Modern Language', 0, 2), 
    (21, 6, N'Home Tool Markup Language', 0, 3),
    -- Question 7
    (22, 7, N'Cascading Style Sheets', 1, 1), (23, 7, N'Computer Style Sheets', 0, 2), 
    (24, 7, N'Creative Style System', 0, 3),
    -- Question 8
    (25, 8, N'Đúng', 1, 1), (26, 8, N'Sai', 0, 2),
    -- Question 9
    (27, 9, N'color', 1, 1), (28, 9, N'text-color', 0, 2), (29, 9, N'font-color', 0, 3),
    -- Question 10
    (30, 10, N'Tạo Flexbox layout', 1, 1), (31, 10, N'Ẩn phần tử', 0, 2), (32, 10, N'Tạo Grid layout', 0, 3)
) AS source (OptionId, QuestionId, OptionText, IsCorrect, OrderIndex)
ON target.OptionId = source.OptionId
WHEN NOT MATCHED THEN
    INSERT (OptionId, QuestionId, OptionText, IsCorrect, OrderIndex)
    VALUES (source.OptionId, source.QuestionId, source.OptionText, source.IsCorrect, source.OrderIndex);
GO

SET IDENTITY_INSERT dbo.QuestionOptions OFF;
GO

-- Link tests to lessons
INSERT INTO dbo.LessonContents (LessonId, ContentType, RefId, Title, OrderIndex)
SELECT 4, 'Test', 1, N'Bài kiểm tra SQL', 4
WHERE NOT EXISTS (SELECT 1 FROM dbo.LessonContents WHERE LessonId = 4 AND ContentType = 'Test' AND RefId = 1);

INSERT INTO dbo.LessonContents (LessonId, ContentType, RefId, Title, OrderIndex)
SELECT 8, 'Test', 2, N'Bài kiểm tra HTML/CSS', 4
WHERE NOT EXISTS (SELECT 1 FROM dbo.LessonContents WHERE LessonId = 8 AND ContentType = 'Test' AND RefId = 2);
GO

-- =====================================================================
-- 11) SHOPPING CARTS
-- =====================================================================
PRINT 'Seeding Shopping Carts...';

SET IDENTITY_INSERT dbo.ShoppingCarts ON;
GO

MERGE INTO dbo.ShoppingCarts AS target
USING (VALUES
    (1, 5), (2, 6), (3, 7), (4, 8)
) AS source (CartId, UserId)
ON target.CartId = source.CartId
WHEN NOT MATCHED THEN
    INSERT (CartId, UserId)
    VALUES (source.CartId, source.UserId);
GO

SET IDENTITY_INSERT dbo.ShoppingCarts OFF;
GO

-- =====================================================================
-- 12) ORDERS & COURSE PURCHASES
-- =====================================================================
PRINT 'Seeding Orders & Purchases...';

SET IDENTITY_INSERT dbo.Orders ON;
GO

MERGE INTO dbo.Orders AS target
USING (VALUES
    (1, 5, 299000, 'VND', 'Paid', DATEADD(DAY, -10, SYSUTCDATETIME())),
    (2, 6, 349000, 'VND', 'Paid', DATEADD(DAY, -8, SYSUTCDATETIME())),
    (3, 7, 299000, 'VND', 'Paid', DATEADD(DAY, -5, SYSUTCDATETIME()))
) AS source (OrderId, BuyerId, TotalAmount, Currency, Status, PaidAt)
ON target.OrderId = source.OrderId
WHEN NOT MATCHED THEN
    INSERT (OrderId, BuyerId, TotalAmount, Currency, Status, PaidAt)
    VALUES (source.OrderId, source.BuyerId, source.TotalAmount, source.Currency, source.Status, source.PaidAt);
GO

SET IDENTITY_INSERT dbo.Orders OFF;
GO

-- Order items
SET IDENTITY_INSERT dbo.OrderItems ON;
GO

MERGE INTO dbo.OrderItems AS target
USING (VALUES
    (1, 1, 1, 299000),
    (2, 2, 2, 349000),
    (3, 3, 1, 299000)
) AS source (ItemId, OrderId, CourseId, Price)
ON target.ItemId = source.ItemId
WHEN NOT MATCHED THEN
    INSERT (ItemId, OrderId, CourseId, Price)
    VALUES (source.ItemId, source.OrderId, source.CourseId, source.Price);
GO

SET IDENTITY_INSERT dbo.OrderItems OFF;
GO

-- Payments
SET IDENTITY_INSERT dbo.Payments ON;
GO

MERGE INTO dbo.Payments AS target
USING (VALUES
    (1, 1, 'VNPay', 'VNP123456789', 299000, 'VND', 'Paid', DATEADD(DAY, -10, SYSUTCDATETIME())),
    (2, 2, 'MoMo', 'MOMO987654321', 349000, 'VND', 'Paid', DATEADD(DAY, -8, SYSUTCDATETIME())),
    (3, 3, 'VNPay', 'VNP234567890', 299000, 'VND', 'Paid', DATEADD(DAY, -5, SYSUTCDATETIME()))
) AS source (PaymentId, OrderId, Provider, ProviderRef, Amount, Currency, Status, PaidAt)
ON target.PaymentId = source.PaymentId
WHEN NOT MATCHED THEN
    INSERT (PaymentId, OrderId, Provider, ProviderRef, Amount, Currency, Status, PaidAt)
    VALUES (source.PaymentId, source.OrderId, source.Provider, source.ProviderRef, 
            source.Amount, source.Currency, source.Status, source.PaidAt);
GO

SET IDENTITY_INSERT dbo.Payments OFF;
GO

-- Course purchases (legacy table)
SET IDENTITY_INSERT dbo.CoursePurchases ON;
GO

MERGE INTO dbo.CoursePurchases AS target
USING (VALUES
    (1, 1, 5, 299000, 'VND', 'Paid', DATEADD(DAY, -10, SYSUTCDATETIME())),
    (2, 2, 6, 349000, 'VND', 'Paid', DATEADD(DAY, -8, SYSUTCDATETIME())),
    (3, 1, 7, 299000, 'VND', 'Paid', DATEADD(DAY, -5, SYSUTCDATETIME()))
) AS source (PurchaseId, CourseId, BuyerId, PricePaid, Currency, Status, PurchasedAt)
ON target.PurchaseId = source.PurchaseId
WHEN NOT MATCHED THEN
    INSERT (PurchaseId, CourseId, BuyerId, PricePaid, Currency, Status, PurchasedAt)
    VALUES (source.PurchaseId, source.CourseId, source.BuyerId, source.PricePaid, 
            source.Currency, source.Status, source.PurchasedAt);
GO

SET IDENTITY_INSERT dbo.CoursePurchases OFF;
GO

-- =====================================================================
-- 13) COURSE PROGRESS
-- =====================================================================
PRINT 'Seeding Course Progress...';

-- Student 1 (UserId 5) progress on Course 1
MERGE INTO dbo.CourseProgress AS target
USING (VALUES
    (5, 1, 1, 'Video', 1, 1, DATEADD(DAY, -9, SYSUTCDATETIME()), 320),
    (5, 1, 1, 'Theory', 2, 1, DATEADD(DAY, -9, SYSUTCDATETIME()), 180),
    (5, 1, 2, 'Video', 3, 1, DATEADD(DAY, -8, SYSUTCDATETIME()), 340),
    (5, 1, 3, 'Video', 5, 1, DATEADD(DAY, -7, SYSUTCDATETIME()), 380)
) AS source (UserId, CourseId, LessonId, ContentType, ContentId, IsCompleted, CompletionAt, DurationSec)
ON target.UserId = source.UserId 
   AND target.CourseId = source.CourseId 
   AND target.ContentId = source.ContentId
WHEN NOT MATCHED THEN
    INSERT (UserId, CourseId, LessonId, ContentType, ContentId, IsCompleted, CompletionAt, DurationSec)
    VALUES (source.UserId, source.CourseId, source.LessonId, source.ContentType, 
            source.ContentId, source.IsCompleted, source.CompletionAt, source.DurationSec);
GO

-- Student 2 (UserId 6) progress on Course 2
MERGE INTO dbo.CourseProgress AS target
USING (VALUES
    (6, 2, 5, 'Video', 9, 1, DATEADD(DAY, -7, SYSUTCDATETIME()), 300),
    (6, 2, 5, 'Theory', 10, 1, DATEADD(DAY, -7, SYSUTCDATETIME()), 150),
    (6, 2, 6, 'Video', 11, 1, DATEADD(DAY, -6, SYSUTCDATETIME()), 330)
) AS source (UserId, CourseId, LessonId, ContentType, ContentId, IsCompleted, CompletionAt, DurationSec)
ON target.UserId = source.UserId 
   AND target.CourseId = source.CourseId 
   AND target.ContentId = source.ContentId
WHEN NOT MATCHED THEN
    INSERT (UserId, CourseId, LessonId, ContentType, ContentId, IsCompleted, CompletionAt, DurationSec)
    VALUES (source.UserId, source.CourseId, source.LessonId, source.ContentType, 
            source.ContentId, source.IsCompleted, source.CompletionAt, source.DurationSec);
GO

-- =====================================================================
-- 14) TEST ATTEMPTS
-- =====================================================================
PRINT 'Seeding Test Attempts...';

SET IDENTITY_INSERT dbo.TestAttempts ON;
GO

MERGE INTO dbo.TestAttempts AS target
USING (VALUES
    (1, 1, 5, DATEADD(DAY, -6, SYSUTCDATETIME()), DATEADD(DAY, -6, DATEADD(MINUTE, 8, SYSUTCDATETIME())), 'Graded', 480, 4.0, 5),
    (2, 2, 6, DATEADD(DAY, -5, SYSUTCDATETIME()), DATEADD(DAY, -5, DATEADD(MINUTE, 7, SYSUTCDATETIME())), 'Graded', 420, 4.5, 5)
) AS source (AttemptId, TestId, UserId, StartedAt, SubmittedAt, Status, TimeSpentSec, Score, MaxScore)
ON target.AttemptId = source.AttemptId
WHEN NOT MATCHED THEN
    INSERT (AttemptId, TestId, UserId, StartedAt, SubmittedAt, Status, TimeSpentSec, Score, MaxScore)
    VALUES (source.AttemptId, source.TestId, source.UserId, source.StartedAt, source.SubmittedAt, 
            source.Status, source.TimeSpentSec, source.Score, source.MaxScore);
GO

SET IDENTITY_INSERT dbo.TestAttempts OFF;
GO

-- =====================================================================
-- 15) COURSE REVIEWS
-- =====================================================================
PRINT 'Seeding Course Reviews...';

SET IDENTITY_INSERT dbo.CourseReviews ON;
GO

MERGE INTO dbo.CourseReviews AS target
USING (VALUES
    (1, 1, 5, 5.0, N'Khóa học rất chi tiết và dễ hiểu. Video rất rõ ràng!', 1),
    (2, 1, 7, 4.5, N'Nội dung hay, giảng viên giải thích tốt.', 1),
    (3, 2, 6, 5.0, N'Perfect cho người bắt đầu học web development!', 1)
) AS source (ReviewId, CourseId, UserId, Rating, Comment, IsApproved)
ON target.ReviewId = source.ReviewId
WHEN NOT MATCHED THEN
    INSERT (ReviewId, CourseId, UserId, Rating, Comment, IsApproved)
    VALUES (source.ReviewId, source.CourseId, source.UserId, source.Rating, source.Comment, source.IsApproved);
GO

SET IDENTITY_INSERT dbo.CourseReviews OFF;
GO

-- Update course ratings
UPDATE dbo.Courses 
SET AverageRating = 4.75, TotalReviews = 2
WHERE CourseId = 1;

UPDATE dbo.Courses 
SET AverageRating = 5.0, TotalReviews = 1
WHERE CourseId = 2;
GO

-- =====================================================================
-- 16) NOTIFICATIONS
-- =====================================================================
PRINT 'Seeding Notifications...';

MERGE INTO dbo.Notifications AS target
USING (VALUES
    (5, 'course_enrolled', N'Đăng ký khóa học thành công', N'Bạn đã đăng ký thành công khóa học "SQL Server từ cơ bản đến nâng cao"', 1),
    (5, 'test_completed', N'Hoàn thành bài kiểm tra', N'Bạn đã hoàn thành bài kiểm tra "Kiểm tra SQL cơ bản" với điểm 4.0/5', 1),
    (6, 'course_enrolled', N'Đăng ký khóa học thành công', N'Bạn đã đăng ký thành công khóa học "Phát triển Web với HTML, CSS, JavaScript"', 1),
    (6, 'test_completed', N'Hoàn thành bài kiểm tra', N'Bạn đã hoàn thành bài kiểm tra "Kiểm tra HTML/CSS" với điểm 4.5/5', 0),
    (2, 'system', N'Khóa học của bạn được đánh giá 5 sao', N'Học viên Nguyễn Lan Anh vừa đánh giá 5 sao cho khóa học "SQL Server từ cơ bản đến nâng cao"', 0)
) AS source (UserId, Type, Title, Body, IsRead)
ON 1=0  -- Always insert notifications
WHEN NOT MATCHED THEN
    INSERT (UserId, Type, Title, Body, IsRead)
    VALUES (source.UserId, source.Type, source.Title, source.Body, source.IsRead);
GO

-- =====================================================================
-- 17) USER INTERESTS
-- =====================================================================
PRINT 'Seeding User Interests...';

MERGE INTO dbo.UserInterests AS target
USING (VALUES
    (5, 2), (6, 4), (7, 1), (7, 2)
) AS source (UserId, CategoryId)
ON target.UserId = source.UserId AND target.CategoryId = source.CategoryId
WHEN NOT MATCHED THEN
    INSERT (UserId, CategoryId)
    VALUES (source.UserId, source.CategoryId);
GO

-- =====================================================================
-- VERIFICATION & SUMMARY
-- =====================================================================
PRINT '';
PRINT '========================================';
PRINT 'SEED DATA WITH REAL VIDEOS COMPLETED!';
PRINT '========================================';
PRINT '';

PRINT 'Summary of seeded data:';
SELECT 'Users' AS TableName, COUNT(*) AS RecordCount FROM dbo.Users
UNION ALL SELECT 'Courses', COUNT(*) FROM dbo.Courses
UNION ALL SELECT 'CourseCategories', COUNT(*) FROM dbo.CourseCategories
UNION ALL SELECT 'CourseChapters', COUNT(*) FROM dbo.CourseChapters
UNION ALL SELECT 'Lessons', COUNT(*) FROM dbo.Lessons
UNION ALL SELECT 'LessonContents (Total)', COUNT(*) FROM dbo.LessonContents
UNION ALL SELECT 'LessonContents (Video)', COUNT(*) FROM dbo.LessonContents WHERE ContentType = 'Video'
UNION ALL SELECT 'LessonContents (Theory)', COUNT(*) FROM dbo.LessonContents WHERE ContentType = 'Theory'
UNION ALL SELECT 'FlashcardSets', COUNT(*) FROM dbo.FlashcardSets
UNION ALL SELECT 'Flashcards', COUNT(*) FROM dbo.Flashcards
UNION ALL SELECT 'Tests', COUNT(*) FROM dbo.Tests
UNION ALL SELECT 'Questions', COUNT(*) FROM dbo.Questions
UNION ALL SELECT 'QuestionOptions', COUNT(*) FROM dbo.QuestionOptions
UNION ALL SELECT 'Orders', COUNT(*) FROM dbo.Orders
UNION ALL SELECT 'OrderItems', COUNT(*) FROM dbo.OrderItems
UNION ALL SELECT 'Payments', COUNT(*) FROM dbo.Payments
UNION ALL SELECT 'CoursePurchases', COUNT(*) FROM dbo.CoursePurchases
UNION ALL SELECT 'CourseProgress', COUNT(*) FROM dbo.CourseProgress
UNION ALL SELECT 'TestAttempts', COUNT(*) FROM dbo.TestAttempts
UNION ALL SELECT 'CourseReviews', COUNT(*) FROM dbo.CourseReviews
UNION ALL SELECT 'Notifications', COUNT(*) FROM dbo.Notifications
UNION ALL SELECT 'UserInterests', COUNT(*) FROM dbo.UserInterests;
GO

PRINT '';
PRINT '=== VIDEO FILES MAPPING (EXACT FILENAMES) ===';
PRINT 'All videos from: wwwroot/uploads/videos/2025/11/';
PRINT '';
PRINT 'Course 1 - SQL Server (4 videos):';
PRINT '  Lesson 1: 34984d26b31e4d1997ab96a3654f5b5c.mp4 (9.4 MB)';
PRINT '  Lesson 2: 4b0780d63ad140a9bdce94d278d6d59a.mp4 (716 KB)';
PRINT '  Lesson 3: a48e346170174c5f9f8c49a6c4e4f426.mp4 (716 KB)';
PRINT '  Lesson 4: bfb7e92a2c2e4d42bd420eb341152273.mp4 (9.6 MB)';
PRINT '';
PRINT 'Course 2 - Web Development (4 videos):';
PRINT '  Lesson 5: d3feb84c9b614b8e8d82bcf097586eda.mp4 (9.6 MB)';
PRINT '  Lesson 6: d8abe973a6654aa69bbd35c5f103c31c.mp4 (27.3 MB)';
PRINT '  Lesson 7: e20e46a25e6d4107bcbcf90164ed72bc.mp4 (613 KB)';
PRINT '  Lesson 8: e7c217eee8314be09adf33c6a18c200e.mp4 (9.6 MB)';
PRINT '';
PRINT '=== LOGIN CREDENTIALS ===';
PRINT 'Admin:   admin@ymedu.vn / password123';
PRINT 'Teacher: teacher1@ymedu.vn / password123';
PRINT 'Student: student1@ymedu.vn / password123';
PRINT '';
PRINT 'Note: All video paths use EXACT filenames from the uploads folder.';
PRINT 'Video URL format: /uploads/videos/2025/11/{filename}.mp4';
PRINT '';
PRINT '=== IMPORTANT ===';
PRINT 'Make sure video files exist at:';
PRINT 'Quiz_Web\wwwroot\uploads\videos\2025\11\';
PRINT '';
GO
