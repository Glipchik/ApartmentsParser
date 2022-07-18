using ApartmentsParser.DataAccess.Data;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task DeleteAsync(string name)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(e => e.Name.Equals(name));

            _context.Remove(apartment);
        }

        public async Task<List<Apartment>> GetAllAsync()
        {
            List<Apartment> apartments = await Task.Run(() => _context.Apartments.ToListAsync<Apartment>());

            return apartments;
        }

        public Task<Apartment> GetByNameAsync(string name)
        {
            return _context.Apartments.FirstOrDefaultAsync(e => e.Name.Equals(name));
        }
    }
}
