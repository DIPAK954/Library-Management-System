$(document).ready(function () {
    // Fetch and display Borrow and Return Chart Data
    fetch('/Dashboard/GetBorrowReturnChartData')
        .then(res => res.json())
        .then(data => {
            const ctx = document.getElementById('borrowReturnChart').getContext('2d');
            new Chart(ctx, {
                type: 'line',
                data: {
                    labels: data.labels,
                    datasets: [
                        {
                            label: 'Borrowed',
                            data: data.borrowed,
                            borderColor: 'blue',
                            backgroundColor: 'rgba(0, 123, 255, 0.2)',
                            fill: true,
                            tension: 0.3
                        },
                        {
                            label: 'Returned',
                            data: data.returned,
                            borderColor: 'green',
                            backgroundColor: 'rgba(40, 167, 69, 0.2)',
                            fill: true,
                            tension: 0.3
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false, // IMPORTANT for custom height to work
                    plugins: {
                        legend: { position: 'bottom' }
                    }
                }
            });
        });
});

// Fetch and display Book by Category Chart Data
fetch('/Dashboard/GetGenreDistribution')
    .then(res => res.json())
    .then(data => {
        const pieCtx = document.getElementById('categoryPieChart').getContext('2d');
        new Chart(pieCtx, {
            type: 'pie',
            data: {
                labels: data.labels,
                datasets: [{
                    data: data.counts,
                    backgroundColor: [
                        '#4e73df', '#1cc88a', '#36b9cc', '#f6c23e',
                        '#e74a3b', '#858796', '#20c997', '#6f42c1'
                    ]
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false, // IMPORTANT for custom height to work
                plugins: {
                    legend: { position: 'bottom' }
                }
            }
        });
    });