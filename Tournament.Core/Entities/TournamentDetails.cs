namespace Tournament.Core.Entities
{
    public class TournamentDetails
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }

        // En turnering kan ha många matcher
        public ICollection<Game>? Game { get; set; }
    }
}