/* ===================================
   INDEX.JS - FLASHCARD LEARNING LOGIC
   ================================== */

// Global variables
let flashcards = [];
let currentCardIndex = 0;
let isFlipped = false;
let totalCards = 0;
let currentSetId = 0;
let courseLinkData = null; // Store course link info

/**
 * Initialize flashcards with data
 * @param {Array} data - Array of flashcard objects with FrontText and BackText
 * @param {Number} total - Total number of flashcards
 * @param {Number} setId - Current flashcard set ID
 * @param {Object} linkData - Course link data {courseSlug, lessonId, contentId}
 */
function initializeFlashcards(data, total, setId, linkData) {
    console.log('Initializing flashcards with:', { data, total, setId, linkData });
    
    flashcards = data || [];
    totalCards = total || 0;
    currentCardIndex = 0;
    isFlipped = false;
    currentSetId = setId || 0;
    courseLinkData = linkData || null;
    
    console.log('Flashcards initialized:', flashcards.length, 'cards');
    console.log('First card:', flashcards.length > 0 ? flashcards[0] : 'No cards');
    
    if (flashcards.length > 0) {
        updateCard();
    } else {
        console.error('No flashcards to display');
        const frontText = document.getElementById('frontText');
        const backText = document.getElementById('backText');
        if (frontText) frontText.textContent = 'Không có th? nào';
        if (backText) backText.textContent = 'B? flashcard tr?ng';
    }
}

/**
 * Update the current card display
 */
function updateCard() {
    const frontText = document.getElementById('frontText');
    const backText = document.getElementById('backText');
    
    if (!frontText || !backText) {
        console.error('Card text elements not found');
        return;
    }
    
    if (flashcards.length === 0) {
        frontText.textContent = 'Không có th? nào';
        backText.textContent = 'B? flashcard tr?ng';
        return;
    }
    
    const currentCard = flashcards[currentCardIndex];
    console.log('Current card index:', currentCardIndex, 'Card data:', currentCard);
    
    // Handle both property name formats (FrontText/frontText)
    frontText.textContent = currentCard.FrontText || currentCard.frontText || 'Không có n?i dung';
    backText.textContent = currentCard.BackText || currentCard.backText || 'Không có n?i dung';
    
    // Reset flip state
    const card = document.getElementById('flashcard');
    if (card && isFlipped) {
        card.classList.remove('flipped');
        isFlipped = false;
    }
    
    updateCounter();
    updateProgress();
    updateNavigationButtons();
    checkCompletion();
}

/**
 * Update navigation button states
 */
function updateNavigationButtons() {
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');
    
    if (prevBtn) {
        prevBtn.disabled = currentCardIndex === 0;
    }
    
    if (nextBtn) {
        nextBtn.disabled = currentCardIndex >= flashcards.length - 1;
    }
}

/**
 * Check if all flashcards are completed and show completion button
 */
function checkCompletion() {
    const progress = ((currentCardIndex + 1) / totalCards) * 100;
    const completeSection = document.getElementById('completeSection');
    
    if (completeSection) {
        if (progress >= 100) {
            completeSection.style.display = 'block';
            completeSection.classList.add('show-complete');
        } else {
            completeSection.style.display = 'none';
            completeSection.classList.remove('show-complete');
        }
    }
}

/**
 * Navigate to finish page
 */
function goToFinish() {
    let url = `/flashcards/finish/${currentSetId}`;
    
    // Add course link params if available
    if (courseLinkData && courseLinkData.courseSlug && courseLinkData.lessonId && courseLinkData.contentId) {
        const params = new URLSearchParams({
            courseSlug: courseLinkData.courseSlug,
            lessonId: courseLinkData.lessonId,
            contentId: courseLinkData.contentId
        });
        url += `?${params.toString()}`;
    }
    
    window.location.href = url;
}

/**
 * Flip the current card
 */
function flipCard() {
    const card = document.getElementById('flashcard');
    if (card) {
        card.classList.toggle('flipped');
        isFlipped = !isFlipped;
    }
}

/**
 * Navigate to next card
 */
function nextCard() {
    if (currentCardIndex < flashcards.length - 1) {
        currentCardIndex++;
        updateCard();
    }
}

/**
 * Navigate to previous card
 */
function previousCard() {
    if (currentCardIndex > 0) {
        currentCardIndex--;
        updateCard();
    }
}

/**
 * Update card counter display
 */
function updateCounter() {
    const counter = document.getElementById('cardCounter');
    if (counter) {
        counter.textContent = `${currentCardIndex + 1} / ${totalCards} Flashcards`;
    }
}

/**
 * Update progress bar and percentage
 */
function updateProgress() {
    const progress = ((currentCardIndex + 1) / totalCards) * 100;
    const progressFill = document.getElementById('progressFill');
    const progressText = document.getElementById('progressText');
    
    if (progressFill) {
        progressFill.style.width = progress + '%';
    }
    if (progressText) {
        progressText.textContent = Math.round(progress) + '%';
    }
}

// ===================================
// EVENT LISTENERS
// ===================================

// Keyboard navigation
document.addEventListener('keydown', function(event) {
    if (event.code === 'Space') {
        event.preventDefault();
        flipCard();
    } else if (event.code === 'ArrowLeft') {
        event.preventDefault();
        previousCard();
    } else if (event.code === 'ArrowRight') {
        event.preventDefault();
        nextCard();
    }
});

// Click to flip card
document.addEventListener('DOMContentLoaded', function() {
    const flashcard = document.getElementById('flashcard');
    if (flashcard) {
        flashcard.addEventListener('click', function(event) {
            flipCard();
        });
    }
});
