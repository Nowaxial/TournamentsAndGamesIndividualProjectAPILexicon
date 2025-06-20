﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Time { get; set; }

        // Främmande nyckel till TournamentDetails
        public int TournamentId { get; set; }

        // Navigationsproperties
        public TournamentDetails? Tournament { get; set; }
    }
}
