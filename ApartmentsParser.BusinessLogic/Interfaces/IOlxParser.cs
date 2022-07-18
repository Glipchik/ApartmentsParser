using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IOlxParser
    {
        public Task ParsePages(string city, int numberOfPages);
    }
}
