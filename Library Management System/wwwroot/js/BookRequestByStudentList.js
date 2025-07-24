$(document).ready(function () {
    $("#bookRequestByStudentList").DataTable({
        "responsive": true,
        "ajax": {
            "url": "/StudentBookRequest/GetAllMyBookRequest",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", 'visible': false },
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
                    return `<button class="btn btn-danger btn-sm" onclick="DeleteBookRequest('${row.id}','${row.bookTitle}')"><i class="bi bi-trash"></i></button>`;
                },
                "orderable": false
            }
        ]
    })
})


function DeleteBookRequest(id, bookTitle) {
    Swal.fire({
        title: 'Are you sure?',
        text: `You are about to delete the book request for "${bookTitle}". This action cannot be undone.`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/StudentBookRequest/DeleteBookRequest",
                data: { id: id },
                success: function (response) {
                    if (response.success) {
                        Swal.fire('Deleted!', response.message, 'success');
                        $("#bookRequestByStudentList").DataTable().ajax.reload();
                    } else {
                        Swal.fire('Error!', response.message, 'error');
                    }
                },
                error: function () {
                    Swal.fire('Error!', 'An error occurred while deleting the request.', 'error');
                }
            });
        }
    });
}