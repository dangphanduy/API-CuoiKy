// Cart Dropdown JavaScript
(function() {
    'use strict';

    // Load cart items when dropdown is opened
    document.addEventListener('DOMContentLoaded', function() {
        const cartDropdown = document.getElementById('cartDropdownToggle');
        const cartMenu = document.getElementById('cartDropdownMenu');

        if (cartDropdown) {
            cartDropdown.addEventListener('click', function(e) {
                // Check if user is authenticated
                const isAuthenticated = document.body.dataset.userAuthenticated === 'true';
                if (!isAuthenticated) {
                    e.preventDefault();
                    window.location.href = '/login?returnUrl=' + encodeURIComponent(window.location.pathname);
                    return;
                }

                // Load cart items
                loadCartItems();
            });
        }

        // Listen for custom cart update events
        document.addEventListener('cartUpdated', function() {
            loadCartItems();
            updateCartBadge();
        });
    });

    function loadCartItems() {
        const cartItemsList = document.getElementById('cartItemsList');
        if (!cartItemsList) return;

        // Show loading
        cartItemsList.innerHTML = `
            <div class="text-center py-3">
                <div class="spinner-border spinner-border-sm text-primary" role="status">
                    <span class="visually-hidden">Đang tải...</span>
                </div>
            </div>
        `;

        fetch('/api/cart/items')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    renderCartItems(data.items, data.total);
                } else {
                    showError('Không thể tải giỏ hàng');
                }
            })
            .catch(error => {
                console.error('Error loading cart:', error);
                showError('Đã xảy ra lỗi khi tải giỏ hàng');
            });
    }

    function renderCartItems(items, total) {
        const cartItemsList = document.getElementById('cartItemsList');
        const cartTotal = document.getElementById('cartTotal');

        if (!cartItemsList) return;

        if (items.length === 0) {
            // Show empty cart
            const cartContent = cartItemsList.closest('.cart-content');
            cartContent.innerHTML = `
                <div class="cart-empty text-center py-5 px-3">
                    <i class="bi bi-cart-x text-muted" style="font-size: 48px;"></i>
                    <p class="text-muted mt-3 mb-2">Giỏ hàng của bạn đang trống.</p>
                    <a href="/courses" class="text-decoration-none text-primary">
                        Tiếp tục mua sắm <i class="bi bi-arrow-right"></i>
                    </a>
                </div>
            `;
            return;
        }

        // Render cart items
        let html = '';
        items.forEach(item => {
            const imageUrl = item.coverUrl && item.coverUrl !== '/images/default-course.jpg' ? item.coverUrl : 'https://placehold.co/300x200/ffffff/000000?text=Course';
            const price = formatPrice(item.price);
            
            html += `
                <div class="cart-item" data-course-id="${item.courseId}">
                    <img src="${imageUrl}" alt="${escapeHtml(item.title)}" class="cart-item-image" 
                         onerror="this.src='https://placehold.co/300x200/ffffff/000000?text=Course'">

                    <div class="cart-item-details">
                        <div class="cart-item-title">${escapeHtml(item.title)}</div>
                        <div class="cart-item-instructor">
                            <i class="bi bi-person"></i> ${escapeHtml(item.instructor)}
                        </div>
                        <div class="cart-item-footer">
                            <span class="cart-item-price">${price}</span>
                            <button class="cart-item-remove" onclick="removeFromCart(${item.courseId})">
                                Xóa
                            </button>
                        </div>
                    </div>
                </div>
            `;
        });

        cartItemsList.innerHTML = html;

        // Show footer because items exist
        if (cartFooter) {
            cartFooter.style.display = "";
        }

        // Update total
        if (cartTotal) {
            cartTotal.textContent = formatPrice(total);
        }
    }

    function formatPrice(price) {
        return new Intl.NumberFormat('vi-VN').format(price) + ' đ';
    }

    function escapeHtml(text) {
        const map = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#039;'
        };
        return text.replace(/[&<>"']/g, m => map[m]);
    }

    function showError(message) {
        const cartItemsList = document.getElementById('cartItemsList');
        if (cartItemsList) {
            cartItemsList.innerHTML = `
                <div class="text-center py-4 px-3 text-danger">
                    <i class="bi bi-exclamation-circle" style="font-size: 32px;"></i>
                    <p class="mt-2 mb-0">${message}</p>
                </div>
            `;
        }
    }

    function updateCartBadge() {
        fetch('/api/cart/count')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    const badge = document.querySelector('.cart-badge');
                    const cartLink = document.getElementById('cartDropdownToggle');
                    
                    if (data.count > 0) {
                        if (badge) {
                            badge.textContent = data.count;
                        } else if (cartLink) {
                            const newBadge = document.createElement('span');
                            newBadge.className = 'cart-badge position-absolute top-0 start-100 translate-middle badge rounded-pill bg-purple';
                            newBadge.textContent = data.count;
                            cartLink.appendChild(newBadge);
                        }
                    } else {
                        if (badge) {
                            badge.remove();
                        }
                    }
                }
            })
            .catch(error => {
                console.error('Error updating cart badge:', error);
            });
    }

    // Global function to remove item from cart
    window.removeFromCart = function(courseId) {
        if (!confirm('Bạn có chắc muốn xóa khỏi giỏ hàng')) {
            return;
        }

        fetch(`/api/cart/remove/${courseId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Reload cart items
                loadCartItems();
                updateCartBadge();
                
                // Show success message
                showToast('success', data.message || 'Đã xóa khỏi giỏ hàng');
                
                // Dispatch custom event
                document.dispatchEvent(new CustomEvent('cartUpdated'));
            } else {
                showToast('error', data.message || 'Không thể xóa khỏi giỏ hàng');
            }
        })
        .catch(error => {
            console.error('Error removing from cart:', error);
            showToast('error', 'Đã xảy ra lỗi');
        });
    };

    // Global function to add item to cart
    window.addToCart = function(courseId) {
        fetch(`/api/cart/add/${courseId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Show success message
                showToast('success', data.message || 'Đã thêm vào giỏ hàng');
                
                // Reload page after short delay to show toast
                setTimeout(() => {
                    window.location.reload();
                }, 500);
            } else {
                showToast('error', data.message || 'Không thể thêm vào giỏ hàng');
            }
        })
        .catch(error => {
            console.error('Error adding to cart:', error);
            showToast('error', 'Đã xảy ra lỗi');
        });
    };

    // Toast notification helper
    function showToast(type, message) {
        // Check if toastr is available
        if (typeof toastr !== 'undefined') {
            toastr[type](message);
        } else {
            // Fallback to alert
            alert(message);
        }
    }

    // Initialize cart badge on page load
    if (document.body.dataset.userAuthenticated === 'true') {
        updateCartBadge();
    }
})();