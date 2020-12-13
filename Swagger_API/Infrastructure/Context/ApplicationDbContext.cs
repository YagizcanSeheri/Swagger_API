 using Microsoft.EntityFrameworkCore;
using Swagger_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Infrastructure.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) { }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<AppUser> Users { get; set; }

    }
}
