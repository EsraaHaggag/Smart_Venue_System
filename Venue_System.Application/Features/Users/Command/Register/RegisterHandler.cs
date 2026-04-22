using MediatR;
using Venue_System.Application.Bases;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Domain.Entities;
using Venue_System.Domain.Enums;

namespace Venue_System.Application.Features.Users.Command.Register
{
    public class RegisterHandler : ResponseHandler,
    IRequestHandler<RegisterCommand, Response<Guid>>
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public RegisterHandler(
            IIdentityService identityService,
            IApplicationDbContext context,
            IEmailService emailService)
        {
            _identityService = identityService;
            _context = context;
            _emailService = emailService;
        }

        public async Task<Response<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // 1. التحقق المبدئي
            if (await _identityService.ExistsByEmailAsync(request.Email))
            {
                return BadRequest<Guid>("This email is already registered.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await _identityService.CreateUserAsync(
                    request.Email,
                    request.Password,
                    request.FirstName,
                    request.LastName,
                    request.PhoneNumber,
                    request.Username);

                if (!result.Success)
                {
                    return UnprocessableEntity<Guid>(result.Error);
                }


                await _identityService.AddToRoleAsync(result.UserId, request.Role.ToString());


                if (request.Role == UserRule.Customer)
                {
                    await _context.Customers.AddAsync(new Customer(result.UserId), cancellationToken);
                }
                else if (request.Role == UserRule.VenueOwner)
                {
                    await _context.VenueOwners.AddAsync(new VenueOwner(result.UserId), cancellationToken);
                }


                await _context.SaveChangesAsync(cancellationToken);


                await transaction.CommitAsync(cancellationToken);

                await SendConfirmationEmail(result.UserId, request.Email);

                return Success(result.UserId, "Registered successfully. Please verify your email.");
            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync(cancellationToken);


                return BadRequest<Guid>("An error occurred during registration. Please try again.");
            }
        }

        private async Task SendConfirmationEmail(Guid userId, string email)
        {
            var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId);
            var encodedToken = Uri.EscapeDataString(token);
            var confirmationLink = $"https://localhost:7147/api/auth/confirm-email?userId={userId}&token={encodedToken}";

            var emailContent = $@"
        <p>Welcome! Please verify your account by clicking the button below:</p>
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{confirmationLink}' class='btn'>Verify My Account</a>
        </div>";

            await _emailService.SendAsync(email, "Confirm Registration", emailContent, "email-template");
        }
    }
}
