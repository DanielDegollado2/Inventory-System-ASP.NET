namespace Backend.DTOs.WebHook
{
    public class WebHookDTO
    {
        public string Url { get; set; }
        public int SupplierId { get; set; }
        public string Event { get; set; }
        public bool IsActive { get; set; }
    }
}
