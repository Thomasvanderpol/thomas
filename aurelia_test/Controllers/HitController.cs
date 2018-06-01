using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject;
using Factory;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RikApplication.Controllers
{
    [Route("api/[controller]")]
    public class HitController : Controller
    {
        IHitBL hitbl = BussinessLayerFactory.CreateHitBL();
        IGameBL gamebl = BussinessLayerFactory.CreateGameBL();
        IPlayerBL playerbl = BussinessLayerFactory.CreatePlayerBL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public void Delay()
        {
            System.Threading.Thread.Sleep(1500);
        }

        [HttpGet("[action]/{CurrentGameID}")]
        public void WhoWonBid(int CurrentGameID)
        {
            //method to get Trump in current game
            List<string> TrumpAndAce = gamebl.GetTrumpAce(CurrentGameID);
            //methode in busines logic aanroepen met lijst van de kaarten erin.
            hitbl.WhoWonBid(CurrentGameID, TrumpAndAce[0]);
        }
        [HttpGet("[action]/{CurrentGameID}/{PlayerID}/{Card}")]
        public void PlayedCard(int CurrentGameID, int PlayerID, string Card)
        {
            hitbl.AddCard(CurrentGameID, PlayerID, Card);
        }

        [HttpGet("[action]/{CurrentGameID}")]
        public List<int> GetAllHits(int CurrentGameID)
        {
            List<int> PlayersInGame = gamebl.GetPlayersIDs();
            List<int> AllHitsPlayers = hitbl.GetAllHits(CurrentGameID, PlayersInGame);
            return AllHitsPlayers;
        }

        [HttpGet("[action]/{CurrentGameID}")]
        public List<CardPlayerBO> ShowLastHit(int CurrentGameID)
        {
            List<CardPlayerBO> LastHit = hitbl.ShowLastHit(CurrentGameID);
            return LastHit;
        }
        [HttpGet("[action]/{Player}")]
        public List<AllHitsBO> GetAllHitsByPlayer(string player)
        {
            int PlayerID = playerbl.GetPlayerID(player);
            List<AllHitsBO> LastHit = hitbl.GetAllHitsByPlayer(PlayerID);
            return LastHit;
        }
        
    }
}