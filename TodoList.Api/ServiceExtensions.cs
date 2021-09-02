using Microsoft.Extensions.DependencyInjection;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.Repositories;

namespace TodoList.Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
