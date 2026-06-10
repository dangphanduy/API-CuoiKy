// Payment handling JavaScript
class PaymentHandler {
    constructor() {
        this.initializeEventListeners();
    }

    initializeEventListeners() {
        // Xử lý nút thanh toán MoMo
        $(document).on('click', '.btn-momo-payment', (e) => {
            e.preventDefault();
            this.processMoMoPayment();
        });

        // Kiểm tra quyền truy cập khóa học
        $(document).on('click', '.check-course-access', (e) => {
            e.preventDefault();
            const courseId = $(e.target).data('course-id');
            this.checkCourseAccess(courseId);
        });
    }

    async processMoMoPayment() {
        try {
            // Hiển thị loading
            this.showLoading('Đang tạo thanh toán...');

            const response = await fetch('/Payment/CreateMoMoPayment', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                }
            });

            const result = await response.json();

            if (!result || !result.payUrl) {
                this.showError(result.message || 'Không tạo được liên kết thanh toán MoMo');
                return;
            }

            //  ĐÚNG CHUẨN MOMO: chỉ redirect sang payUrl
            window.location.href = result.payUrl;
        } catch (error) {
            console.error('Payment error:', error);
            this.showError('Có lỗi xảy ra khi xử lý thanh toán');
        } finally {
            this.hideLoading();
        }
    }

    async checkCourseAccess(courseId) {
        try {
            const response = await fetch(`/Payment/CheckCourseAccess?courseId=${courseId}`);
            const result = await response.json();

            if (result.hasAccess) {
                this.showSuccess('Bạn đã có quyền truy cập khóa học này');
                // Có thể redirect đến trang khóa học
                setTimeout(() => {
                    window.location.href = `/Course/Detail/${courseId}`;
                }, 1500);
            } else {
                this.showInfo('Bạn chưa mua khóa học này');
            }
        } catch (error) {
            console.error('Access check error:', error);
            this.showError('Có lỗi xảy ra khi kiểm tra quyền truy cập');
        }
    }

    showQRCode(qrCodeUrl) {
        const modal = `
            <div class="modal fade" id="qrCodeModal" tabindex="-1">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Quét mã QR để thanh toán</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body text-center">
                            <img src="${qrCodeUrl}" alt="QR Code" class="img-fluid mb-3" style="max-width: 300px;">
                            <p>Sử dụng ứng dụng MoMo để quét mã QR và thanh toán</p>
                            <div class="alert alert-info">
                                <i class="fas fa-info-circle"></i>
                                Sau khi thanh toán thành công, trang sẽ tự động cập nhật
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;

        $('body').append(modal);
        $('#qrCodeModal').modal('show');

        // Xóa modal khi đóng
        $('#qrCodeModal').on('hidden.bs.modal', function () {
            $(this).remove();
        });

        // Kiểm tra trạng thái thanh toán định kỳ
        this.startPaymentStatusCheck();
    }

    startPaymentStatusCheck() {
        // Kiểm tra trạng thái thanh toán mỗi 3 giây
        const checkInterval = setInterval(async () => {
            try {
                // Có thể thêm API endpoint để check payment status
                // Tạm thời dừng sau 2 phút
                setTimeout(() => {
                    clearInterval(checkInterval);
                }, 120000);
            } catch (error) {
                console.error('Payment status check error:', error);
            }
        }, 3000);
    }

    showLoading(message = 'Đang xử lý...') {
        const loadingHtml = `
            <div id="payment-loading" class="position-fixed top-0 start-0 w-100 h-100 d-flex align-items-center justify-content-center" 
                 style="background: rgba(0,0,0,0.5); z-index: 9999;">
                <div class="bg-white p-4 rounded text-center">
                    <div class="spinner-border text-primary mb-3" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                    <div>${message}</div>
                </div>
            </div>
        `;
        $('body').append(loadingHtml);
    }

    hideLoading() {
        $('#payment-loading').remove();
    }

    showSuccess(message) {
        this.showToast(message, 'success');
    }

    showError(message) {
        this.showToast(message, 'error');
    }

    showInfo(message) {
        this.showToast(message, 'info');
    }

    showToast(message, type = 'info') {
        const bgClass = {
            'success': 'bg-success',
            'error': 'bg-danger',
            'info': 'bg-info'
        }[type] || 'bg-info';

        const toastHtml = `
            <div class="toast align-items-center text-white ${bgClass} border-0" role="alert" style="position: fixed; top: 20px; right: 20px; z-index: 9999;">
                <div class="d-flex">
                    <div class="toast-body">
                        ${message}
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>
            </div>
        `;

        $('body').append(toastHtml);
        const toast = new bootstrap.Toast($('.toast').last()[0]);
        toast.show();

        // Tự động xóa toast sau khi ẩn
        $('.toast').last().on('hidden.bs.toast', function () {
            $(this).remove();
        });
    }
}

// Khởi tạo PaymentHandler khi document ready
$(document).ready(() => {
    new PaymentHandler();
});