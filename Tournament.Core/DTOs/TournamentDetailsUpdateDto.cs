namespace Tournament.Core.DTOs
{
    public record TournamentDetailsUpdateDto : TournamentDetailsCreateDto
    {
        public int Id { get; init; }
    }
}