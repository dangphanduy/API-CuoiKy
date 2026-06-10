(function() {
    'use strict';

    let slideCounter = 0;

    // Event Listeners
    document.addEventListener('DOMContentLoaded', function() {
        // Add slide buttons
        document.getElementById('add-flashcard-slide')?.addEventListener('click', () => addSlide('flashcard'));
        document.getElementById('add-mcq-slide')?.addEventListener('click', () => addSlide('mcq'));
        document.getElementById('add-truefalse-slide')?.addEventListener('click', () => addSlide('truefalse'));
        document.getElementById('add-shorttext-slide')?.addEventListener('click', () => addSlide('shorttext'));
        
        // Save button
        document.getElementById('save-lesson-button')?.addEventListener('click', saveLesson);
        
        // Initialize slide count
        updateSlideCount();
    });

    // Add Slide Function
    function addSlide(type) {
        slideCounter++;
        const container = document.getElementById('slide-container');
        
        let template;
        switch(type) {
            case 'flashcard':
                template = document.getElementById('flashcard-template');
                break;
            case 'mcq':
                template = document.getElementById('mcq-template');
                // Add 2 default options for MCQ
                setTimeout(() => {
                    const newSlide = container.lastElementChild;
                    addOption(newSlide.querySelector('.add-option-btn'));
                    addOption(newSlide.querySelector('.add-option-btn'));
                }, 0);
                break;
            case 'truefalse':
                template = document.getElementById('truefalse-template');
                break;
            case 'shorttext':
                template = document.getElementById('shorttext-template');
                break;
            default:
                console.error('Unknown slide type:', type);
                return;
        }
        
        if (!template) {
            console.error('Template not found for type:', type);
            return;
        }
        
        const clone = template.content.cloneNode(true);
        const slideElement = clone.querySelector('.slide-item');
        
        // Set slide number
        slideElement.querySelector('.slide-index').textContent = slideCounter;
        
        // Add remove slide event
        slideElement.querySelector('.remove-slide').addEventListener('click', function() {
            this.closest('.slide-item').remove();
            updateSlideNumbers();
            updateSlideCount();
        });
        
        // Add option button event for MCQ
        const addOptionBtn = slideElement.querySelector('.add-option-btn');
        if (addOptionBtn) {
            addOptionBtn.addEventListener('click', function() {
                addOption(this);
            });
        }
        
        // MCQ type change event
        const mcqTypeSelect = slideElement.querySelector('.mcq-type-select');
        if (mcqTypeSelect) {
            mcqTypeSelect.addEventListener('change', function() {
                const slideItem = this.closest('.slide-item');
                slideItem.dataset.type = this.value;
            });
        }
        
        container.appendChild(clone);
        updateSlideCount();
        
        // Scroll to new slide
        slideElement.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }

    // Add Option Function (for MCQ)
    function addOption(button) {
        const slideElement = button.closest('.slide-item');
        const optionsContainer = slideElement.querySelector('.options-container');
        const optionTemplate = document.getElementById('option-template');
        
        if (!optionTemplate) {
            console.error('Option template not found');
            return;
        }
        
        const clone = optionTemplate.content.cloneNode(true);
        const optionElement = clone.querySelector('.option-item');
        
        // Remove option event
        optionElement.querySelector('.remove-option-btn').addEventListener('click', function() {
            this.closest('.option-item').remove();
        });
        
        optionsContainer.appendChild(clone);
    }

    // Update Slide Numbers
    function updateSlideNumbers() {
        const slides = document.querySelectorAll('#slide-container .slide-item');
        slides.forEach((slide, index) => {
            slide.querySelector('.slide-index').textContent = index + 1;
        });
        slideCounter = slides.length;
    }

    // Update Slide Count
    function updateSlideCount() {
        const slides = document.querySelectorAll('#slide-container .slide-item');
        const countElement = document.getElementById('slide-count');
        if (countElement) {
            countElement.textContent = slides.length;
        }
    }

    // Save Lesson Function
    async function saveLesson() {
        try {
            // Get antiforgery token
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            // Create ViewModel
            const viewModel = {
                Title: document.getElementById('Title').value.trim(),
                Description: document.getElementById('Description').value.trim() || null,
                Visibility: document.getElementById('Visibility').value,
                CoverUrl: document.getElementById('CoverUrl').value.trim() || null,
                Slides: []
            };
            
            // Validate title
            if (!viewModel.Title) {
                alert('Vui lòng nhập tiêu đề bài học');
                document.getElementById('Title').focus();
                return;
            }
            
            // Collect slides
            const slideElements = document.querySelectorAll('#slide-container .slide-item');
            
            if (slideElements.length === 0) {
                alert('Vui lòng thêm ít nhất một slide');
                return;
            }
            
            slideElements.forEach((slideElement, index) => {
                const slideType = slideElement.dataset.type;
                const stemText = slideElement.querySelector('.stem-text-input').value.trim();
                const points = parseFloat(slideElement.querySelector('.points-input').value) || 1;
                
                if (!stemText) {
                    throw new Error(`Slide ${index + 1}: Vui lòng nhập nội dung câu hỏi`);
                }
                
                const slideVM = {
                    OrderIndex: index,
                    SlideType: slideType,
                    StemText: stemText,
                    Points: points,
                    Options: []
                };
                
                // Handle different slide types
                if (slideType === 'Flashcard') {
                    const backText = slideElement.querySelector('.back-text-input').value.trim();
                    if (!backText) {
                        throw new Error(`Slide ${index + 1}: Vui lòng nhập câu trả lời`);
                    }
                    slideVM.BackText = backText;
                }
                else if (slideType === 'MCQ_Single' || slideType === 'MCQ_Multi') {
                    const optionElements = slideElement.querySelectorAll('.option-item');
                    
                    if (optionElements.length < 2) {
                        throw new Error(`Slide ${index + 1}: Trắc nghiệm cần ít nhất 2 lựa chọn`);
                    }
                    
                    let hasCorrectAnswer = false;
                    optionElements.forEach((optionElement, optionIndex) => {
                        const optionText = optionElement.querySelector('.option-text-input').value.trim();
                        const isCorrect = optionElement.querySelector('.option-correct-checkbox').checked;
                        
                        if (!optionText) {
                            throw new Error(`Slide ${index + 1}: Lựa chọn ${optionIndex + 1} không được để trống`);
                        }
                        
                        if (isCorrect) hasCorrectAnswer = true;
                        
                        slideVM.Options.push({
                            OptionText: optionText,
                            IsCorrect: isCorrect,
                            OrderIndex: optionIndex
                        });
                    });
                    
                    if (!hasCorrectAnswer) {
                        throw new Error(`Slide ${index + 1}: Vui lòng chọn ít nhất một đáp án đúng`);
                    }
                }
                else if (slideType === 'TrueFalse') {
                    const trueFalseAnswer = slideElement.querySelector('.truefalse-answer').value;
                    slideVM.Options = [
                        { OptionText: 'Đúng', IsCorrect: trueFalseAnswer === 'true', OrderIndex: 0 },
                        { OptionText: 'Sai', IsCorrect: trueFalseAnswer === 'false', OrderIndex: 1 }
                    ];
                }
                else if (slideType === 'ShortText') {
                    const correctText = slideElement.querySelector('.correct-text-input').value.trim();
                    if (!correctText) {
                        throw new Error(`Slide ${index + 1}: Vui lòng nhập câu trả lời đúng`);
                    }
                    slideVM.CorrectText = correctText;
                }
                
                viewModel.Slides.push(slideVM);
            });
            
            // Show loading
            const saveButton = document.getElementById('save-lesson-button');
            const originalText = saveButton.innerHTML;
            saveButton.disabled = true;
            saveButton.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Đang lưu...';
            
            // Send request
            const response = await fetch('/Lesson/Create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(viewModel)
            });
            
            const result = await response.json();
            
            if (result.success) {
                // Show success message
                const successDiv = document.createElement('div');
                successDiv.className = 'success-message';
                successDiv.innerHTML = `
                    <i class="bi bi-check-circle-fill"></i>
                    <span>${result.message}</span>
                `;
                document.querySelector('.lesson-creator-container').insertBefore(
                    successDiv, 
                    document.querySelector('.lesson-header')
                );
                
                // Redirect after 1 second
                setTimeout(() => {
                    window.location.href = '/Lesson/Details/' + result.lessonId;
                }, 1000);
            } else {
                alert('Lỗi: ' + result.message);
                saveButton.disabled = false;
                saveButton.innerHTML = originalText;
            }
        }
        catch (error) {
            console.error('Error saving lesson:', error);
            alert('Đã xảy ra lỗi: ' + error.message);
            
            const saveButton = document.getElementById('save-lesson-button');
            saveButton.disabled = false;
            saveButton.innerHTML = '<i class="bi bi-save"></i> Lưu Bài học';
        }
    }
})();