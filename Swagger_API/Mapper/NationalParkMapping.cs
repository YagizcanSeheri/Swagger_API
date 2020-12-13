using AutoMapper;
using Swagger_API.Models;
using Swagger_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_API.Mapper
{
    public class NationalParkMapping:Profile
    {
        public NationalParkMapping()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
        }
    }
}
