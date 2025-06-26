namespace Tournament.Core.DTOs
{
    public record TournamentDetailsDto
    {
        //public int Id { get; set; }

        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<GameDto>? Games { get; init; }
    }
}