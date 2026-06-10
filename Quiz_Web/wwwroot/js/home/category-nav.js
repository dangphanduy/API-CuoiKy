/**
 * Category Navigation Bar - Home Page
 * Handles scroll indicators and navigation interactions
 */

(function() {
    'use strict';
    
    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
    
    function init() {
        const wrapper = document.querySelector('.category-nav-wrapper');
        if (!wrapper) return;
        
        // Setup scroll indicators
        setupScrollIndicators(wrapper);
        
        // Setup active state management
        setupActiveState();
        
        // Setup smooth scroll on mobile
        setupSmoothScroll(wrapper);
    }
    
    /**
     * Setup scroll indicators to show when content is scrollable
     */
    function setupScrollIndicators(wrapper) {
        function updateIndicators() {
            const scrollLeft = wrapper.scrollLeft;
            const scrollWidth = wrapper.scrollWidth;
            const clientWidth = wrapper.clientWidth;
            
            // Add/remove left indicator
            if (scrollLeft > 10) {
                wrapper.classList.add('scroll-left');
            } else {
                wrapper.classList.remove('scroll-left');
            }
            
            // Add/remove right indicator
            if (scrollLeft + clientWidth < scrollWidth - 10) {
                wrapper.classList.add('scroll-right');
            } else {
                wrapper.classList.remove('scroll-right');
            }
        }
        
        // Update on scroll
        wrapper.addEventListener('scroll', updateIndicators);
        
        // Initial check
        updateIndicators();
        
        // Update on window resize
        window.addEventListener('resize', debounce(updateIndicators, 150));
    }
    
    /**
     * Setup active state for current category
     */
    function setupActiveState() {
        const currentPath = window.location.pathname;
        const categoryLinks = document.querySelectorAll('.category-nav-item');
        
        categoryLinks.forEach(link => {
            if (link.getAttribute('href') === currentPath) {
                link.classList.add('active');
            }
            
            // Add click handler for loading state
            link.addEventListener('click', function() {
                // Remove active from all
                categoryLinks.forEach(l => l.classList.remove('active'));
                // Add active to clicked
                this.classList.add('active', 'loading');
            });
        });
    }
    
    /**
     * Setup smooth scroll behavior for mobile touch
     */
    function setupSmoothScroll(wrapper) {
        let isDown = false;
        let startX;
        let scrollLeft;
        
        wrapper.addEventListener('mousedown', (e) => {
            isDown = true;
            wrapper.style.cursor = 'grabbing';
            startX = e.pageX - wrapper.offsetLeft;
            scrollLeft = wrapper.scrollLeft;
        });
        
        wrapper.addEventListener('mouseleave', () => {
            isDown = false;
            wrapper.style.cursor = 'default';
        });
        
        wrapper.addEventListener('mouseup', () => {
            isDown = false;
            wrapper.style.cursor = 'default';
        });
        
        wrapper.addEventListener('mousemove', (e) => {
            if (!isDown) return;
            e.preventDefault();
            const x = e.pageX - wrapper.offsetLeft;
            const walk = (x - startX) * 2;
            wrapper.scrollLeft = scrollLeft - walk;
        });
    }
    
    /**
     * Debounce utility function
     */
    function debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }
})();
