// Update account of the teacher
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

// Add new account for a teacher
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
                updateTeacherAjax(res.userId);
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

function updateTeacherAjax(userId) {
    $.ajax({
        url: '/Teacher/UpdateAccountId',
        type: 'POST',
        data: {
            teacherId: $('#TeacherId').val(), // hidden input
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


// Ajax delete teacher
//$(document).ready(function () {
//    let teacherIdToDelete;

//    $('.delete').on('click', function (e) {
//        e.preventDefault();
//        teacherIdToDelete = $(this).data('teacher-id');
//        $('#deleteModal').modal('show');
//    });


//    $('#confirmDelete').on('click', function () {
//        $.ajax({
//            url: `/Teacher/Delete`,
//            type: 'POST',
//            data: {
//                id: teacherIdToDelete
//            },
//            success: function (response) {
//                Snackbar.show({
//                    text: 'Xóa giáo viên thành công',
//                    textColor: '#ddf5f0',
//                    backgroundColor: '#00ab55',
//                    actionText: 'Bỏ qua',
//                    actionTextColor: '#3b3f5c'
//                });

//                setTimeout(function () {
//                    location.reload();
//                }, 1000);
//            },
//            error: function (xhr, status, error) {
//                Snackbar.show({
//                    text: 'Xóa giáo viên thất bại',
//                    textColor: '#fbeced',
//                    backgroundColor: '#e7515a',
//                    actionText: 'Bỏ qua',
//                    actionTextColor: '#3b3f5c'
//                });
//            }
//        });

//        $('#deleteModal').modal('hide');
//    });
//});


// Display the comment for each student asignment
$(document).ready(function () {

    var table = $('#teacher-table').DataTable();

    $('#teacher-table tbody').on('click', '.view-comment', function () {

        var tr = $(this).closest('tr');
        var row = table.row(tr);
        var comment = $(this).data('comment');

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
            return;
        }

        table.rows().every(function () {
            if (this.child.isShown()) {
                this.child.hide();
                $(this.node()).removeClass('shown');
            }
        });

        row.child(`
                <div class="p-3 bg-light border rounded">
                    <strong>Nhận xét:</strong>
                    <p class="mb-0 mt-2" style="word-break: break-word; white-space: normal;">
                        ${comment}
                    </p>
                </div>
            `).show();

        tr.addClass('shown');
    });

});
