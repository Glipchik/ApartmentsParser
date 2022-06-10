using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Interfaces
{
    public interface IOtodomParser
    {
        public Task ParseOtodomPages(string link, string city, int numberOfPages);
    }
}
