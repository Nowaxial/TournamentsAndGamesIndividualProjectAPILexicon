using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.DTOs
{
    public record GameDto
    {
        [Required(ErrorMessage = "Tournament title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }
        public DateTime Time { get; set; }
        //public int TournamentDetailsId { get; set; }
    }
}