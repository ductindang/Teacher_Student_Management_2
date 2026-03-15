
function loadClassInfo(classId) {
    console.log("Selected classId:", classId);
    if (!classId) {
        // Xóa thông tin nếu không có ID được chọn
        document.querySelector('#class-info').innerHTML = `<p>Chưa có thông tin lớp học được chọn.</p>`;
        return;
    }
    const baseUrl = window.location.origin; // Lấy gốc URL, ví dụ: https://localhost:8080
    const url = `${baseUrl}/class/getClassById/${classId}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Không tìm thấy lơp học này');
            }
            return response.json();
        })
        .then(data => {
            const startDate = formatDateVN(data.startDate);
            const endDate = formatDateVN(data.endDate);

            document.querySelector('#class-info').innerHTML = `
                            <div class="d-flex">
                                <p class="text-start me-1">Mã lớp học: </p>
                                <p class="text-dark">${data.id}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Giáo viên: </p>
                                <p class="text-dark">${data.teacherName}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Tên lớp học: </p>
                                <p class="text-dark text-bold">${data.name}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Ngày bắt đầu: </p>
                                <p class="text-dark text-bold">${startDate}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Ngày kết thúc: </p>
                                <p class="text-dark text-bold">${endDate}</p>
                            </div>
                            <div class="d-flex">
                                <p class="text-start me-1">Số lượng sinh viên: </p>
                                <p class="text-dark text-bold">${data.maxStudents}</p>
                            </div>
                        `;
        })
        .catch(error => {
            console.error('Error:', error);
            document.querySelector('#class-info').innerHTML = `<p class="text-danger">Không tìm thấy thông tin lớp học.</p>`;
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
