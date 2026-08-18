using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
	public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
			if (coupon == null)
			{
				coupon = new Coupon
				{
					ProductName = request.ProductName,
					Description = "No discount available",
					Amount = 0
				};
			}


			logger.LogInformation("Discount retrieved for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
			var couponModel = new CouponModel
			{
				Id = coupon.Id,
				ProductName = coupon.ProductName,
				Description = coupon.Description,
				Amount = (double)coupon.Amount
			};

			return couponModel;

		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{

			if (request.Coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
			}
			var coupon = new Coupon
			{
				Id = request.Coupon.Id,
				ProductName = request.Coupon.ProductName,
				Description = request.Coupon.Description,
				Amount = (decimal)request.Coupon.Amount
			};

			dbContext.
				Coupons.Update(coupon);
			await dbContext.SaveChangesAsync();
			logger.LogInformation("Discount updated for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

			var couponModel = new CouponModel
			{
				Id = coupon.Id,
				ProductName = coupon.ProductName,
				Description = coupon.Description,
				Amount = (double)coupon.Amount
			};

			return couponModel;
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			if(request.Coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
			}
			var coupon = new Coupon
			{
				ProductName = request.Coupon.ProductName,
				Description = request.Coupon.Description,
				Amount = (decimal)request.Coupon.Amount
			};
			
			dbContext.
				Coupons.Add(coupon);
			await dbContext.SaveChangesAsync();
			logger.LogInformation("Discount created for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

			var couponModel = new CouponModel
			{
				Id = coupon.Id,
				ProductName = coupon.ProductName,
				Description = coupon.Description,
				Amount = (double)coupon.Amount
			};

			return couponModel;

		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
			if(coupon == null)
			{
				logger.LogWarning("Discount not found for ProductName: {ProductName}", request.ProductName);
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount not found for ProductName: {request.ProductName}"));

			}
			dbContext.Coupons.Remove(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount deleted for ProductName: {ProductName}", request.ProductName);	
			return new DeleteDiscountResponse
			{
				Success = true
			};
		}
	}
}
