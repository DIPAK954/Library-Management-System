$(document).ready(function () {
    $('#bookTable').DataTable({
        "responsive": true,
        "ajax": {
            "url": "/Book/AllBooks",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            {
                "data": "coverImage",
                render: function (data) {
                    return `<img src="wwwroot/BooksImg/${data}" style="height: 70px; width: 50px; object-fit: cover;" />`;
                }
            },
            { "data": "title" },
            { "data": "isbn" }, 
            { "data": "genre" }, 
            { "data": "language" },
            { "data": "author" }, 
            { "data": "totalCopies" }, 
            { "data": "availableCopies" },
            {
                "data": "status",
                render: function (data) {
                    return data == 0 ? "Available" : "Archive"
                }
            },
            {
                "data": "action",
                render: function (data, type, row) {
                    return `<a href="/Book/Details/${row.id}" class="btn btn-info btn-sm">Details</a> 
                            <a href="/Book/Create/${row.id}" class="btn btn-warning btn-sm">Edit</a> 
                            <button class="btn btn-danger btn-sm" onclick="deleteBook(${row.id})">Delete</button>`;
                },
                "orderable": false
            }
        ]
    })
})
