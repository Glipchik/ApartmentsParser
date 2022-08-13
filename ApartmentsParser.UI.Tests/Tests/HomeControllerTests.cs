using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.Domain.Entities;
using ApartmentsParser.Domain.Models;
using ApartmentsParser.UI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsParser.UI.Tests.Tests
{
    public class HomeControllerTests
    {
        private Mock<IApartmentService> _apartmentService;
        private HomeController _homeController;
        [SetUp]
        public void Setup()
        {
            _apartmentService = new Mock<IApartmentService>();
            _homeController = new HomeController(_apartmentService.Object);
        }

        [Test]
        public void Index_WhenMethodIsInvoked_ShouldReturnViewWithApartmentCollectionModel()
        {
            var apartments = new List<Apartment>() { new Apartment() { Name = "New apartment" } };
            var apartmentsCollectionModel = new ApartmentsCollectionModel() { Apartments = apartments };
            _apartmentService.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(apartmentsCollectionModel));

            var result = _homeController.Index() as ViewResult;

            result.Should().NotBeNull();
            result.Model.Should().BeAssignableTo<ApartmentsCollectionModel>();
        }
    }
}