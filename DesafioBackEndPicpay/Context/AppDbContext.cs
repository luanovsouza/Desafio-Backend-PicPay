using DesafioBackEndPicpay.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndPicpay;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}