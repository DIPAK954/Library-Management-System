$(document).ready(function () {
    // Initialize DataTable
    $('#fineListTable').DataTable({
        "ajax": {
            "url": "/StudentFine/GetStudentFines", // Adjust the URL to your API endpoint
            "type": "GET",
        },
        "columns": [
            { "data": "id", "visible": false },
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
                "render": function (data) {
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
            }
        ]
    });
});