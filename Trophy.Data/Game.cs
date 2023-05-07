using System;
using System.Collections.Generic;

namespace Trophy.Data
{
    public partial class Game
    {
        public Game()
        {
            PlayerResults = new HashSet<PlayerResult>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public DateTime MatchDate { get; set; }
        public string Location { get; set; } = null!;

        public virtual ICollection<PlayerResult> PlayerResults { get; set; }
    }
}
