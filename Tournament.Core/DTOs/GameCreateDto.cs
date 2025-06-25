namespace Tournament.Core.DTOs
{
    public record GameCreateDto : GameDto
    {
        //public int Id { get; set; }
        public int TournamentDetailsId { get; set; }
    }
}
