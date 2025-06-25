using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public DateTime Time { get; set; }

        // Främmande nyckel till TournamentDetails
        public int TournamentDetailsId { get; set; }
    }
}