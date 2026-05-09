using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core_Web_API.Models
{
    public class Reservation : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Nazwa organizatora nie może być pusta.")]
        public string OrganizerName { get; set; }

        [Required(ErrorMessage = "Temat nie może być pusty.")]
        public string Topic { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "Czas zakończenia musi być późniejszy niż czas rozpoczęcia.",
                    new[] { nameof(EndTime) }
                );
            }
        }
    }
}