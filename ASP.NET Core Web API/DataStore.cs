using System;
using System.Collections.Generic;
using Core_Web_API.Models;

namespace Core_Web_API.Data
{
    public static class DataStore
    {
        public static List<Room> Rooms { get; set; } = new List<Room>
        {
            new Room { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "Sala Wykładowa", BuildingCode = "A", Floor = 0, Capacity = 100, HasProjector = true, IsActive = true },
            new Room { Id = 4, Name = "Pokój 10", BuildingCode = "C", Floor = 1, Capacity = 8, HasProjector = false, IsActive = true },
            new Room { Id = 5, Name = "Sala B2 (w remoncie)", BuildingCode = "B", Floor = 1, Capacity = 20, HasProjector = false, IsActive = false }
        };

        public static List<Reservation> Reservations { get; set; } = new List<Reservation>
        {
            new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Nowak", Topic = "Wstęp do C#", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Kowalska", Topic = "Warsztaty z HTTP", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0), Status = "confirmed" },
            new Reservation { Id = 3, RoomId = 1, OrganizerName = "Tomasz Kot", Topic = "ASP.NET Core", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(10, 30, 0), EndTime = new TimeSpan(14, 0, 0), Status = "planned" },
            new Reservation { Id = 4, RoomId = 3, OrganizerName = "Jan Nowak", Topic = "Wykład gościnny", Date = new DateOnly(2026, 5, 11), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 0, 0), Status = "cancelled" }
        };
    }
}