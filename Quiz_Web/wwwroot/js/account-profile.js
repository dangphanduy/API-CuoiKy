// Account Profile JavaScript

$(document).ready(function() {
    $('#saveProfileBtn').click(function() {
        const fullName = $('#fullNameInput').val().trim();
        const phone = $('#phoneInput').val().trim();

        if (!fullName) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Vui lòng nhập họ và tên'
            });
            return;
        }

        // Phone validation (optional field)
        if (phone) {
            const phoneRegex = /^[0-9]{10}$/;
            if (!phoneRegex.test(phone)) {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: 'Số điện thoại không đúng định dạng (10 chữ số)'
                });
                return;
            }
        }

        // Show loading
        Swal.fire({
            title: 'Đang xử lý...',
            allowOutsideClick: false,
            didOpen: () => {
                Swal.showLoading();
            }
        });

        $.ajax({
            url: '/Account/UpdateProfile',
            type: 'POST',
            data: {
                fullName: fullName,
                phone: phone,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function(response) {
                // Check for both 'success' (lowercase) and 'SUCCESS' (uppercase)
                if (response.status === 'success' || response.status === 'SUCCESS') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thành công',
                        text: 'Cập nhật hồ sơ thành công',
                        confirmButtonText: 'OK'
                    }).then(() => {
                        // Update user name in dropdown and header
                        if (response.fullName) {
                            updateUserNameInUI(response.fullName);
                        }

                        
                        // Reload page to ensure all data is fresh
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Lỗi',
                        text: response.message
                    });
                }
            },
            error: function() {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: 'Đã xảy ra lỗi. Vui lòng thử lại sau.'
                });
            }
        });
    });
});

// Function to update user name in UI (dropdown and avatar)
function updateUserNameInUI(fullName) {
    // Update user name in dropdown
    $('.user-name').text(fullName);
    
    // Update initials in avatar
    const initials = fullName.split(' ')
        .map(word => word.charAt(0))
        .slice(0, 2)
        .join('')
        .toUpperCase();
    
    $('.user-avatar').text(initials);
    $('.user-avatar-large').text(initials);
}
