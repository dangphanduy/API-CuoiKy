-- Check all flashcard sets and their flashcards
SELECT 
    fs.SetId,
    fs.Title,
    fs.OwnerId,
    fs.Visibility,
    COUNT(f.CardId) AS FlashcardCount
FROM FlashcardSets fs
LEFT JOIN Flashcards f ON fs.SetId = f.SetId
GROUP BY fs.SetId, fs.Title, fs.OwnerId, fs.Visibility
ORDER BY fs.SetId;

-- Check flashcards for Set ID 3 specifically
SELECT 
    CardId,
    SetId,
    LEFT(FrontText, 50) AS FrontText_Preview,
    LEFT(BackText, 50) AS BackText_Preview,
    OrderIndex
FROM Flashcards
WHERE SetId = 3
ORDER BY OrderIndex;

-- Check if Set ID 3 exists
SELECT * FROM FlashcardSets WHERE SetId = 3;
