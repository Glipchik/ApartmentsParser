using ApartmentsParser.Domain.Entities;
using ApartmentsParser.Domain.Models;
using System.Collections.Generic;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IApartmentService
    {
        public ApartmentsCollectionModel GetAll();
    }
}
