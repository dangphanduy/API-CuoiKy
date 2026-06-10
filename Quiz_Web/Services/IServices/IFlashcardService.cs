using Quiz_Web.Models.Entities;
using Quiz_Web.Models.ViewModels;

namespace Quiz_Web.Services.IServices
{
    public interface IFlashcardService
    {
        // Existing methods
        Task<FlashcardSet?> GetFlashcardSetByIdAsync(int setId);
        Task<List<Flashcard>> GetFlashcardsBySetIdAsync(int setId);
        Task<bool> FlashcardSetExistsAsync(int setId);

        // FlashcardSet CRUD
        List<FlashcardSet> GetAllPublishedFlashcardSets();
        List<FlashcardSet> GetFlashcardSetsByOwner(int ownerId);
        FlashcardSet? GetFlashcardSetById(int setId);
        FlashcardSet? GetOwnedFlashcardSet(int setId, int ownerId);
        List<FlashcardSet> SearchFlashcardSets(string keyword);
        FlashcardSet? CreateFlashcardSet(CreateFlashcardSetViewModel model, int ownerId);
        FlashcardSet? UpdateFlashcardSet(EditFlashcardSetViewModel model, int ownerId);
        bool DeleteFlashcardSet(int setId, int ownerId, string? webRootPath);

        // Flashcard CRUD
        List<Flashcard> GetFlashcardsBySetId(int setId);
        Flashcard? GetFlashcardById(int cardId);
        Flashcard? CreateFlashcard(CreateFlashcardViewModel model, int ownerId);
        Flashcard? UpdateFlashcard(EditFlashcardViewModel model, int ownerId);
        bool DeleteFlashcard(int cardId, int ownerId);
        bool DeleteAllFlashcardsInSet(int setId, int ownerId);
        Task<IEnumerable<FlashcardSet>> GetPublicFlashcardSetsAsync();
    }
}
