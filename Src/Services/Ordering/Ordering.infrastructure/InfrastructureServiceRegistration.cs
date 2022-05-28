using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.infrastructure.Mail;
using Ordering.infrastructure.Persistence;
using Ordering.infrastructure.Repositories;

namespace Ordering.infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"));
            });


            service.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));//For mediatoR
            service.AddScoped<IOrderRepository, OrderRepository>();

            service.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            service.AddTransient<IEmailService, EmailService>();

            return service;
        }
    }
}
