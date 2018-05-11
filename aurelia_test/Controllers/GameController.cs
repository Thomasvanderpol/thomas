using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Factory;
using Interfaces;
using BusinessObject;

namespace RikApplication.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        IGameBL igameBL = BussinessLayerFactory.CreateGameBL();
        IPlayerBL iplayerBL = BussinessLayerFactory.CreatePlayerBL();
        ICardBL icardBL = BussinessLayerFactory.CreateCardBL();
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("[action]/{Player1}/{Player2}/{Player3}/{Player4}")]
        public int BeginGame(string Player1, string Player2, string Player3, string Player4)
        {

            List<string> playersInGame = new List<string>();

            playersInGame.Add(Player1);
            playersInGame.Add(Player2);
            playersInGame.Add(Player3);
            playersInGame.Add(Player4);

            int gelukt = 0;
            try
            {
                List<int> playersInGameObjecten = iplayerBL.GetFourPlayers(playersInGame);

                igameBL.BeginGame(playersInGameObjecten);
                gelukt = 1;
            }
            catch
            {
                gelukt = 0;
            }
            return gelukt;
        }
        [HttpGet("[action]")]
        public List<string> GetCards()
        {
            return icardBL.GetCards();
        }
        [HttpGet("[action]")]
        public int GetCurrentGameID()
        {
            return igameBL.GetCurrentGameID();
        }

        [HttpGet("[action]")]
        public List<int> GetPlayersIDs()
        {
            return igameBL.GetPlayersIDs();
        }

        [HttpGet("[action]/{CurrentGameID}/{Bid}/{playerIDs}")]
        public void SubmitChoice(int CurrentGameID, string Bid, int playerIDs)
        {
            igameBL.SubmitChoice(CurrentGameID, Bid, playerIDs);
        }
    }
}