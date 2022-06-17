using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Models;
using System;

namespace ApartmentsParser.BusinessLogic.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository ?? throw new ArgumentNullException(nameof(apartmentRepository));
        }

        public ApartmentsCollectionModel GetAll()
        {
            var model = new ApartmentsCollectionModel();
            model.Apartments = _apartmentRepository.GetAll();
            return model;
        }
    }
}