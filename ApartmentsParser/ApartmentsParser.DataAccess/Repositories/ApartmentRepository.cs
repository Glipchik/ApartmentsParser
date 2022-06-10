using ApartmentsParser.DataAccess.Data;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsParser.DataAccess.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DataContext _context;

        public ApartmentRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Apartment apartment)
        {
            await _context.Apartments.AddAsync(apartment);
        }

        public void Delete(string name)
        {
            var apartment = _context.Apartments.FirstOrDefault(e => e.Name.Equals(name));

            _context.Remove(apartment);
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _context.Apartments;
        }

        public Apartment GetByName(string name)
        {
            return _context.Apartments.FirstOrDefault(e => e.Name.Equals(name));
        }
    }
}
