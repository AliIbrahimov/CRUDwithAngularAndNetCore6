using Microsoft.EntityFrameworkCore;
using SuperHero_API.Models;

namespace SuperHero_API.DataAccess;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options):base(options){	}
	public DbSet<SuperHero> SuperHeros { get; set; }
}
