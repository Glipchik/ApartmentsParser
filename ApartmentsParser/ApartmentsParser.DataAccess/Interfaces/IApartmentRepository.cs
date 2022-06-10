using ApartmentsParser.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.DataAccess.Interfaces
{
    public interface IApartmentRepository
    {
        public Apartment GetByName(string name);

        public Task CreateAsync(Apartment apartment);

        public void Delete(string name);

        public IEnumerable<Apartment> GetAll();
    }
}
