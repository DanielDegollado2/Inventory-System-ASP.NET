namespace Backend.DTOs.Movement
{
    public class UpdateMovementDTO
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime? Date { get; set; }

        public string Type { get; set; } = null!;

        public int ProductId { get; set; }
    }
}
