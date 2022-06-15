using System.Collections.Generic;

namespace ApartmentsParser.Domain.ConfigurationObjects
{
    public class JobsConfiguration
    {
        public string RepeatInterval { get; set; }
        public int NumberOfPages { get; set; }
        public List<string> ListOfCities { get; set; }
    }
}
