using ApartmentsParser.DataAccess.Data;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentsParser.DataAccess.DI
{
    public static class DataAccessDI
    {
        public static void AddDataLogic(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<DataContext>(options=>
            { 
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")); 
            });
            service.AddTransient<IApartmentRepository, ApartmentRepository>();
        }
    }
}
