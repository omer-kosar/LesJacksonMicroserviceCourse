using AutoMapper;
using CommandsService.Data;
using CommandsService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepo _repository;
        public PlatformsController(ICommandRepo repository,IMapper mapper)
        {
            _repository= repository;
            _mapper= mapper;
        }
        [HttpGet]
        public IActionResult GetPlatforms()
        {
            var platformItems=_repository.GetAllPlaforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }
    }
}
