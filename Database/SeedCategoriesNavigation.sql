-- ===================================================
-- Sample Data for Course Categories Navigation
-- Use this to test the category navigation bar
-- ===================================================

-- Clear existing test data if needed (CAREFUL!)
-- DELETE FROM dbo.CourseCategories WHERE CategoryId > 4;

-- Insert sample categories with proper display order
INSERT INTO dbo.CourseCategories (Name, Slug, Description, DisplayOrder)
VALUES
(N'Phát tri?n', 'phat-trien', N'Các khóa h?c v? l?p trình và phát tri?n ph?n m?m', 1),
(N'Kinh doanh', 'kinh-doanh', N'K? n?ng kinh doanh và qu?n lý', 2),
(N'Tài chính & K? toán', 'tai-chinh-ke-toan', N'K? toán, tài chính cá nhân và ??u t?', 3),
(N'CNTT & Ph?n m?m', 'cntt-phan-mem', N'Công ngh? thông tin và ph?n m?m', 4),
(N'N?ng su?t v?n phòng', 'nang-suat-van-phong', N'Microsoft Office, Google Workspace', 5),
(N'Phát tri?n cá nhân', 'phat-trien-ca-nhan', N'K? n?ng m?m và phát tri?n b?n thân', 6),
(N'Thi?t k?', 'thiet-ke', N'Thi?t k? ?? h?a, UI/UX, web design', 7),
(N'Marketing', 'marketing', N'Digital marketing, SEO, qu?ng cáo', 8),
(N'S?c kh?e & Th? d?c', 'suc-khoe-the-duc', N'Yoga, fitness, dinh d??ng', 9),
(N'Âm nh?c', 'am-nhac', N'H?c nh?c c? và lý thuy?t âm nh?c', 10);

-- Update existing categories if they exist
UPDATE dbo.CourseCategories 
SET DisplayOrder = 1
WHERE Name = N'L?p trình' AND DisplayOrder = 0;

UPDATE dbo.CourseCategories 
SET DisplayOrder = 2
WHERE Name = N'C? s? d? li?u' AND DisplayOrder = 0;

UPDATE dbo.CourseCategories 
SET DisplayOrder = 3
WHERE Name = N'Phân tích d? li?u' AND DisplayOrder = 0;

UPDATE dbo.CourseCategories 
SET DisplayOrder = 4
WHERE Name = N'Trí tu? nhân t?o' AND DisplayOrder = 0;

-- Add icons (optional)
UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/code-slash.svg'
WHERE Slug = 'phat-trien';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/briefcase.svg'
WHERE Slug = 'kinh-doanh';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/calculator.svg'
WHERE Slug = 'tai-chinh-ke-toan';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/laptop.svg'
WHERE Slug = 'cntt-phan-mem';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/file-earmark-text.svg'
WHERE Slug = 'nang-suat-van-phong';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/person-check.svg'
WHERE Slug = 'phat-trien-ca-nhan';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/palette.svg'
WHERE Slug = 'thiet-ke';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/megaphone.svg'
WHERE Slug = 'marketing';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/heart-pulse.svg'
WHERE Slug = 'suc-khoe-the-duc';

UPDATE dbo.CourseCategories
SET IconUrl = 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/icons/music-note-beamed.svg'
WHERE Slug = 'am-nhac';

-- Verify the data
SELECT CategoryId, Name, Slug, DisplayOrder, IconUrl
FROM dbo.CourseCategories
ORDER BY DisplayOrder, Name;

-- Check total count
SELECT COUNT(*) AS TotalCategories FROM dbo.CourseCategories;
