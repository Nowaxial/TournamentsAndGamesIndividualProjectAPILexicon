namespace Tournament.Core.DTOs
{
    public record TournamentDetailsDto
    {
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}