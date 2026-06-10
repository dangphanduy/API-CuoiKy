// Account Settings JavaScript

$(document).ready(function() {
    console.log('Account settings JS loaded');

    // Email Edit
    $('#editEmailBtn').on('click', function() {
        console.log('Edit email button clicked');
        $('#emailEditForm').slideDown(300);
        $(this).hide();
    });

    $('#cancelEmailBtn').on('click', function() {
        $('#emailEditForm').slideUp(300);
        $('#editEmailBtn').show();
        $('#newEmailInput').val('');
    });

    $('#saveEmailBtn').on('click', function() {
        const newEmail = $('#newEmailInput').val().trim();
        
        if (!newEmail) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Vui lòng nhập email mới'
            });
            return;
        }

        // Email validation
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(newEmail)) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Email không đúng định dạng'
            });
            return;
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
            url: '/Account/UpdateEmail',
            type: 'POST',
            data: {
                newEmail: newEmail,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function(response) {
                console.log('Response:', response);
                // Check for both 'success' (lowercase) and 'SUCCESS' (uppercase)
                if (response.status === 'success' || response.status === 'SUCCESS') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thành công',
                        text: response.message || 'Cập nhật email thành công',
                        confirmButtonText: 'OK'
                    }).then(() => {
                        // Update email in dropdown immediately
                        if (response.email) {
                            updateEmailInUI(response.email);
                        }
                        
                        // Reload page after clicking OK
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Lỗi',
                        text: response.message || 'Không thể cập nhật email'
                    });
                }
            },
            error: function(xhr, status, error) {
                console.error('Error:', error);
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: 'Đã xảy ra lỗi. Vui lòng thử lại sau.'
                });
            }
        });
    });

    // Password Edit
    $('#editPasswordBtn').on('click', function() {
        console.log('Edit password button clicked');
        $('#passwordEditForm').slideDown(300);
        $(this).hide();
    });

    $('#cancelPasswordBtn').on('click', function() {
        $('#passwordEditForm').slideUp(300);
        $('#editPasswordBtn').show();
        $('#currentPasswordInput, #newPasswordInput, #confirmPasswordInput').val('');
    });

    $('#savePasswordBtn').on('click', function() {
        const currentPassword = $('#currentPasswordInput').val().trim();
        const newPassword = $('#newPasswordInput').val().trim();
        const confirmPassword = $('#confirmPasswordInput').val().trim();

        if (!currentPassword || !newPassword || !confirmPassword) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Vui lòng điền đầy đủ thông tin'
            });
            return;
        }

        if (newPassword !== confirmPassword) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Mật khẩu xác nhận không khớp'
            });
            return;
        }

        // Password strength validation
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        if (!passwordRegex.test(newPassword)) {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi',
                text: 'Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt'
            });
            return;
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
            url: '/Account/UpdatePassword',
            type: 'POST',
            data: {
                currentPassword: currentPassword,
                newPassword: newPassword,
                confirmPassword: confirmPassword,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
            },
            success: function(response) {
                console.log('Response:', response);
                // Check for both 'success' (lowercase) and 'SUCCESS' (uppercase)
                if (response.status === 'success' || response.status === 'SUCCESS') {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thành công',
                        text: response.message || 'Cập nhật mật khẩu thành công',
                        confirmButtonText: 'OK'
                    }).then(() => {
                        // Reload page after clicking OK
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Lỗi',
                        text: response.message || 'Không thể cập nhật mật khẩu'
                    });
                }
            },
            error: function(xhr, status, error) {
                console.error('Error:', error);
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: 'Đã xảy ra lỗi. Vui lòng thử lại sau.'
                });
            }
        });
    });

    // Multi-Factor Authentication Button
    $('.btn-security').on('click', function() {
        Swal.fire({
            icon: 'info',
            title: 'Tính năng đang phát triển',
            text: 'Tính năng xác thực đa yếu tố sẽ sớm được ra mắt!'
        });
    });

    // Debug: Check if buttons exist
    console.log('Email button exists:', $('#editEmailBtn').length);
    console.log('Password button exists:', $('#editPasswordBtn').length);
});

// Function to update email in UI (dropdown)
function updateEmailInUI(email) {
    // Update email in dropdown
    $('.user-email').text(email);
    
    console.log('Email updated in UI:', email);
}
