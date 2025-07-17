function requestBook(bookId) {
    Swal.fire({
        title: "Request Book",
        text: "Are you sure you want to request this book?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#28a745",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, request it!",
        cancelButtonText: "No, cancel"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/StudentBookRequest/AddRequestBook/${bookId}`,
                type: 'POST',
                headers: {
                    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            title: "Requested!",
                            text: response.message,
                            icon: "success",
                            confirmButtonColor: "#28a745"
                        });
                    } else {
                        Swal.fire({
                            title: "Error!",
                            text: response.message,
                            icon: "error",
                            confirmButtonColor: "#d33"
                        });
                    }
                },
                error: function (error) {
                    toastr.error(error)
                }
            });
        }
    });
}
