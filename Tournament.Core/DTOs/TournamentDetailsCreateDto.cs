using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.DTOs
{
    public record TournamentDetailsCreateDto
    {
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Tournament title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }
    }
}
