using Microsoft.IdentityModel.Tokens;
using ObligatoriskOpgave.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObligatoriskOpgave.Services
{
    public class AuthRepository
    {
        private List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "1234", Role = "Admin" },
            new User { Username = "Rebecca", Password = "1234", Role = "User" },
            new User { Username = "Sofie", Password = "1234", Role = "User" }
        };

        private readonly IConfiguration _config;


        public AuthRepository(IConfiguration config)
        {
            _config = config;
        }

        public object Login(LoginRequest login)
        {
            string role = "";
            User? user = _users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (login.Username == "admin" && login.Password == "1234")
            {
                role = "Admin"; //You can assign roles based on your logic
                var token = GenerateJwtToken(login.Username, role);
                return (new { token, role });
            }
            else if (login.Username == "Rebecca" && login.Password == "1234")
            {
                role = "User"; //You can assign roles based on your logic
                var token = GenerateJwtToken(login.Username, role);
                return new { token, role };
            }
            else if (login.Username == "Sofie" && login.Password == "1234")
            {
                role = "User"; //You can assign roles based on your logic
                var token = GenerateJwtToken(login.Username, role);
                return (new { token, role });
            }
            else
            {
                return "Invalid username or password.";
            }
        }


        private string GenerateJwtToken(string username, string role)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims are the pieces of information "baked" into the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role) // You can add roles here
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2), // Token is valid for 2 hours
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
