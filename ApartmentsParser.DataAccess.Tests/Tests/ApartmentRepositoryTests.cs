using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.BusinessLogic.Services;
using ApartmentsParser.DataAccess.Data;
using ApartmentsParser.DataAccess.Repositories;
using ApartmentsParser.Domain.Entities;
using ApartmentsParser.SharedData.TestsData;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Transactions;
using FluentAssertions;

namespace ApartmentsParser.DataAccess.Tests.Tests
{
    public class Tests
    {
        private DataContext _context;
        private ApartmentRepository _apartmentRepository;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder().UseSqlServer(UnitTestsConstants.TestsConnectionString).Options;
            _context = new DataContext(options);
            _apartmentRepository = new ApartmentRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

#pragma warning disable CS4014 
        [Test]
        public void CreateAsync_WhenApartmentIsSet_ShouldCreateNewApartment()
        {
            using (var scope = new TransactionScope())
            {
                var expectedApartmentName = "New apartment";
                var apartment = new Apartment() { Name = expectedApartmentName };

                _apartmentRepository.CreateAsync(apartment);
                _unitOfWork.SaveChanges();

                var result = _apartmentRepository.GetByNameAsync(expectedApartmentName).Result;

                result.Name.Should().Be(expectedApartmentName);
            }
        }

        [Test]
        public void GetByNameAsync_WhenApartmentNameIsNotCorrect_ShouldReturnNull()
        {
            using (var scope = new TransactionScope())
            {
                var expectedApartmentName = "New apartment";
                var incorrectApartmentName = "Incorrect apartment";
                var apartment = new Apartment() { Name = expectedApartmentName };

                _apartmentRepository.CreateAsync(apartment);
                _unitOfWork.SaveChanges();

                var result = _apartmentRepository.GetByNameAsync(incorrectApartmentName).Result;

                result.Should().BeNull();
            }
        }
        
        [Test]
        public void GetByNameAsync_WhenApartmentNameIsCorrect_ShouldReturnApartment()
        {
            using (var scope = new TransactionScope())
            {
                var expectedApartmentName = "New apartment";
                var apartment = new Apartment() { Name = expectedApartmentName };

                _apartmentRepository.CreateAsync(apartment);
                _unitOfWork.SaveChanges();

                var result = _apartmentRepository.GetByNameAsync(expectedApartmentName).Result;

                result.Should().Be(apartment);
            }
        }

#pragma warning restore CS4014 
    }
}