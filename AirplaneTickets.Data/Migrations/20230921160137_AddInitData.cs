using AirplaneTickets.Data.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirplaneTickets.Data.Migrations
{
    public partial class AddInitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTickets();

            foreach (Flight flight in flights)
            {
                Guid flightId = Guid.NewGuid();
                migrationBuilder.InsertData(
                    table: "Flights",
                    columns: new[] { "Id", "Name", "Company", "Date", "Plane" },
                    values: new object[] { flightId, flight.Name, flight.Company, flight.Date, flight.Plane });

                foreach (Ticket ticket in tickets)
                {
                    migrationBuilder.InsertData(
                        table: "Tickets",
                        columns: new[] { "Id", "FlightId", "Seat", "RootPrice", "Sold" },
                        values: new object[] {  Guid.NewGuid(), flightId, ticket.Seat, ticket.RootPrice, ticket.Sold });
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }

        private readonly IEnumerable<Flight> flights = new List<Flight>
        {
            new Flight
            {
                Company = Companies.Alrosa,
                Date = new DateTime(2023, 12, 3, 14, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 3, 16, 30, 0),
                Name = "Минск - Санкт-Петербург",
                Plane = PlaneNames.Boeing737,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 3, 18, 0, 0),
                Name = "Минск - Стамбул",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 3, 14, 30, 0),
                Name = "Минск - Стамбул",
                Plane = PlaneNames.Boeing767,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 3, 0, 30, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 4, 5, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing777,
            },
            new Flight
            {
                Company = Companies.Alrosa,
                Date = new DateTime(2023, 12, 4, 10, 30, 0),
                Name = "Минск - Санкт-Петербург",
                Plane = PlaneNames.Boeing747,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 4, 12, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing767,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 4, 20, 0, 0),
                Name = "Минск - Санкт-Петербург",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 5, 03, 30, 0),
                Name = "Минск - Тбилиси",
                Plane = PlaneNames.Boeing717,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 5, 10, 0, 0),
                Name = "Минск - Дубай",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 5, 18, 20, 0),
                Name = "Минск - Ереван",
                Plane = PlaneNames.Boeing727,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 5, 22, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing737,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 6, 1, 45, 0),
                Name = "Минск - Тбилиси",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 6, 7, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 6, 13, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 6, 20, 0, 0),
                Name = "Минск - Тбилиси",
                Plane = PlaneNames.Boeing717,
            },
            new Flight
            {
                Company = Companies.Alrosa,
                Date = new DateTime(2023, 12, 7, 14, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing767,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 8, 10, 20, 0),
                Name = "Минск - Ташкент",
                Plane = PlaneNames.Boeing737,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 8, 21, 0, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Alrosa,
                Date = new DateTime(2023, 12, 9, 4, 10, 0),
                Name = "Минск - Санкт-Петербург",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 9, 14, 30, 0),
                Name = "Минск - Стамбул",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 10, 12, 30, 0),
                Name = "Минск - Стамбул",
                Plane = PlaneNames.Boeing727,
            },
            new Flight
            {
                Company = Companies.Aeroflot,
                Date = new DateTime(2023, 12, 11, 4, 15, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing747,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 12, 10, 30, 0),
                Name = "Минск - Москва",
                Plane = PlaneNames.Boeing707,
            },
            new Flight
            {
                Company = Companies.Belavia,
                Date = new DateTime(2023, 12, 12, 20, 0, 0),
                Name = "Минск - Санкт-Петербург",
                Plane = PlaneNames.Boeing757,
            },
        };

        private class Flight
        {
            public string Name { get; set; }
            public string Company { get; set; }
            public DateTime Date { get; set; }
            public string Plane { get; set; }
        }

        private void CreateTickets()
        {
            List<char> seatLetters = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F' };
            const int seatRows = 28;

            tickets = new List<Ticket>();

            for (int i = 1; i <= seatRows; i++)
            {
                foreach (char seatLetter in seatLetters)
                {
                    tickets.Add(new Ticket
                    {
                        Seat = $"{i}{seatLetter}",
                        Sold = false,
                        RootPrice = GetPrice(i),
                    });
                }
            }
        }

        private int GetPrice(int row)
        {
            if (row > 20)
                return 1000;
            if (row > 10)
                return 700;

            return 500;
        }

        private List<Ticket> tickets;

        private class Ticket
        {
            public string Seat { get; set; }
            public int RootPrice { get; set; }
            public bool Sold { get; set; }
        }
    }
}
