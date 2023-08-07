using AutoMapper;
using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;
using System.Text.Json;
using System.Windows.Input;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;

        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished: AddPlatfrom(message); break;
                default: break;
            }
            throw new NotImplementedException();
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Platform_Published": return EventType.PlatformPublished;
                default: return EventType.Undetermined;
            }
        }
        private void AddPlatfrom(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);
                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repository.ExternalPlatformExists(platform.ExternalID))
                    {
                        repository.CreatePlatform(platform);
                        repository.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not add platform to DB.");
                    throw;
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
