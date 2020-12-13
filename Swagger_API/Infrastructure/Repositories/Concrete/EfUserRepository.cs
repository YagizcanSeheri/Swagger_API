using Microsoft.IdentityModel.Tokens;
using Swagger_API.Infrastructure.Context;
using Swagger_API.Infrastructure.CustomSettings;
using Swagger_API.Infrastructure.Repositories.Abstraction;
using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Repositories.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        public EfUserRepository(ApplicationDbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }
        public AppUser Authentication(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            if (user == null)
            {
                return null;
            }
            else
            {
                //if giremez ise token uret
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                var tokenDescription = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescription);
                user.Token = tokenHandler.WriteToken(token);
                return user;
            }
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);
            if (user == null)
                return true;
            else
                return false;
        }

        public AppUser Register(string userName, string password)
        {
            AppUser userObj = new AppUser()
            {
                UserName = userName,
                Password = password
                //Role = role; Role managament ısteyenle yapabılır
            };
            _context.Users.Add(userObj);
            _context.SaveChanges();
            return userObj;
        }
    }
}
