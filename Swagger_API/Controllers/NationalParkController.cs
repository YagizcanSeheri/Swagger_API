using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swagger_API.Infrastructure.Repositories.Abstraction;
using Swagger_API.Models;
using Swagger_API.Models.DTOs;

namespace Swagger_API.Controllers
{
    //ProducesResponseType=>Bir action methodu ıcerısınde bır cok donus turu ve yolu bulunma ıhtımalı yuksektır.Ornegın asagıda bulunan "GetNationalPark" methodu ıcerısınde 2 adet degisik donus tipi bulunmaktadır."ProducesREsponseType" öz niteligi kullanarak bu donus tıplerının Swagger gibi araclar tarafında web API dokumantasyonlarınada ıstemcı ıcın daha acıklayıcı yanıt ayrıntıları uretecegız.SwashBuckle ,Postman apı dokumantasyonları sunan toollarla calısır.
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _repo;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _repo = nationalParkRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of active national park list
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDTO>))]
        public async Task<IActionResult> GetNationalParks()
        {
            var nationalParkList = await _repo.GetNationalParks();
            var objDTO = new List<NationalParkDTO>();
            foreach (var item in nationalParkList)
            {
                objDTO.Add(_mapper.Map<NationalParkDTO>(item));
            }
            return Ok(objDTO);
        }

        /// <summary>
        /// Get individual National Park by id information
        /// </summary>
        /// <param name="id">The Id of National Park</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNationalPark")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<IActionResult> GetNationalPark(int id)
        {
            var obj = await _repo.GetNationalPark(id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var objDTO = _mapper.Map<NationalParkDTO>(obj);
                return Ok(objDTO);
            }
        }




        /// <summary>
        /// Create a new national park
        /// </summary>
        /// <param name="nationalParkDTO">In this process, National parks name and description information does required fileds</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNationalPark([FromBody] NationalParkDTO nationalParkDTO)
        {
            if (nationalParkDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (await _repo.NationalParksExists(nationalParkDTO.Name))
            {
                ModelState.AddModelError("", $"This {nationalParkDTO.Name} parl already exsist..! ");
                return StatusCode(404, ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDTO);

            if (!await _repo.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you creating a national park.Error was{nationalParkObj.Name} or {nationalParkObj.Description} ");
                return StatusCode(500, ModelState);
            }
            return Ok(nationalParkObj);
        }



        /// <summary>
        /// Update national park
        /// </summary>
        /// <param name="id">You must to type into field a id information</param>
        /// <param name="nationalParkDTO">In this process, national park name and description fields does requiret</param>
        /// <returns></returns>

        [HttpPut("{id}", Name = "UpdateNationalPark")]
        public async Task<IActionResult> UpdateNationalPark(int id, [FromBody] NationalParkDTO nationalParkDTO)
        {
            if (nationalParkDTO == null || nationalParkDTO.Id != id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDTO);

            if (!await _repo.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you are updating record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok(nationalParkObj);
        }




        /// <summary>
        /// Delete National Park
        /// </summary>
        /// <param name="id">National Park Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNationalPark(int id)
        {
            var nationalParkObj = await _repo.GetNationalPark(id);

            if (!await _repo.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the this record{nationalParkObj.Id}");
            }

            return Ok();
        }

    }
}
