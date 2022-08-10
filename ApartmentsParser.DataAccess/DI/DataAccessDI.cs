using ApartmentsParser.DataAccess.Data;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentsParser.DataAccess.DI
{
    public static class DataAccessDi
    {
        public static void AddDataLogic(this IServiceCollection service, IConfiguration config)
        {
            service.AddTransient<IApartmentRepository, ApartmentRepository>();

            service.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
