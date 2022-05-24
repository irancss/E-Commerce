using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<Coupon> GetDiscountAsync(string productName)
    {
        await using (var connection =
                     new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
        {
            var coupon =
                await connection.QueryFirstOrDefaultAsync<Coupon>(
                    "Select * From Coupon Where ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            return coupon;
        }
    }

    public async Task<bool> CreateDiscountAsync(Coupon coupon)
    {
        await using (var connection =
                     new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
        {
            var createCoupon = await connection.ExecuteAsync(
                "Insert Intro Coupon (ProductName,Description,Amount) Values (@ProductName,@Descirption,@Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (createCoupon == 0)
            {
                return false;
            }
            return true;
        }
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        await using (var connection =
                     new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
        {
            var updateCoupon = await connection.ExecuteAsync(
                "Update Coupon Set ProductName=@ProductName, Descirption=@Descirption, Amount=@Amount Where Id=@Id",
                new { ProdutName = coupon.ProductName, Descirption = coupon.Description, Amount = coupon.Amount });

            if (updateCoupon == 0)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        await using (var connection =
                     new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
        {
            var deleteCoupon = await connection.ExecuteAsync("Delete From Coupon Where ProductName=@ProductName",
                new { ProductName = productName });

            if (deleteCoupon == 0)
            {
                return false;
            }

            return true;
        }
    }
}