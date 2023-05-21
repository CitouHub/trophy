namespace Trophy.Domain
{
    public class PlayerResultDTO
    {
        public PlayerDTO? Player { get; set; }
        public short Score { get; set; }
        public bool Win { get; set; }
    }
}
