$(document).ready(function () {
    $("#issueBookList").DataTable({
        "responsive": true, 
        "ajax": {
            "url": "/IssuedBookHistory/GetAllIssuedBookHistory",
            "type": "GET",
            "datatype": "json"
        }, 
        "columns": [
            { "data": "id", "name": "Id", "visible": false },
            { "data": "studentName", "name": "StudentName", "autoWidth": true },
            { "data": "bookTittle", "name": "bookTittle", "autoWidth": true },
            { "data": "isbn", "name": "ISBN", "autoWidth": true }, 
            {
                "data": "issueDate", "name": "IssueDate", "autoWidth": true,
                render: function (data) {
                    const date = new Date(data);
                    const day = ("0" + date.getDate()).slice(-2);
                    const month = ("0" + (date.getMonth() + 1)).slice(-2);
                    const year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            },
            {
                "data": "dueDate", "name": "DueDate", "autoWidth": true,
                render: function (data) {
                    const date = new Date(data);
                    const day = ("0" + date.getDate()).slice(-2);
                    const month = ("0" + (date.getMonth() + 1)).slice(-2);
                    const year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            }, 
            {
                "data": "returnDate", "name": "ReturnDate", "autoWidth": true,
                render: function (data) {
                    if (data === null || data === "" || data === undefined) {
                        return "<span class='text-muted'>N/A</span>";
                    } else {
                        const date = new Date(data);
                        const day = ("0" + date.getDate()).slice(-2);
                        const month = ("0" + (date.getMonth() + 1)).slice(-2);
                        const year = date.getFullYear();
                        return `${day}/${month}/${year}`;
                    }
                }
            },
            {
                "data": "fineAmount", "name": "FineAmount", "autoWidth": true,
                render: function (data) {
                    var fineColor = data == 0 ? 'text-success' : 'text-danger'
                    return `<span class=" ${fineColor}">₹${data}</span>`
                }
            }, 
            {
                "data": "isReturned", "name": "IsReturned", "autoWidth": true,
                render: function (data) {
                    var statusColor = data == 1 ? 'bg-success' : 'bg-warning' 
                    var status = data == 1 ? 'Return' : 'Not Return'
                    return `<span class="badge ${statusColor}">${status}</span>`
                }
            },
            {
                "data": null, "name": "Actions", "autoWidth": true, "orderable": false, "searchable": false,
                render: function (data, type, row) {
                    return `<button class="btn btn-primary btn-sm" onclick="openReturnDatePicker('${row.id}', '${row.returnDate}')">
                    <i class="bi bi-calendar-date"></i>
                </button>`;
                }
            }
        ],
    })

})

function openReturnDatePicker(id, currentDate) {
    let defaultDate = '';
    if (currentDate && currentDate !== "null") {
        const date = new Date(currentDate);
        const yyyy = date.getFullYear();
        const mm = ('0' + (date.getMonth() + 1)).slice(-2);
        const dd = ('0' + date.getDate()).slice(-2);
        defaultDate = `${yyyy}-${mm}-${dd}`;
    }

    Swal.fire({
        title: 'Select Return Date',
        input: 'date',
        inputLabel: 'Return Date',
        inputValue: defaultDate,
        showCancelButton: true,
        confirmButtonText: 'Save',
        cancelButtonText: 'Cancel',
        inputAttributes: {
            min: '2000-01-01',
            max: '2099-12-31'
        },
        preConfirm: (date) => {
            if (!date) {
                Swal.showValidationMessage('Please select a date');
            }
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const returnDate = result.value;

            // ✅ AJAX call to update return date
            $.ajax({
                url: '/IssuedBookHistory/UpdateReturnDate',
                type: 'POST',
                data:{ id: id, returnDate: returnDate },
                success: function (response) {
                    if (response.success) {
                        Swal.fire('Success', response.message, 'success');
                        $('#issueBookList').DataTable().ajax.reload(null, false); // refresh row
                    }
                    else {
                        Swal.fire('Error', response.message, 'error');
                    }
                },
                error: function () {
                    Swal.fire('Error', 'Failed to update return date.', 'error');
                }
            });
        }
    });
}
