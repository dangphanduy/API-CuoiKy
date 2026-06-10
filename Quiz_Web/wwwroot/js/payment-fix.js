// Script để cập nhật pending purchases thành completed
function updatePendingPurchases() {
    fetch('/Payment/UpdatePendingPurchases', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            alert(`Thành công: ${data.message}`);
            // Reload trang để cập nhật UI
            window.location.reload();
        } else {
            alert(`Lỗi: ${data.message}`);
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

// Thêm button vào trang purchase history nếu có pending purchases
document.addEventListener('DOMContentLoaded', function() {
    // Kiểm tra xem có pending purchases không
    const pendingElements = document.querySelectorAll('.purchase-status[data-status="pending"]');
    
    if (pendingElements.length > 0) {
        // Tạo button để fix pending purchases
        const fixButton = document.createElement('button');
        fixButton.className = 'btn btn-warning mb-3';
        fixButton.innerHTML = '<i class="fas fa-sync"></i> Cập nhật giao dịch pending';
        fixButton.onclick = updatePendingPurchases;
        
        // Thêm button vào đầu trang
        const container = document.querySelector('.container') || document.body;
        container.insertBefore(fixButton, container.firstChild);
    }
});