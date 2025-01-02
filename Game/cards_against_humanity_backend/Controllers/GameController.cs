using Microsoft.AspNetCore.Mvc;
using CardsAgainstHumanity.Services;
using CardsAgainstHumanity.Models;

namespace CardsAgainstHumanity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;
        private readonly CardService _cardService;

        public GameController(GameService gameService, CardService cardService)
        {
            _gameService = gameService;
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame()
        {
            var game = await _gameService.CreateGameAsync();
            return Ok(game);
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGame(string id, [FromBody] Player player)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound("Spiel nicht gefunden.");

            player.Id = Guid.NewGuid().ToString();
            game.Players.Add(player);
            await _gameService.UpdateGameAsync(game);

            return Ok(game);
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartGame(string id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound("Spiel nicht gefunden.");

            var blackCards = await _cardService.GetRandomCardsAsync(true, 1);
            game.CurrentBlackCard = blackCards.First().Id;
            game.JudgeId = game.Players.First().Id;
            game.IsStarted = true;

            foreach (var player in game.Players)
            {
                var whiteCards = await _cardService.GetRandomCardsAsync(false, 10);
                player.Hand.AddRange(whiteCards.Select(c => c.Id));
            }

            await _gameService.UpdateGameAsync(game);
            return Ok(game);
        }

        [HttpPost("{id}/play-card")]
        public async Task<IActionResult> PlayCard(string id, [FromBody] string cardId)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound("Spiel nicht gefunden.");

            var player = game.Players.FirstOrDefault(p => p.Hand.Contains(cardId));
            if (player == null) return BadRequest("Die Karte gehört keinem Spieler.");

            game.PlayedWhiteCards.Add(new PlayedCard { CardId = cardId, PlayerId = player.Id });
            player.Hand.Remove(cardId);

            await _gameService.UpdateGameAsync(game);
            return Ok(game);
        }

        [HttpPost("{id}/judge")]
        public async Task<IActionResult> JudgeCard(string id, [FromBody] string cardId)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound("Spiel nicht gefunden.");

            var playedCard = game.PlayedWhiteCards.FirstOrDefault(pc => pc.CardId == cardId);
            if (playedCard == null) return BadRequest("Die gewählte Karte wurde nicht gespielt.");

            var winningPlayer = game.Players.FirstOrDefault(p => p.Id == playedCard.PlayerId);
            if (winningPlayer == null) return BadRequest("Kein Spieler für die gewählte Karte gefunden.");

            winningPlayer.Score++;
            game.PlayedWhiteCards.Clear();
            game.Round++;
            game.JudgeId = game.Players[(game.Round - 1) % game.Players.Count].Id;

            await _gameService.UpdateGameAsync(game);
            return Ok(game);
        }
    }
}
