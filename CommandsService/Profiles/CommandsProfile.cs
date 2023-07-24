using AutoMapper;
using CommandsService.Dto;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CommandsService.Profiles
{
    public class CommandsProfile:Profile
    {
        public CommandsProfile()
        {
            //source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command,CommandReadDto>();    
        }
    }
}
