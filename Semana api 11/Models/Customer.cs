namespace Semana_api_11.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
