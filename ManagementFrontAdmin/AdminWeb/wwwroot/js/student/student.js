function loadStudentInfo(studentId) {
    console.log("Selected studentId:", studentId);
    if (!studentId) {
        // Xóa thông tin nếu không có ID được chọn
        document.querySelector('#student-info').innerHTML = `<p>Chưa có thông tin học viên được chọn.</p>`;
        return;
    }
    const baseUrl = window.location.origin; // Lấy gốc URL, ví dụ: https://localhost:8080
    const url = `${baseUrl}/student/getStudentById/${studentId}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Không tìm thấy học viên này');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            const birthdateFormat = formatDateVN(data.dateOfBirth);
            const genderText = data.gender === 0 ? 'Nam' : data.gender === 1 ? 'Nữ' : 'Không xác định';

            document.querySelector('#student-info').innerHTML = `
                            <div class="d-flex">
                                <p class="text-start me-1">Mã học viên: </p>
                                <p class="text-dark">${data.id}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Họ và tên: </p>
                                <p class="text-dark">${data.fullName}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Ngày sinh: </p>
                                <p class="text-dark text-bold">${birthdateFormat}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Giới tính: </p>
                                <p class="text-dark text-bold">${genderText}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Địa chỉ: </p>
                                <p class="text-dark text-bold">${data.address}</p>
                            </div>
                        `;
        })
        .catch(error => {
            console.error('Error:', error);
            document.querySelector('#student-info').innerHTML = `<p class="text-danger">Không tìm thấy thông tin học viên.</p>`;
        });
}

function formatDateVN(dateString) {
    if (!dateString) return 'Chưa cập nhật';

    const date = new Date(dateString);
    if (isNaN(date)) return 'Không hợp lệ';

    return new Intl.DateTimeFormat('vi-VN', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    }).format(date);
}


// Update account of the student
$('#btnUpdateAccount').on('click', function () {

    const formData = $('#edit-user-form').serialize();

    $.ajax({
        url: '/User/Edit',
        type: 'POST',
        data: formData,
        success: function (res) {
            if (res.success) {
                Snackbar.show({
                    text: res.message,
                    backgroundColor: '#00ab55'
                });
            } else {
                Snackbar.show({
                    text: res.message,
                    backgroundColor: '#e7515a'
                });
            }
        },
        error: function () {
            Snackbar.show({
                text: 'Có lỗi xảy ra',
                backgroundColor: '#e7515a'
            });
        }
    });
});

$(document).ready(function () {

    // Press "Thêm mới tài khoản"
    $('#btnShowCreateAccount').on('click', function () {
        $('#no-account-section').hide();
        $('#create-account-section').fadeIn(200);
    });

    // Press "Hủy"
    $('#btnCancelCreateAccount').on('click', function () {
        $('#create-account-section').hide();
        $('#no-account-section').fadeIn(200);
    });

});

// Validate account for the text-danger
$('#btnCreateAccount').on('click', function () {

    let hasError = false;

    $('span.text-danger').each(function () {
        if ($(this).text().trim() !== '') {
            console.log('[' + $(this).text() + ']');
            hasError = true;
            return false; // break loop
        }
    });

    if (hasError) {
        Snackbar.show({
            text: 'Vui lòng kiểm tra lại dữ liệu',
            backgroundColor: '#e7515a'
        });
        return;
    }

    createAccountAjax();
});

// Add new account for a student
function createAccountAjax() {

    const formData = $('#create-user-form').serialize();

    $.ajax({
        url: '/User/Create',
        type: 'POST',
        data: formData,
        success: function (res) {
            if (res.success) {
                Snackbar.show({
                    text: res.message,
                    backgroundColor: '#00ab55'
                });
                updateStudentAjax(res.userId);
            } else {
                Snackbar.show({
                    text: res.message,
                    backgroundColor: '#e7515a'
                });
            }
        },
        error: function () {
            Snackbar.show({
                text: 'Có lỗi xảy ra',
                backgroundColor: '#e7515a'
            });
        }
    });
};

function updateStudentAjax(userId) {
    $.ajax({
        url: '/Student/UpdateAccountId',
        type: 'POST',
        data: {
            studentId: $('#StudentId').val(), // hidden input
            userId: userId
        },
        success: function (res) {
            if (!res.success) {
                Snackbar.show({
                    text: res.message,
                    backgroundColor: '#e7515a'
                });
                return;
            }

            Snackbar.show({
                text: res.message,
                backgroundColor: '#1abc9c'
            });

            setTimeout(() => {
                window.location.reload();
            }, 1000);
        }
    });
}