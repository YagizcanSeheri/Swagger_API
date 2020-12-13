using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swagger_API.Infrastructure.Context;
using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Seeding
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.NationalParks.Any())
                {
                    return;
                }

                context.NationalParks.AddRange(
                    new NationalPark
                    {
                        
                        Name = "Bolu Yedi Göller",
                        Description = "Eski tadı kalmadı"
                    },
                    new NationalPark
                    {
                        
                        Name = "Saklı Kanyon",
                        Description = "Muazzam bir yer"
                    },
                    new NationalPark
                    {
                        
                        Name = "Kaz Dağları",
                        Description = "Hey gidi hey"
                    });

                context.SaveChanges();
            }
        }
    }
}
