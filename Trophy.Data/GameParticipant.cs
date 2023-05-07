using System;
using System.Collections.Generic;

namespace Trophy.Data
{
    public partial class GameParticipant
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; } = null!;
        public byte Score { get; set; }
        public bool Win { get; set; }
    }
}
