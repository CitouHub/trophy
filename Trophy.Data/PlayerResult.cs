using System;
using System.Collections.Generic;

namespace Trophy.Data
{
    public partial class PlayerResult
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int GameId { get; set; }
        public short PlayerId { get; set; }
        public short Score { get; set; }
        public bool Win { get; set; }

        public virtual Game Game { get; set; } = null!;
        public virtual Player Player { get; set; } = null!;
    }
}
