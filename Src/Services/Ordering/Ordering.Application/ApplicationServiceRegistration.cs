using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection _service)
        {

            _service.AddAutoMapper(Assembly.GetExecutingAssembly());
            _service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            _service.AddMediatR(Assembly.GetExecutingAssembly());


            _service.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            _service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return _service;

        }
    }
}
