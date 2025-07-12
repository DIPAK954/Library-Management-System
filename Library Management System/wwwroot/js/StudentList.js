toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right", // Other options below
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",       // in milliseconds
    "hideDuration": "1000",
    "timeOut": "5000",           // How long to display toast
    "extendedTimeOut": "1000",
    "showEasing": "swing",       // Animation
    "hideEasing": "linear",
    "showMethod": "show",      // fadeIn, slideDown, etc.
    "hideMethod": "fadeOut"
};

$(document).ready(function () {
    $('#studentList').DataTable({
        "ajax": {
            "url": "/Student/StudentGrid",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "visible": false },
            { "data": "fullName" },
            { "data": "email" }, 
            { "data": "phoneNumber" },
            { "data": "createdAt" },
            {
                "data": "actions",
                render: function (data, type, row) {
                    return `<a href="/Student/Details/${row.id}" class="btn btn-info btn-sm"><i class="bi bi-info-square"></i></a> 
                            <button class="btn btn-danger btn-sm" onclick="deleteStudent('${row.id}', '${row.fullName}')"><i class="bi bi-trash"></i></button>`;
                },
                "orderable": false
            }
        ]

    })
})

function deleteStudent(id, fullName) {
    Swal.fire({
        title: "Are you sure?",
        text: `You want to delete ${fullName} !!`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Student/Delete/${id}`,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        toastr.success(response.message);
                        $('#studentList').DataTable().ajax.reload();
                    }
                    else {
                        toastr.error(response.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error(`${error} to deleting user. Please try again later.`);
                }
            });
        }
    })
}