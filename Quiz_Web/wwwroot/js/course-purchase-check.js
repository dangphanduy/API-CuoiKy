// Course Purchase Check JavaScript
(function() {
    'use strict';

    document.addEventListener('DOMContentLoaded', function() {
        // Kiểm tra user đã đăng nhập
        const isAuthenticated = document.body.dataset.userAuthenticated === 'true';
        if (!isAuthenticated) return;

        // Tìm tất cả button "Thêm vào giỏ hàng"
        const addToCartButtons = document.querySelectorAll('[onclick*="addToCart"]');
        
        addToCartButtons.forEach(button => {
            const onclickAttr = button.getAttribute('onclick');
            const courseIdMatch = onclickAttr.match(/addToCart\((\d+)\)/);
            
            if (courseIdMatch) {
                const courseId = parseInt(courseIdMatch[1]);
                checkPurchaseStatus(courseId, button);
            }
        });
    });

    function checkPurchaseStatus(courseId, button) {
        fetch(`/api/cart/check-purchased/${courseId}`)
            .then(response => response.json())
            .then(data => {
                if (data.success && data.hasPurchased) {
                    // Thay đổi button thành "Đã mua"
                    button.innerHTML = '<i class="bi bi-check-circle"></i> Đã mua';
                    button.className = 'btn btn-success disabled';
                    button.removeAttribute('onclick');
                    button.disabled = true;
                }
            })
            .catch(error => {
                console.error('Error checking purchase status:', error);
            });
    }

    // Cập nhật sau khi thanh toán thành công
    document.addEventListener('purchaseCompleted', function() {
        location.reload();
    });
})();