using ApartmentsParser.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        public Task<Apartment> GetByNameAsync(string name);

        public Task CreateAsync(Apartment apartment);

        public Task DeleteAsync(string name);

        public Task<List<Apartment>> GetAllAsync();
    }
}
