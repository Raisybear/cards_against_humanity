namespace CardsAgainstHumanity.Models
{
    public class Game
    {
        public string Id { get; set; }
        public List<Player> Players { get; set; } = new();
        public string CurrentBlackCard { get; set; }
        public List<PlayedCard> PlayedWhiteCards { get; set; } = new();
        public string JudgeId { get; set; }
        public bool IsStarted { get; set; } = false;
        public int Round { get; set; } = 1;
    }
}
