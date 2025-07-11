using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Schedule.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace AdviLaw.Application.Features.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleCommand : IRequest<Response<List<CreatedScheduleDTO>>>
    {
        [JsonPropertyName("schedulesToBeAdded")]
        public List<CreateScheduleDTO> SchedulesToBeAdded { get; set; } = new List<CreateScheduleDTO>();
        [JsonPropertyName("schedulesToBeRemoved")]
        public List<int> SchedulesToBeRemoved { get; set; } = new List<int>();
        public string? UserId { get; set; } 
    }
}
    