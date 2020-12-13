using Microsoft.EntityFrameworkCore;
using Swagger_API.Infrastructure.Context;
using Swagger_API.Infrastructure.Repositories.Abstraction;
using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Repositories.Concrete
{
    public class EfNationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public EfNationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateNationalPark(NationalPark nationalPark)
        {
            await _context.NationalParks.AddAsync(nationalPark);
            return await Save();
        }

        public async Task<bool> DeleteNationalPark(NationalPark nationalPark)
        {
            var obj = await _context.NationalParks.FirstOrDefaultAsync(x => x.Id == nationalPark.Id);
            if (obj != null)
            {
                obj.Status = Status.Passive;
                obj.DeleteDate = DateTime.Now;
                return await Save();
            }
            return false;
        }

        public async Task<NationalPark> GetNationalPark(int id)
        {
            return await _context.NationalParks.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ICollection<NationalPark>> GetNationalParks()
        {
            return await _context.NationalParks.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<bool> NationalParkExists(int id)
        {
            return await _context.NationalParks.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> NationalParksExists(string name)
        {
            return await _context.NationalParks.AnyAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> UpdateNationalPark(NationalPark nationalPark)
        {
            var parkObj = await _context.NationalParks.FirstOrDefaultAsync(x => x.Id == nationalPark.Id);
            if (parkObj == null)
            {
                return false;
            }
            else
            {
                parkObj.Status = Status.Modified;
                parkObj.UpdateDate = DateTime.Now;
                return await Save();
            }
        }
    }
}
