using Quiz_Web.Models.ViewModels;

namespace Quiz_Web.Services.IServices
{
    public interface ILessonService
    {
        /// <summary>
        /// Nhận vào ViewModel từ Controller và ID của chủ sở hữu
        /// Trả về ID của Lesson mới được tạo
        /// </summary>
        //Task<int> CreateLessonAsync(LessonCreateViewModel viewModel, int ownerId);
    }
}
