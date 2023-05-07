namespace Trophy.Domain
{
    public class GameDTO
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<PlayerResultDTO> PlayerResults { get; set; } = new List<PlayerResultDTO>();
    }
}