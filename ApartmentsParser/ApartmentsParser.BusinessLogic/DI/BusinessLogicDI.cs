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
            service.AddTransient<IApartmentsParser, OtodomParser>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
