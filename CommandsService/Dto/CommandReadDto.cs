﻿using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dto
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string ComandLine { get; set; }
        public int PlatformId { get; set; }
    }
}
