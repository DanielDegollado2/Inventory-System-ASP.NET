namespace Backend.DTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Code { get; set; }

        public int Stock { get; set; }

        public int MinimumStock { get; set; }

        public int SupplierId { get; set; }
    }
}
