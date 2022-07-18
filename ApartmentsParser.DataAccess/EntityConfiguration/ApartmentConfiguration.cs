using ApartmentsParser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentsParser.DataAccess.EntityConfiguration
{
    class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.City).IsRequired().HasMaxLength(50);
            builder.Property(f => f.RoomsNumber).IsRequired();
            builder.Property(f => f.Link).IsRequired().HasMaxLength(250);
        }
    }
}