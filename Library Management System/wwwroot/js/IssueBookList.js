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
                "data": "status", "name": "Status", "autoWidth": true,
                render: function (data) {
                    var statusColor = data == 1 ? 'bg-success' : 'bg-warning' 
                    var status = data == 1 ? 'Return' : 'Not Return'
                    return `<span class="badge ${statusColor}">${status}</span>`
                }
            },
            {
                "data": "actions", "name": "Actions", "autoWidth": true, "orderable": false, "searchable": false,
                render: function (row) {
                    return`<button class="btn btn-warning btn-sm"><i class="bi bi-pencil-square"></i></button>`
                }
            }
        ],
    })

})