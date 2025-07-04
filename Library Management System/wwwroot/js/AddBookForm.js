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

    $("#AddEditBookForm").validate({
        ignore: [], // include hidden fields (important!)
        rules: {
            Title: { required: true },
            ISBN: { required: true, minlength: 10, maxlength: 13 },
            Genre: { required: true },
            Language: { required: true },
            Author: { required: true },
            TotalCopies: { required: true, digits: true, min: 1 },
            AvailableCopy: { required: true, digits: true, min: 0 },
            Publisher: { required: true },
            PublishedDate: { required: true, date: true },
            Status: { required: true },
            Description: { required: true, minlength: 10 },
            CoverImageUrl: { required: true }
        },
        messages: {
            Title: { required: "Please enter the book title." },
            ISBN: {
                required: "Please enter the ISBN number.",
                minlength: "ISBN must be at least 10 characters.",
                maxlength: "ISBN cannot exceed 13 characters."
            },
            Genre: { required: "Please select a genre." },
            Language: { required: "Please select a language." },
            Author: { required: "Please enter the author's name." },
            TotalCopies: {
                required: "Please enter the total number of copies.",
                digits: "Must be a number.", min: "At least 1 copy required."
            },
            AvailableCopy: {
                required: "Please enter available copies.",
                digits: "Must be a number.", min: "Cannot be negative."
            },
            Publisher: { required: "Please enter the publisher name." },
            PublishedDate: { required: "Select published date.", date: "Enter a valid date." },
            Status: { required: "Please select status." },
            Description: {
                required: "Provide a description.",
                minlength: "At least 10 characters."
            },
            CoverImageUrl: { required: "Please upload a image"}
        },
        errorClass: "text-danger",
        submitHandler: function (form) {
            const formData = new FormData(form);

            //// Manually validate CoverImageUrl for new books only
            //const bookId = $("[name='Id']").val();
            //const fileInput = $("[name='CoverImageUrl']")[0];

            //if (!bookId || bookId === "00000000-0000-0000-0000-000000000000") {
            //    if (fileInput.files.length === 0) {
            //        toastr.warning("Please upload a cover image.");
            //        return false;
            //    }
            //}

            $.ajax({
                url: "/Book/AddUpdateBook",
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.success) {
                        toastr.success(response.message);
                        setTimeout(() => window.location.href = "/Book/Index", 5000);
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

            return false;
        }
    });

    // Manual submit
    $("#saveBookBtn").click(function (e) {
        e.preventDefault();
        if ($("#AddEditBookForm").valid()) {
            $("#AddEditBookForm").submit();
        }
    });
});
