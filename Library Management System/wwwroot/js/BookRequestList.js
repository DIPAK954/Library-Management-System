$(document).ready(function () {
    $("#bookRequestList").DataTable({
        "responsive": true,
        "ajax": {
            "url": "/AdminBookRequest/GetAllBookRequest",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", 'visible': false }, 
            { "data": "studentName" }, 
            { "data": "bookTitle" }, 
            { "data": "isbn" },
            {
                "data": "requestDate",
                render: function (data) {
                    const date = new Date(data);
                    const day = ("0" + date.getDate()).slice(-2);
                    const month = ("0" + (date.getMonth() + 1)).slice(-2);
                    const year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            },
            {
                "data": "status",
                render: function (data) {
                    let statusText, badgeClass;

                    if (data === 0) { // Pending
                        statusText = 'Pending';
                        badgeClass = 'text-bg-warning';
                    } else if (data === 1) { // Approved
                        statusText = 'Approved';
                        badgeClass = 'text-bg-success';
                    } else if (data === 2) { // Rejected
                        statusText = 'Rejected';
                        badgeClass = 'text-bg-danger';
                    } else if (data === 3) { // Cancelled
                        statusText = 'Cancelled';
                        badgeClass = 'text-bg-secondary';
                    } else {
                        // Ignore other statuses
                        return '';
                    }
                    return `<span class="badge ${badgeClass}">${statusText}</span>`
                }
            },
            {
                "data": "actions",
                render: function (data, type, row) {
                    return `<button class="btn btn-warning btn-sm" onclick="BookRequestStatus('${row.id}', '${row.studentName}', '${row.bookTitle}', ${row.status})"><i class="bi bi-pencil-square"></i></button>`;
                },
                "orderable": false
            }
        ]
    })
})


function BookRequestStatus(id, studentName, bookTitle, currentStatus) {
    Swal.fire({
        title: "Change Request Status",
        html: `
            <p><strong>Student:</strong> ${studentName}</p>
            <p><strong>Book:</strong> ${bookTitle}</p>
            <p><strong>Current Status:</strong> ${getStatusText(currentStatus)}</p>
            <p>Do you want to <strong>Approve</strong> or <strong>Reject</strong> this request?</p>
        `,
        icon: "question",
        showCancelButton: true,
        showDenyButton: true,
        confirmButtonText: "Approve",
        denyButtonText: "Reject",
        cancelButtonText: "Cancel",
        confirmButtonColor: "#28a745",  // green
        denyButtonColor: "#dc3545",     // red
    }).then((result) => {
        if (result.isConfirmed) {
            // Call approve endpoint
            updateBookRequestStatus(id, 1); // 1 = Approved
        } else if (result.isDenied) {
            // Call reject endpoint
            updateBookRequestStatus(id, 2); // 2 = Rejected
        }
    });
}

// Helper to convert status number to readable text
function getStatusText(status) {
    switch (status) {
        case 0: return 'Pending';
        case 1: return 'Approved';
        case 2: return 'Rejected';
        case 3: return 'Cancelled';
        default: return 'Unknown';
    }
}

function updateBookRequestStatus(id, newStatus) {
    $.ajax({
        url: `/AdminBookRequest/UpdateStatus`, // Your controller endpoint
        type: 'POST',
        data: { id: id, status: newStatus },
        success: function (response) {
            if (response.success) {
                Swal.fire("Success", response.message, "success");
                $('#bookRequestList').DataTable().ajax.reload();
            } else {
                Swal.fire("Error", response.message, "error");
            }
        },
        error: function () {
            Swal.fire("Error", "Failed to update status. Try again later.", "error");
        }
    });
}
