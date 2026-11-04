namespace ObligatoriskOpgave.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}, Role: {Role}";
        }
    }
}
