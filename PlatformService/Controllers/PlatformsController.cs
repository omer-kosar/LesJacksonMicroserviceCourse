using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Dto;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepostiory _repostiory;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        public PlatformsController(IPlatformRepostiory repostiory, IMapper mapper,ICommandDataClient commandDataClient)
        {
            _repostiory = repostiory;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            var platformItems = await _repostiory.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }
        [HttpGet("{id:int}", Name = "GetPlatformById")]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            var platformItem = await _repostiory.GetPlatformById(id);
            if (platformItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDto model)
        {
            var platformEntity = _mapper.Map<Platform>(model);
            await _repostiory.CreatePlatform(platformEntity);
            var platformReadItem = _mapper.Map<PlatformReadDto>(platformEntity);
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadItem);
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadItem.Id }, platformReadItem);
        }
    }
}
