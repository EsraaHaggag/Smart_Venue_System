using MediatR;
using Microsoft.AspNetCore.Mvc;
using Venue_System.Application.Features.Venues.Commnds.AddWorkingHours;
using Venue_System.Application.Features.Venues.Commnds.UpdateWorkingHours;
using Venue_System.Application.Features.Venues.Query.GetVenueWorkingHour;

namespace Venue_System.API.Controllers
{
    [ApiController]
    [Route("api/venues/{venueId}/working-hours")]

    public class WorkingHoursController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkingHoursController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkingHour(Guid venueId, [FromBody] AddWorkingHoursCommand command)
        {
            command = command with { VenueId = venueId };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkingHour(Guid venueId, [FromBody] UpdateWorkingHoursCommand command)
        {
            command = command with { VenueId = venueId };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetWorkingHours(Guid venueId)
        {
            var query = new GetVenueWorkingHourQuery
            {
                VenueId = venueId
            };

            var response = await _mediator.Send(query);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
