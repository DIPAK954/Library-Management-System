using library.DataModel;
using Library.Common.Models;
using Microsoft.EntityFrameworkCore;

public class FineUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public FineUpdateService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

                var issuedBooks = await _context.IssuedBooks
                    .Where(b => !b.IsReturned && DateTime.Now > b.DueDate)
                    .ToListAsync();

                foreach (var book in issuedBooks)
                {
                    // Skip if book is already marked paid for LateReturn or LostBook
                    if (book.FineType == (int)FineType.LateReturn || book.FineType == (int)FineType.LostBook)
                        continue;

                    int overdueDays = (DateTime.Now.Date - book.DueDate.Date).Days;
                    book.FineAmount = overdueDays * 10;
                    if (book.FineAmount > 0)
                    {
                        book.IsFinePaid = false; // Mark as unpaid if fine is applicable
                    }
                }

                await _context.SaveChangesAsync();
            }

            // Run once every 24 hours
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}
