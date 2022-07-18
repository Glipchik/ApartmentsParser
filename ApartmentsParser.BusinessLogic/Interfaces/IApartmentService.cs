using ApartmentsParser.Domain.Models;
using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        public Task<ApartmentsCollectionModel> GetAll();
    }
}
