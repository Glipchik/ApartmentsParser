using ApartmentsParser.Domain.Entities;
using System.Collections.Generic;

namespace ApartmentsParser.Domain.Models
{
    public class ApartmentsCollectionModel
    {
        public IEnumerable<Apartment> Apartments { get; set; }
    }
}
