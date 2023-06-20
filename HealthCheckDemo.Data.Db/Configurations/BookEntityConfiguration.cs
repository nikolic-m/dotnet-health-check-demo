using HealthCheckDemo.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCheckDemo.Data.Db.Configurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        private Book[] _books = new Book[]
        {
            new Book
            {
                Id = Guid.Parse("688b0616-0fb2-4c0e-a8a4-4866a8fec2c9"),
                Name = "Tomorrow, and Tomorrow, and Tomorrow - Gabrielle Zevin",
                ISBN = "9780593321201",
            },
            new Book
            {
                Id = Guid.Parse("20aea7d3-3c03-4e85-be87-8a39b133ebec"),
                Name = "Remarkably Bright Creatures - Shelby Van Pelt",
                ISBN = "9780063204157",
            },
            new Book
            {
                Id = Guid.Parse("98d11ba9-2cc5-4264-ba39-c421feaeb316"),
                Name = "Book Lovers - Emily Henry",
                ISBN = "9780593334836",
            },
            new Book
            {
                Id = Guid.Parse("1189cc7d-c03d-4e70-a607-2101616ca1e3"),
                Name = "Sea of Tranquility - Emily St. John Mandel",
                ISBN = "9780593321447",
            },
        };

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(_books);
        }
    }
}
