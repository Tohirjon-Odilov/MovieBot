using Microsoft.EntityFrameworkCore;
using MovieBot.Models;

namespace MovieBot.Persistence;

public sealed class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<Video> Videos { get; set; }
}