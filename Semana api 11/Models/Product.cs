namespace Semana_api_11.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Activo { get; set; } = true;
    }
}
