//using JWT.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentService.AuthFolder;
using StudentService.Data;
using StudentService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentService.AuthFolder
{
    public class Auth : IAuth
    { 
         /*private readonly string username = "USTGlobal";
         private readonly string password = "pwd";*/
         private readonly string key;
        private readonly StudentServiceContext _userCredentials;
        public Auth(string key, StudentServiceContext userCredentials)
         {
            _userCredentials = userCredentials;
            this.key = "This_is_my_first_Test_Key_for_jwt_token";
         }
        public Auth(StudentServiceContext userCredentials)
        {
            _userCredentials = userCredentials;
           // this.key = "This_is_my_first_Test_Key_for_jwt_token";
        }
        public string Authentication(UserCredentials user)
        {
            //  Use Any() to check if there are matching records instead of directly assigning IQueryable to a bool
            bool uExists = _userCredentials.UserCredentials.Any(u => u.UserName == user.UserName);
            bool pExists = _userCredentials.UserCredentials.Any(u => u.Password == user.Password);

            if (!uExists || !pExists) // Corrected the condition logic
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // 3. Create JWTdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                          new Claim(ClaimTypes.Name, user.UserName),
                          new Claim(ClaimTypes.Role, user.Role)

                        // Use user.UserName instead of undefined 'username'
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
     }
    }
