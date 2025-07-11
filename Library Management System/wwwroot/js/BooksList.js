
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
                "data": "coverImagePath",
                render: function (data) {
                    return `<img src="${data}" style="height: 70px; width: 50px; object-fit: cover;" />`;
                },
                "orderable": false
            },
            { "data": "title" },
            { "data": "isbn" }, 
            { "data": "genre" }, 
            { "data": "language" },
            { "data": "author" }, 
            { "data": "totalCopies" }, 
            { "data": "availableCopy" },
            {
                "data": "status",
                render: function (data, type, row) {
                    const statusText = data == 0 ? 'Available' : 'Unavailable';
                    const badgeClass = data == 0 ? 'text-bg-success' : 'text-bg-danger';
                    return `<span class="badge ${badgeClass}" style="cursor:pointer;" onclick="toggleStatus('${row.id}', '${row.title}')">${statusText}</span>`;
                }
            },
            {
                "data": "action",
                render: function (data, type, row) {
                    return `<a href="/Book/Details/${row.id}" class="btn btn-info btn-sm"><i class="bi bi-info-square"></i></a> 
                            <a href="/Book/Create/${row.id}" class="btn btn-warning btn-sm"><i class="bi bi-pencil-square"></i></a> 
                            <button class="btn btn-danger btn-sm" onclick="deleteBook('${row.id}', '${row.title}')"><i class="bi bi-trash"></i></button>`;
                },
                "orderable": false
            }
        ]
    })
})


function deleteBook(id, title) {
    Swal.fire({
        title: 'Are you sure ?',
        text: `You want to delete ${title} book ?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Book/DeleteBook/${id}`,
                type: 'DELETE',
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        `${title} has been deleted.`,
                        'success'
                    )
                    $('#bookTable').DataTable().ajax.reload();
                },
                error: function (xhr, status, error) {
                    Swal.fire(
                        'Error!',
                        `Failed to delete ${title}. Please try again later.`,
                        'error'
                    )
                }
            });
        }
    })
}
function toggleStatus(id, title) {
    Swal.fire({
        title: 'Are you sure ?',
        text: `You want to update ${title} book status`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, update it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Book/BookStatus/${id}`,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message);
                    }
                    $('#bookTable').DataTable().ajax.reload();
                },
                error: function (xhr, status, error) {
                    toastr.error(`${error} to updating status of ${title}. Please try again later.`);
                }
            });
        }
    })
}
