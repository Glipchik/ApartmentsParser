using ApartmentsParser.BusinessLogic.Services;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Tests.Tests
{
    public class ApartmentServiceTests
    {
        private Mock<IApartmentRepository> _apartmentRepository;
        private ApartmentService _apartmentService;

        [SetUp]
        public void Setup()
        {
            _apartmentRepository = new Mock<IApartmentRepository>();
            _apartmentService = new ApartmentService(_apartmentRepository.Object);
        }

        [Test]
        public void GetAllAsync_WhenMethodIsInvoked_ShouldReturnAllApartments()
        {
            var apartments = new List<Apartment>() { new Apartment() { Name = "New apartment" } };
            _apartmentRepository.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(apartments));

            var result = _apartmentService.GetAllAsync().Result.Apartments;

            result.Should().BeEquivalentTo(apartments);
        }
    }
}