// Update account of the student
$('#btnUpdateAccount').on('click', function () {
    if (!validateForm()) {
        return validateForm();
    }
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

function validateForm() {
    const isPasswordValid = validatePassword();
    const isPhoneNumberValid = validatePhoneNumber();
    return isPasswordValid && isPhoneNumberValid;
}