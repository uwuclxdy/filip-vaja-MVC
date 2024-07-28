using Microsoft.EntityFrameworkCore;

namespace filip_vaja_MVC.Models;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
        
    }
}