$(document).ready(function () {
    // Initialize DataTable
    $('#fineListTable').DataTable({
        "ajax": {
            "url": "/AdminFine/AllStudentFine",
            "type": "GET",
        },
        "columns": [
            { "data": "id", "visible": false },
            { "data": "studentName" },
            { "data": "phoneNumber" },
            { "data": "bookTitle" },
            {
                "data": "fineDate",
                render: function (data) {
                    const date = new Date(data);
                    const day = ("0" + date.getDate()).slice(-2);
                    const month = ("0" + (date.getMonth() + 1)).slice(-2);
                    const year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            },
            {
                "data": "fineAmount",
                "render": function(data) {
                    var Color = data == 1 ? 'text-success' : 'text-danger';
                    return `<span class="${Color}">₹${data}</span>`
                }
            },
            {
                "data": "fineType",
                "render": function (data) {
                    let badgeClass = "";
                    let label = "";

                    if (data == 1) {
                        badgeClass = "bg-warning";
                        label = "Late Return";
                    } else if (data == 2) {
                        badgeClass = "bg-danger";
                        label = "Lost Book";
                    } else {
                        badgeClass = "bg-secondary";
                        label = "Unknown";
                    }

                    return `<span class="badge ${badgeClass}">${label}</span>`;
                }
            },
            {
                "data": "fineStatus",
                "render": function (data, type, row) {
                    var fineColor = data == 1 ? 'bg-success' : 'bg-danger';
                    var fineStatus = data == 1 ? 'Paid' : 'Unpaid';
                    return `<span class="badge ${fineColor}">${fineStatus}</span>`
                }
            },
            {
                "data": null,
                "render": function (row) {
                    return `<button onclick="markFinePaid('${row.id}')" class="btn btn-sm btn-success">Mark Paid</button>`;
                },
                "orderable": false
            }
        ]
    });
})

function markFinePaid(id) {
    Swal.fire({
        title: "Select Fine Reason",
        text: "Choose the reason for marking the fine as paid:",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Late Return",
        cancelButtonText: "Cancel",
        showDenyButton: true,
        denyButtonText: "Lost Book"
    }).then((result) => {
        if (result.isConfirmed) {
            // Late Return selected
            submitFineStatus(id, "LateReturn");
        } else if (result.isDenied) {
            // Lost Book selected
            submitFineStatus(id, "LostBook");
        } else {
            // Cancelled
            Swal.fire("Cancelled", "No action was taken", "info");
        }
    });
}

function submitFineStatus(id, reason) {
    $.ajax({
        url: "/AdminFine/MarkFinePaid", // your controller route
        type: "POST",
        data: { id: id, reason: reason },
        success: function (response) {
            if (response.success) {
                toastr.success(response.message);
                $('#fineListTable').DataTable().ajax.reload();
            } else {
                toastr.error(response.message);
                if (response.errors) {
                    response.errors.forEach(err => toastr.warning(err));
                }
            }
        },
        error: function () {
            toastr.error("Something went wrong. Try again later.");
        }
    });
}



