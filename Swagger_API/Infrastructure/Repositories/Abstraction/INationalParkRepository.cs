using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Repositories.Abstraction
{
    public interface INationalParkRepository
    {
        Task<ICollection<NationalPark>> GetNationalParks();

        Task<NationalPark> GetNationalPark(int id);

        Task<bool> NationalParkExists(int id);

        Task<bool> NationalParksExists(string name);

        Task<bool> CreateNationalPark(NationalPark nationalPark);
        Task<bool> UpdateNationalPark(NationalPark nationalPark);
        Task<bool> DeleteNationalPark(NationalPark nationalPark);

        Task<bool> Save();
    }
}
