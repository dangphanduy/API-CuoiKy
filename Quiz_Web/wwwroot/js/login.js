function token() {
    return $('input[name="__RequestVerificationToken"]').val();
}

function loginAccount(userInput) {
    userInput.__RequestVerificationToken = token();
    $.ajax({
        type: "POST",
        url: "/Account/LoginToSystem",
        data: userInput,
        dataType: 'json',

        success: function (res) {

            if (res.status === 'success') {
                Swal.fire({
                    icon: 'success',
                    title: 'Đăng nhập thành công! Vui lòng chờ giây lát',
                    showConfirmButton: false,
                    timer: 1500
                }).then(() => {
                    location.href = res.redirectUrl || '/';
                });

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi đăng nhập',
                    text: res.message
                });
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi hệ thống',
                text: 'Vui lòng thử lại sau'
            });
        }
    });
}

function registerAccount(userInput) {
    userInput.__RequestVerificationToken = token();
    $.ajax({
        type: "POST",
        url: "Account/RegisterToSystem",
        data: userInput,
        dataType: 'json',

        success: function (res) {
            if (res.status === 'success') {
                Swal.fire({
                    icon: 'success',
                    title: 'Đăng ký thành công',
                    text: res.messagge
                }).then(() => location.href = '/Login');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi đăng ký',
                    text: res.message
                });
            }
        },

        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi hệ thống',
                text: 'Vui lòng thử lại sau'
            });
        }
    });
}

function forgotPassword(userInput) {
    userInput.__RequestVerificationToken = token();
    $.ajax({
        type: "POST",
        url: "/Account/ForgotPasswordSubmit",
        data: userInput,
        dataType: 'json',

        success: function (res) {
            if (res.status === "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Thành công',
                    text: res.message
                }).then(() => location.href = '/Login');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: res.message
                });
            }
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi hệ thống',
                text: 'Vui lòng thử lại sau'
            });
        }
    });
}


function resetPassword(userInput) {
    userInput.__RequestVerificationToken = token();
    $.ajax({
        type: "POST",
        url: "/Account/ResetPasswordSubmit",
        data: userInput,
        dataType: 'json',

        success: function (res) {
            if (res.status === "success") {
                Swal.fire({
                    icon: 'success',
                    title: 'Thành công',
                    text: res.message
                }).then(() => location.href = '/login');
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Lỗi',
                    text: res.message
                });
            }
        },

        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Lỗi hệ thống',
                text: 'Vui lòng thử lại sau'
            });

        }
    }
    );
}

document.addEventListener('DOMContentLoaded', function () {
    const params = new URLSearchParams(window.location.search);
    const returnUrl = params.get('ReturnUrl') || params.get('returnUrl');

    //login form
    $(document).off('submit', '#login_form');
    $(document).on('submit', '#login_form', function (e) {
        e.preventDefault();
        loginAccount({
            username: $('#usernameInput').val(),
            password: $('#passwordInput').val(),
            returnUrl: returnUrl
        });
    })
    //register form
    $(document).off('submit', '#register_form');
    $(document).on('submit', '#register_form', function (e) {
        e.preventDefault();
        registerAccount({
            fullname: $('#fullname').val(),
            email: $('#email').val(),
            username: $('#username').val(),
            password: $('#password').val(),
            confirmPassword: $('#confirmPassword').val()
        });
    });

    //forgot password form
    $(document).off('submit', '#forgot_password_form');
    $(document).on('submit', '#forgot_password_form', function (e) {
        e.preventDefault();
        forgotPassword({
            email: $('#emailInput').val()
        });
    });

    //reset password form
    $(document).off('submit', '#reset_password_form');
    $(document).on('submit', '#reset_password_form', function (e) {
        e.preventDefault();
        resetPassword({
            token: $('#tokenInput').val(),
            password: $('#passwordInput').val(),
            confirmPassword: $('#confirmPasswordInput').val()
        });
    });
});