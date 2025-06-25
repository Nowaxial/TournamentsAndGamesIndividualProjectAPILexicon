using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.DTOs
{
    public record GameUpdateDto : GameDto
    {
        public int Id { get; set; }

    }
}