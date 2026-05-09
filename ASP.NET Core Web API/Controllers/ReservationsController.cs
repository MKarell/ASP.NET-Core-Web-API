using Core_Web_API.Data;
using Core_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Core_Web_API.Data;
using Core_Web_API.Models;

namespace Core_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        // GET: api/reservations
        // Obsługuje query string: ?date=2026-05-10&status=confirmed&roomId=2
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservations([FromQuery] DateOnly? date, [FromQuery] string status, [FromQuery] int? roomId)
        {
            var query = DataStore.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date == date.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }

        // GET: api/reservations/{id}
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound(new { message = $"Rezerwacja o ID {id} nie została znaleziona." });
            }
            return Ok(reservation);
        }

        // POST: api/reservations
        [HttpPost]
        public ActionResult<Reservation> CreateReservation([FromBody] Reservation reservation)
        {
            // Reguła: Sala musi istnieć
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null)
            {
                return NotFound(new { message = $"Sala o ID {reservation.RoomId} nie istnieje." });
            }

            // Reguła: Sala musi być aktywna
            if (!room.IsActive)
            {
                return BadRequest(new { message = "Wybrana sala jest obecnie nieaktywna." });
            }

            // Reguła: Brak kolizji czasowych tego samego dnia
            bool hasConflict = DataStore.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date == reservation.Date &&
                r.StartTime < reservation.EndTime && // Istniejąca rezerwacja zaczyna się przed końcem nowej
                r.EndTime > reservation.StartTime);  // Istniejąca rezerwacja kończy się po początku nowej

            if (hasConflict)
            {
                return Conflict(new { message = "W wybranym terminie sala jest już zarezerwowana." });
            }

            reservation.Id = DataStore.Reservations.Any() ? DataStore.Reservations.Max(r => r.Id) + 1 : 1;
            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // PUT: api/reservations/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound(new { message = $"Rezerwacja o ID {id} nie została znaleziona." });
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
            if (room == null)
            {
                return NotFound(new { message = $"Sala o ID {updatedReservation.RoomId} nie istnieje." });
            }

            if (!room.IsActive && room.Id != reservation.RoomId) // Pozwalamy na aktualizację istniejącej rezerwacji w dezaktywowanej sali, ale nie na jej przeniesienie do nieaktywnej
            {
                return BadRequest(new { message = "Wybrana nowa sala jest obecnie nieaktywna." });
            }

            bool hasConflict = DataStore.Reservations.Any(r =>
                r.Id != id && // Ignorujemy modyfikowaną rezerwację
                r.RoomId == updatedReservation.RoomId &&
                r.Date == updatedReservation.Date &&
                r.StartTime < updatedReservation.EndTime &&
                r.EndTime > updatedReservation.StartTime);

            if (hasConflict)
            {
                return Conflict(new { message = "Konflikt czasowy z inną rezerwacją w tej sali." });
            }

            reservation.RoomId = updatedReservation.RoomId;
            reservation.OrganizerName = updatedReservation.OrganizerName;
            reservation.Topic = updatedReservation.Topic;
            reservation.Date = updatedReservation.Date;
            reservation.StartTime = updatedReservation.StartTime;
            reservation.EndTime = updatedReservation.EndTime;
            reservation.Status = updatedReservation.Status;

            return Ok(reservation);
        }

        // DELETE: api/reservations/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound(new { message = $"Rezerwacja o ID {id} nie została znaleziona." });
            }

            DataStore.Reservations.Remove(reservation);
            return NoContent();
        }
    }
}