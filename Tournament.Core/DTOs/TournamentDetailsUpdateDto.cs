namespace Tournament.Core.DTOs
{
    public record TournamentDetailsUpdateDto : TournamentDetailsCreateDto
    {
        public int Id { get; init; }
        public IEnumerable<GameDto>? Games { get; init; }

    }
}