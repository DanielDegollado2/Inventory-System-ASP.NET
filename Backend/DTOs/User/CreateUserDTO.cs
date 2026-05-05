namespace Backend.DTOs.User
{
    public class CreateUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
