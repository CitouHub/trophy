namespace Trophy.Domain
{
    public class RankingDTO
    {
        public string Player { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}