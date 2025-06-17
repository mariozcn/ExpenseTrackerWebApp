using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MVC_first.Models
{
    public class ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; }
    }
}
