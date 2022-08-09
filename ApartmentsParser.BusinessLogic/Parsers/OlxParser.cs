using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Parsers
{
    public class OlxParser : IOlxParser
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OlxParser(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository ?? throw new ArgumentNullException(nameof(apartmentRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task ParsePages(string city, int numberOfPages)
        {
            var web = new HtmlWeb();

            for (int page = 1; page <= numberOfPages; page++)
            {
                string genericLink = "https://www.olx.pl/d/nieruchomosci/mieszkania/{0}?page={1}";

                var document = web.Load(string.Format(genericLink, city, page));

                var ads = document.DocumentNode.SelectNodes("//a[@class='css-1bbgabe']");
                int numberOfAd = 0;

                foreach (var ad in ads)
                {
                    var apartment = GetSingleApartment(ad, city, numberOfAd);

                    if (await _apartmentRepository.GetByNameAsync(apartment.Name) is null)
                    {
                        await _apartmentRepository.CreateAsync(apartment);
                        _unitOfWork.SaveChanges();
                    }

                    numberOfAd++;
                }
            }
        }

        private Apartment GetSingleApartment(HtmlNode ad, string city, int numberOfAd)
        {
            Apartment apartment = new Apartment();

            var nameNode = ad.SelectNodes("//h6[@class='css-v3vynn-Text eu5v0x0']").ElementAt(numberOfAd);
            apartment.Name = nameNode.InnerText;

            apartment.City = city;

            if (!ad.Attributes.FirstOrDefault(i => i.Name.Equals("href")).Value.Contains("https"))
            {
                apartment.Link = "https://www.olx.pl/" + ad.Attributes.FirstOrDefault(i => i.Name.Equals("href")).Value;
            }
            else
            {
                apartment.Link = ad.Attributes.FirstOrDefault(i => i.Name.Equals("href")).Value;
            }

            return apartment;
        }
    }
}
