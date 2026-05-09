using Core_Web_API.Data;
using Microsoft.AspNetCore.Mvc;
using Core_Web_API.Models;

namespace Core_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var query = DataStore.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                query = query.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                query = query.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly.HasValue && activeOnly.Value)
                query = query.Where(r => r.IsActive);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoom(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound(new { message = $"Sala o ID {id} nie została znaleziona." });
            }
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<IEnumerable<Room>> GetRoomsByBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(rooms);
        }

        [HttpPost]
        public ActionResult<Room> CreateRoom([FromBody] Room room)
        {
            room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound(new { message = $"Sala o ID {id} nie została znaleziona." });
            }

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound(new { message = $"Sala o ID {id} nie została znaleziona." });
            }

            var hasReservations = DataStore.Reservations.Any(res => res.RoomId == id);
            if (hasReservations)
            {
                return Conflict(new { message = "Nie można usunąć sali, ponieważ posiada ona przypisane rezerwacje." });
            }

            DataStore.Rooms.Remove(room);
            return NoContent();
        }
    }
}