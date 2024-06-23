// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;

// namespace WebApplication1;

// public class AuthHelpers
// {
//     public string GenerateJWTToken(User user) {
//     var claims = new List<Claim> {
//         // new Claim(ClaimTypes.Sid, user.Id.ToString()),
//         new Claim(ClaimTypes.Name, user.Name),
//     };
//     var jwtToken = new JwtSecurityToken(
//         claims: claims,
//         notBefore: DateTime.UtcNow,
//         expires: DateTime.UtcNow.AddDays(30),
//         signingCredentials: new SigningCredentials(
//             new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes("test-app")
//                 ),
//             SecurityAlgorithms.HmacSha256Signature)
//         );
//     return new JwtSecurityTokenHandler().WriteToken(jwtToken);
// }

// }
