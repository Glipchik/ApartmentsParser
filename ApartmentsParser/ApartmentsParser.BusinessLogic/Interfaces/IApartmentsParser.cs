using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IApartmentsParser
    {
        public Task ParsePages(string city, int numberOfPages);
    }
}
