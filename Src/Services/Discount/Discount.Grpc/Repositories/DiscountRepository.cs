using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories;

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
                "Insert Into Coupon (ProductName,Description,Amount) Values (@ProductName,@Description,@Amount)",
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
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var updateCoupon = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        if (updateCoupon == 0)
        {
            return false;
        }

        return true;
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