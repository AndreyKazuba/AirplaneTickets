using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AirplaneTickets.Data;
using AirplaneTickets.Models;
using AirplaneTickets.Helpers;
using AirplaneTickets.Data.Entities;

#nullable disable
namespace AirplaneTickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AirplaneTicketsDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = "/login/index.html");
            builder.Services.AddAuthorization();

            WebApplication app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            ConfigurePrivateUrls(app);
            ConfigureDefaultUrl(app);
            app.UseStaticFiles();
            
            ConfigureWebApi(app);

            app.Run();
        }

        private static void ConfigureDefaultUrl(WebApplication app)
        {
            app.Use(async (httpContext, next) =>
            {
                string path = httpContext.Request.Path.Value;
                if (path == "/")
                {
                    httpContext.Response.Redirect("/flights/index.html");
                    return;
                }

                await next.Invoke();
            });
        }

        private static void ConfigurePrivateUrls(WebApplication app)
        {
            List<string> privateUrls = new List<string> { "/", "/buy/index.html", "/flights/index.html", "/flight-tickets/index.html", "/my-tickets/index.html" };
            app.Use(async (httpContext, next) =>
            {
                string? path = httpContext.Request.Path.Value;
                if (privateUrls.Any(url => url == path))
                {
                    if (httpContext.User.Identity?.IsAuthenticated != true)
                    {
                        httpContext.Response.Redirect("/login/index.html");
                        return;
                    }
                }

                await next.Invoke();
            });
        }

        private static void ConfigureWebApi(WebApplication app)
        {
            app.MapPost("/api/register", async ([FromBody] RegisterDto registerModel, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                if (string.IsNullOrWhiteSpace(registerModel.Login)
                 || string.IsNullOrWhiteSpace(registerModel.Password)
                 || string.IsNullOrWhiteSpace(registerModel.PasswordConfirmation))
                    return Results.BadRequest();


                if (registerModel.Password != registerModel.PasswordConfirmation)
                    return Results.Unauthorized();

                bool userExist = await dbContext.Users
                    .AnyAsync(user => user.Login == registerModel.Login);

                if (userExist)
                    return Results.Conflict();

                User user = dbContext.Users.Add(new User
                {
                    Login = registerModel.Login,
                    Password = PasswordHasher.GetHashString(registerModel.Password),
                }).Entity;

                await dbContext.SaveChangesAsync();
                List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Results.Ok();
            });

            app.MapPost("/api/login", async ([FromBody] LoginDto loginModel, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                User? user = await dbContext.Users.FirstOrDefaultAsync(currentUser => currentUser.Login == loginModel.Login);

                if (user is null)
                    return Results.Unauthorized();

                string outerPasswordHash = PasswordHasher.GetHashString(loginModel.Password);
                if (user.Password != outerPasswordHash)
                    return Results.Unauthorized();

                List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Results.Ok();
            });

            app.MapPost("/api/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Ok();
            });

            app.MapGet("/api/my-tickets", [Authorize] async (AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                Claim claim = httpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
                Guid userId = Guid.Parse(claim.Value);

                var tickets = await (from ticket in dbContext.Tickets
                                     join flight in dbContext.Flights
                                     on ticket.FlightId equals flight.Id
                                     where ticket.OwnerId == userId
                                     select new
                                     {
                                         FirstName = ticket.PassengerFirstName,
                                         LastName = ticket.PassengerLastName,
                                         ticket.Seat,
                                         flight.Name,
                                         flight.Date,
                                         flight.Company,
                                     }).ToListAsync();

                return Results.Ok(tickets);
            });

            app.MapGet("/api/flights", [Authorize] async (AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                List<Flight> flights = await dbContext.Flights
                    .ToListAsync();

                return Results.Ok(flights);
            });

            app.MapGet("/api/flights/{flightId}", [Authorize] async ([FromRoute] Guid flightId, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                Flight flight = await dbContext.Flights
                    .FirstOrDefaultAsync(flight => flight.Id == flightId);

                return Results.Ok(flight);
            });

            app.MapGet("/api/flight-tickets/{flightId}", [Authorize] async ([FromRoute] Guid flightId, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                List<Ticket> tickets = await dbContext.Tickets
                    .Where(ticket => ticket.FlightId == flightId)
                    .ToListAsync();

                return Results.Ok(tickets);
            });

            app.MapGet("/api/flight-ticket/{ticketId}", [Authorize] async ([FromRoute] Guid ticketId, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                var ticket = await (from currentTicket in dbContext.Tickets
                                    join flight in dbContext.Flights
                                    on currentTicket.FlightId equals flight.Id
                                    where currentTicket.Id == ticketId
                                    select new
                                    {
                                        flight.Date,
                                        flight.Name,
                                        flight.Plane,
                                        flight.Company,
                                        currentTicket.Id,
                                        currentTicket.Seat,
                                        currentTicket.RootPrice,
                                    }).FirstOrDefaultAsync();

                return Results.Ok(ticket);
            });

            app.MapPut("/api/flight-tickets", [Authorize] async ([FromBody] BuyTicketDto buyModel, AirplaneTicketsDbContext dbContext, HttpContext httpContext) =>
            {
                if (string.IsNullOrWhiteSpace(buyModel.FirstName)
                 || string.IsNullOrWhiteSpace(buyModel.LastName))
                    return Results.BadRequest();

                Claim claim = httpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
                Guid userId = Guid.Parse(claim.Value);

                Ticket ticket = await dbContext.Tickets
                    .FirstOrDefaultAsync(ticket => ticket.Id == buyModel.TicketId);

                ticket.OwnerId = userId;
                ticket.PassengerFirstName = buyModel.FirstName;
                ticket.PassengerLastName = buyModel.LastName;
                ticket.Sold = true;

                await dbContext.SaveChangesAsync();

                return Results.Ok();
            });
        }
    }
}
