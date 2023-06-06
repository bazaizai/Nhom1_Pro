const notificationsAlert = document.querySelector(".notificationsAlert"),
    buttons = document.querySelectorAll(".buttons .btn1");

const toast1Details = {
    timer: 5000,
    success: {
        icon: 'fa-circle-check',
        text: 'Thông báo: Thanh toán thành công bạn chắc chắn chứ Thông báo: Thanh toán thành công bạn chắc chắn chứ',
    },
    error: {
        icon: 'fa-circle-xmark',
        text: 'Error: This is an error toast1.',
    },
    warning: {
        icon: 'fa-triangle-exclamation',
        text: 'Warning: This is a warning toast1.',
    },
    info: {
        icon: 'fa-circle-info',
        text: 'Info: This is an information toast1.',
    },
    1: {
        icon: 'fa-triangle-exclamation',
        text: 'Thông báo: Sản phẩm đã tồn tại!',
    },
    2: {
        icon: 'fa-circle-check',
        text: 'Thông báo: Cập nhật thành công!',
    }
}

const removetoast1 = (toast1) => {
    toast1.classList.add("hide");
    if (toast1.timeoutId) clearTimeout(toast1.timeoutId); // Clearing the timeout for the toast1
    setTimeout(() => toast1.remove(), 500); // Removing the toast1 after 500ms
}

const createtoast1 = (id) => {
    // Getting the icon and text for the toast1 based on the id passed
    const { icon, text } = toast1Details[id];
    const toast1 = document.createElement("li"); // Creating a new 'li' element for the toast1
    toast1.className = `toast1 ${id}`; // Setting the classes for the toast1
    // Setting the inner HTML for the toast1
    toast1.innerHTML = `<div class="column">
                         <i class="fa-solid ${icon}"></i>
                         <span style="font-family: 'Poppins', sans-serif;" >${text}</span>
                      </div>
                      <i class="fa-solid fa-xmark" onclick="removetoast1(this.parentElement)"></i>`;
    notificationsAlert.appendChild(toast1); // Append the toast1 to the notification ul
    // Setting a timeout to remove the toast1 after the specified duration
    toast1.timeoutId = setTimeout(() => removetoast1(toast1), toast1Details.timer);
}
