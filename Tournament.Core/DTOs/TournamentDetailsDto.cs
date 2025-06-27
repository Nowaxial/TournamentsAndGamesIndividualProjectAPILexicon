namespace Tournament.Core.DTOs
{
    public record TournamentDetailsDto
    {
        //public int Id { get; set; }

        public string? Title { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public IEnumerable<GameDto>? Games { get; init; }
    }
}