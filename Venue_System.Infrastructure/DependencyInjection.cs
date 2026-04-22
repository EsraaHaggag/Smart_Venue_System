using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Venue_System.Application.Bases;
using Venue_System.Application.Behavior;
using Venue_System.Application.Interfaces.Repositories;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Infrastructure.Repositories;
using Venue_System.Infrastructure.Services;
using Venue_System.Infrastructure.Services.Email;
using Venue_System.Infrustrucure.DBContext;

namespace Venue_System.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            // MediatR - تأكد أنه يشير لـ Application Assembly
            var applicationAssembly = typeof(ApplicationAssemblyMarker).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(applicationAssembly);

                // الأفضل تسجيل الـ Behavior داخل إعدادات MediatR لضمان الترتيب
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // تصحيح الـ Validators: ابحث في Assembly الـ Application وليس الـ Infrastructure
            services.AddValidatorsFromAssembly(applicationAssembly);

            // تصحيح الـ AutoMapper ليعمل Scan لنفس الـ Assembly
            services.AddAutoMapper(applicationAssembly);

            services.AddTransient(typeof(IGenericRepositoryAsync<>),
                      typeof(GenericRepositoryAsync<>));
            services.AddTransient<IVenueRepository, VenueRepository>();

            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
            typeof(ApplicationAssemblyMarker).Assembly));

            //Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //// 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplictionDBContext>());

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
