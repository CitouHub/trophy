namespace Trophy.Domain
{
    public class PlayerResultDTO
    {
        public short PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public short Score { get; set; }
        public bool Win { get; set; }
    }
}
