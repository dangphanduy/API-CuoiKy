$(document).ready(function() {
    // Initialize Slick Carousel with centerMode
    $('.features-carousel').slick({
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        arrows: true,
        centerMode: true,
        centerPadding: '0px',
        focusOnSelect: true,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    centerMode: true
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    centerMode: true,
                    centerPadding: '0px'
                }
            }
        ]
    });

    // Prevent default link behavior during drag
    let isDragging = false;

    $('.features-carousel').on('mousedown touchstart', function() {
        isDragging = false;
    });

    $('.features-carousel').on('mousemove touchmove', function() {
        isDragging = true;
    });

    $('.features-carousel').on('click', '.feature-card', function(e) {
        if (isDragging) {
            e.preventDefault();
            isDragging = false;
            return false;
        }
    });

    $('.features-carousel').on('mouseup touchend', function() {
        setTimeout(function() {
            isDragging = false;
        }, 100);
    });
});