using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Domain.Entities;
using Phoneshop.Infrastructure.Implementations;
using PhoneshopNuget.Repository;

namespace Phoneshop.Infrastructure.Extensions
{
    public static class PhoneshopExtensions
    {
        public static IServiceCollection AddPhoneshopApiInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PhoneshopDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRepository<Phone>, PhoneRepository>();

            return services;
        }

        public static IServiceCollection AddPhoneshopBlazorInfrastructure(this IServiceCollection services, string baseUrl)
        {
            services.AddScoped<Endpoints<Phone>, PhoneEndpoints>();
            services.AddPhoneShopApiClient(baseUrl);

            return services;
        }
    }
}
