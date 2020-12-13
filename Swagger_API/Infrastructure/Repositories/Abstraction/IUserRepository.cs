using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Repositories.Abstraction
{
    public interface IUserRepository
    {
        AppUser Authentication(string userName, string password);
        AppUser Register(string userName, string password);

        bool IsUniqueUser(string userName);


    }
}
