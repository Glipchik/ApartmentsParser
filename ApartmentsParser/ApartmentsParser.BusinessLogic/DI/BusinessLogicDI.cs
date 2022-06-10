using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.BusinessLogic.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentsParser.BusinessLogic.DI
{
    public static class BusinessLogicDI
    {
        public static void AddBusinessLogic(this IServiceCollection service)
        {
            service.AddTransient<IOtodomParser, OtodomParser>();
        }
    }
}
