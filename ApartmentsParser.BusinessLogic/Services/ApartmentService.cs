using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using ApartmentsParser.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository ?? throw new ArgumentNullException(nameof(apartmentRepository));
        }

        public async Task<ApartmentsCollectionModel> GetAll()
        {
            List<Apartment> apartments = await _apartmentRepository.GetAllAsync();
            var model = new ApartmentsCollectionModel
            {
                Apartments = apartments
            };
            return model;
        }
    }
}