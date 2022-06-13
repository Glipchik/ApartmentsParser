using ApartmentsParser.BusinessLogic.Interfaces;
using ApartmentsParser.DataAccess.Data;
using System;

namespace ApartmentsParser.BusinessLogic.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
