using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.DataAccess.Interfaces;
using ApartmentsParser.Domain.Entities;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsParser.BusinessLogic.Parsers
{
    public class OtodomParser : IOtodomParser
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OtodomParser(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository ?? throw new ArgumentNullException(nameof(apartmentRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task ParsePages(string city, int numberOfPages)
        {
            var web = new HtmlWeb();

            for (int page = 1; page <= numberOfPages; page++)
            {
                string genericLink = "https://www.otodom.pl/pl/oferty/sprzedaz/mieszkanie/{0}?page={1}";
                
                var document = web.Load(string.Format(genericLink, city, page));

                var ads = document.DocumentNode.SelectNodes("//a[@class='css-rvjxyq es62z2j14']");
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

        private static Apartment GetSingleApartment(HtmlNode ad, string city, int numberOfAd)
        {
            Apartment apartment = new Apartment();

            var nameNode = ad.SelectNodes("//div[@class='css-xw6zw6 es62z2j12']").ElementAt(numberOfAd);
            apartment.Name = nameNode.InnerText;

            var roomsNumberNode = ad.SelectNodes("//div[@class='css-i38lnz eclomwz1']").ElementAt(numberOfAd);
            int itemNumber = 0;
            foreach (var item in roomsNumberNode.ChildNodes)
            {
                if (itemNumber == 2)
                {
                    var stringRoomsNumber = item.InnerText.Split()[0];
                    if (stringRoomsNumber.Equals("10+"))
                    {
                        apartment.RoomsNumber = 10;
                    }
                    else
                    {
                        apartment.RoomsNumber = int.Parse(item.InnerText.Split()[0]);
                    }
                    break;
                }

                if (!item.Name.Equals("style"))
                {
                    itemNumber++;
                }
            }

            apartment.City = city;

            apartment.Link = "https://www.otodom.pl/" + ad.Attributes.FirstOrDefault(i => i.Name.Equals("href")).Value;
            return apartment;
        }
    }
}
