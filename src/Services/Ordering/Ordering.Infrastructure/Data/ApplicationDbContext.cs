
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext ,IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public DbSet<Customer> Customers => Set<Customer>();

		public DbSet<Order> Orders => Set<Order>();

		public DbSet<OrderItem> OrderItems => Set<OrderItem>();

		public DbSet<Product> Products => Set<Product>();


	}
}
