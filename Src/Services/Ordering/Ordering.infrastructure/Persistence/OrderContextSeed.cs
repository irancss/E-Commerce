using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrder());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed Database successfully");
            }
        }


        public static IEnumerable<Order> GetPreConfiguredOrder()
        {
            return new List<Order>()
            {
                new Order()
                {
                    UserName = "swn",
                    FirstName = "amir",
                    LastName = "varz",
                    EmailAddress = "example@gmail.com",
                    AddressLine = "Tehran",
                    Country = "Iran",
                    TotalPrice = 350
                }
            };
        }
    }


}
