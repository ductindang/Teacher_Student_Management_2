function loadCourseInfo(courseId) {
    if (!courseId) {
        // Clear data if it do not have any Id
        document.querySelector('#course-info').innerHTML = `<p>Chưa có thông tin khóa học được chọn<p>`;
        return;
    }

    const baseUrl = window.location.origin;// Take the root path
    const url = `${baseUrl}/course/getCourseById/${courseId}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Không tìm thấy khóa học này');
            }
            return response.json();
        })
        .then(data => {
            const formattedPrice = data.price
                ? new Intl.NumberFormat('vi-VN', {
                    style: 'currency',
                    currency: 'VND'
                }).format(data.price) : 'Chưa cập nhật';
            const statusText = mapCourseStatus(data.status);
            document.querySelector('#course-info').innerHTML = `
                            <div class="d-flex">
                                <p class="text-start me-1">Mã khóa học: </p>
                                <p class="text-dark">${data.id}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Tên: </p>
                                <p class="text-dark">${data.name}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Học phí: </p>
                                <p class="text-dark text-bold">${formattedPrice}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Trạng thái: </p>
                                <p class="text-dark text-bold">${statusText}</p>
                            </div>
                        `;
        })
        .catch(error => {
            console.error('Error:', error);
            document.querySelector('#course-info').innerHTML = `<p class="text-danger">Không tìm thấy thông tin khóa học.</p>`;
        });
}

function mapCourseStatus(status) {
    switch (status) {
        case 0:
        case "Inactive":
            return "Ngừng hoạt động";
        case 1:
        case "Active":
            return "Đang mở";
        case 2:
        case "Completed":
            return "Đã kết thúc";
        default:
            return "Không xác định";
    }
}
