using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IOtodomParser
    {
        public Task ParsePages(string city, int numberOfPages);
    }
}
