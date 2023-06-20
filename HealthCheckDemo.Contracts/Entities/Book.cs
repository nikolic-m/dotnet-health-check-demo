namespace HealthCheckDemo.Contracts.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string ISBN { get; set; } = default!;
    }
}
