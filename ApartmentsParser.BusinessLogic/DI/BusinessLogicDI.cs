using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.BusinessLogic.Parsers;
using ApartmentsParser.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentsParser.BusinessLogic.DI
{
    public static class BusinessLogicDI
    {
        public static void AddBusinessLogic(this IServiceCollection service)
        {
            service.AddTransient<IOtodomParser, OtodomParser>();
            service.AddTransient<IOlxParser, OlxParser>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IApartmentService, ApartmentService>();
        }
    }
}
