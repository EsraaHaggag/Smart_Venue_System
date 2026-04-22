using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Venue_System.Application.Features.Bookings.Command.CancelBooking;
using Venue_System.Application.Features.Bookings.Command.CreateBooking;
using Venue_System.Application.Features.Bookings.Query.GetMyBookings;

namespace Venue_System.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var command = new CancelBookingCommand(id);

            var response = await _mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings([FromQuery] GetMyBookingsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
