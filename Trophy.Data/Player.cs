using System;
using System.Collections.Generic;

namespace Trophy.Data
{
    public partial class Player
    {
        public Player()
        {
            PlayerResults = new HashSet<PlayerResult>();
        }

        public short Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PlayerResult> PlayerResults { get; set; }
    }
}
