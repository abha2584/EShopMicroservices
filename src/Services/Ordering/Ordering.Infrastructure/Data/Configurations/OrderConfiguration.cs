
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.Id);

			builder.Property(o => o.Id)
				.HasConversion(
					id => id.Value,
					dbId => OrderId.Of(dbId));

			builder.HasOne<Customer>()
				.WithMany()
				.HasForeignKey(o => o.CustomerId)
				.IsRequired();

			builder.HasMany(o => o.OrderItems)
				.WithOne()
				.HasForeignKey(oi => oi.OrderId);

			builder.ComplexProperty(o => o.OrderName, orderNameBuilder =>
			{
				orderNameBuilder.Property(on => on.Value)
					.HasColumnName(nameof(Order.OrderName))
					.HasMaxLength(100)
					.IsRequired();
			});


			builder.ComplexProperty(o => o.ShippingAddress, shippingAddressBuilder =>
			{
				shippingAddressBuilder.Property(sa => sa.FirstName)
					.HasMaxLength(50)
					.IsRequired();
				shippingAddressBuilder.Property(sa => sa.LastName)
				.HasMaxLength(50)
					.IsRequired();
				shippingAddressBuilder.Property(sa => sa.EmailAddress)
					.HasMaxLength(50);
				shippingAddressBuilder.Property(sa => sa.AddressLine)
					.HasMaxLength(200)
					.IsRequired();
				shippingAddressBuilder.Property(sa => sa.Country)
					.HasMaxLength(50);

				shippingAddressBuilder.Property(sa => sa.State)
					.HasMaxLength(50);
				shippingAddressBuilder.Property(sa => sa.ZipCode)
					.HasMaxLength(5)
					.IsRequired();
			});

			builder.ComplexProperty(o => o.BillingAddress, BillingAddressBuilder =>
			{
				BillingAddressBuilder.Property(sa => sa.FirstName)
					.HasMaxLength(50)
					.IsRequired();
				BillingAddressBuilder.Property(sa => sa.LastName)
				.HasMaxLength(50)
					.IsRequired();
				BillingAddressBuilder.Property(sa => sa.EmailAddress)
					.HasMaxLength(50);
				BillingAddressBuilder.Property(sa => sa.AddressLine)
					.HasMaxLength(200)
					.IsRequired();
				BillingAddressBuilder.Property(sa => sa.Country)
					.HasMaxLength(50);

				BillingAddressBuilder.Property(sa => sa.State)
					.HasMaxLength(50);
				BillingAddressBuilder.Property(sa => sa.ZipCode)
					.HasMaxLength(5)
					.IsRequired();
			});

			builder.ComplexProperty(o => o.Payment, paymentBuilder =>
			{

				paymentBuilder.Property(p => p.CardName).HasMaxLength(50);
				paymentBuilder.Property(p => p.CardNumber)
					.HasMaxLength(24)
					.IsRequired();
				paymentBuilder.Property(p => p.PaymentMethod);
				
				paymentBuilder.Property(p => p.Expiration)
					.HasMaxLength(10);
				paymentBuilder.Property(p => p.CVV)
					.HasMaxLength(4)
					.IsRequired();
			});

			builder.Property(o => o.Status)
				.HasDefaultValue(OrderStatus.Draft)
				.HasConversion(
					s => s.ToString(),
					dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

			builder.Property(o => o.TotalPrice);
		}
	}
}
