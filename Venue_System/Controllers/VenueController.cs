using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Venue_System.Application.Features.Venues.Commnds.Activate;
using Venue_System.Application.Features.Venues.Commnds.AddRule;
using Venue_System.Application.Features.Venues.Commnds.ChangePrice;
using Venue_System.Application.Features.Venues.Commnds.CreateVenue;
using Venue_System.Application.Features.Venues.Commnds.Deactivate;
using Venue_System.Application.Features.Venues.Commnds.RemoveRule;
using Venue_System.Application.Features.Venues.Commnds.RemoveVenue;
using Venue_System.Application.Features.Venues.Commnds.SetCancellationPolicy;
using Venue_System.Application.Features.Venues.Commnds.UpdateRule;
using Venue_System.Application.Features.Venues.Commnds.UpdateVenue;
using Venue_System.Application.Features.Venues.Query.GetAllVenues;
using Venue_System.Application.Features.Venues.Query.GetMyVenues;
using Venue_System.Application.Features.Venues.Query.GetRulesOfVenue;
using Venue_System.Application.Features.Venues.Query.GetVenueById;
using Venue_System.Application.Features.Venues.Query.GetVenuesByCondition;

namespace Venue_System.API.Controllers
{

    [ApiController]
    [Route("api/venues")]
    public class VenuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VenuesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVenueCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVenueCommand command)
        {
            command = command with { Id = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPost("{id}/rules")]
        public async Task<IActionResult> AddRule(Guid id, [FromBody] AddRuleCommand command)
        {
            command = command with { VenueId = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPatch("{venueId}/rules/{ruleId}")]
        public async Task<IActionResult> UpdateRule(Guid venueId, Guid ruleId, [FromBody] UpdateRuleCommand command)
        {
            command = command with
            {
                VenueId = venueId,
                RuleId = ruleId
            };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpDelete("{venueId}/rules/{ruleId}")]
        public async Task<IActionResult> RemoveRule(Guid venueId, Guid ruleId)
        {
            var command = new RemoveRuleCommand
            {
                VenueId = venueId,
                RuleId = ruleId
            };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPatch("{id}/price")]
        public async Task<IActionResult> ChangePrice(Guid id, [FromBody] ChangePriceCommand command)
        {
            command = command with { VenueId = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner,Admin")]
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateCommand { VenueId = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner,Admin")]
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var command = new ActivateCommand { VenueId = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [Authorize(Roles = "VenueOwner,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveVenue(Guid id)
        {
            var command = new RemoveVenueCommand { VenueId = id };

            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveVenues()
        {
            var response = await _mediator.Send(new GetAllVenuesQuery());
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenueById(Guid id)
        {
            var query = new GetVenueByIdQuery { VenueId = id };

            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{venueId}/rules")]
        public async Task<IActionResult> GetVenueRules(Guid venueId)
        {
            var query = new GetRulesOfVenueQuery
            {
                VenueId = venueId
            };

            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);

        }
        [Authorize(Roles = "VenueOwner")]
        [HttpPost("{id}/cancellation-policy")]
        public async Task<IActionResult> SetPolicy(Guid id, SetCancellationPolicyCommand command)
        {
            command = command with { VenueId = id };

            var response = await _mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetVenues([FromQuery] GetVenuesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Roles = "VenueOwner")]
        [HttpGet("my-venues")]
        public async Task<IActionResult> GetMyVenues()
        {
            var result = await _mediator.Send(new GetMyVenuesQuery());
            return Ok(result);
        }
    }

}
