using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
