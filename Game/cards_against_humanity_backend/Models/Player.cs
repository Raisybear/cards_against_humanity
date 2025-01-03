namespace CardsAgainstHumanity.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Hand { get; set; } = new();
        public int Score { get; set; } = 0;
    }
}
