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

        //start van de game
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
        //haal de kaarten op voor de spelers
        [HttpGet("[action]")]
        public List<string> GetCards()
        {
            return icardBL.GetCards();
        }

        //game id ophalen
        [HttpGet("[action]")]
        public int GetCurrentGameID()
        {
            return igameBL.GetCurrentGameID();
        }

        //alle id's van de spelers ophalen
        [HttpGet("[action]")]
        public List<int> GetPlayersIDs()
        {
            return igameBL.GetPlayersIDs();
        }

        //keuze van een speler submitten. een bieding toevoegen
        [HttpGet("[action]/{CurrentGameID}/{Bid}/{playerIDs}")]
        public void SubmitChoice(int CurrentGameID, string Bid, int playerIDs)
        {
            igameBL.SubmitChoice(CurrentGameID, Bid, playerIDs);
        }

        //haal laatste bieding op van deze speler.
        [HttpGet("[action]/{Player}/{CurrentGameID}")]
        public List<string> GetBidPlayer(int Player, int CurrentGameID)
        {
            List<string> bids = new List<string>();
            bids.Add(igameBL.GetBidPlayer(Player, CurrentGameID));
            return bids;
        }

        //haal alle biedingen van de huidige game op
        [HttpGet("[action]/{CurrentGameID}")]
        public List<BidBO> GetBidsCurrentGame(int CurrentGameID)
        {
            return igameBL.GetBidsCurrentGame(CurrentGameID);
        }

        //bepaal de hoogste bieding van alle biedingen in de huidige game
        [HttpGet("[action]/{CurrentGameID}")]
        public BidBO GetHighestBidInGame(int CurrentGameID)
        {
            return igameBL.GetHighestBidInGame(CurrentGameID);
        }

        //haal alle keuzes op van een speler waaruit hij mag kiezen
        [HttpGet("[action]/{CurrentGameID}")]
        public List<string> GetChoicesPlayer(int CurrentGameID)
        {
            return igameBL.GetChoicesPlayer(CurrentGameID);
        }

        //update the game met daarin de troef / aas en wat voor soort spel er gespeeld wordt.
        [HttpGet("[action]/{Trump}/{Ace}/{GameTypeGame}/{CurrentGameID}")]
        public void UpdateGame(string Trump, string Ace, string GameTypeGame, int CurrentGameID)
        {
            igameBL.UpdateGame(Trump, Ace, GameTypeGame, CurrentGameID);
        }
        [HttpGet("[action]/{CurrentGameID}")]
        public List<string> GetTrumpAce(int CurrentGameID)
        {
            return igameBL.GetTrumpAce(CurrentGameID);
        }
        [HttpGet("[action]/{CurrentGameID}/{PlayerID}/{GameTypeGame}")]
        public void SetTeam(int CurrentGameID, int PlayerID, string GameTypeGame)
        {
            igameBL.SetTeam(CurrentGameID, PlayerID, GameTypeGame);
        }

        [HttpGet("[action]/{PlayerID}/{CurrentGameID}")]
        public void UpdateTeams(int PlayerID, int CurrentGameID)
        {
            igameBL.UpdateTeams(PlayerID, CurrentGameID);
        }

        [HttpGet("[action]/{CurrentGameID}")]
        public List<string> GetTeam1(int CurrentGameID)
        {
            List<string> Team1PlayerNames = new List<string>();
            List<int> Team1PlayerIDs = igameBL.GetTeam1(CurrentGameID);
            if (Team1PlayerIDs.Count != 0)
            {
                Team1PlayerNames = iplayerBL.GetPlayerNames(Team1PlayerIDs);
            }

            return Team1PlayerNames;
        }

        [HttpGet("[action]/{CurrentGameID}")]
        public List<string> GetTeam2(int CurrentGameID)
        {
            List<string> Team2PlayerNames = new List<string>();
            List<int> Team2PlayerIDs = igameBL.GetTeam2(CurrentGameID);
            if (Team2PlayerIDs.Count != 0)
            {
                Team2PlayerNames = iplayerBL.GetPlayerNames(Team2PlayerIDs);
            }

            return Team2PlayerNames;
        }

        [HttpGet("[action]/{cardID1}/{cardID2}/{cardID3}/{cardID4}")]
        public List<string> GetCardNames(int cardID1, int cardID2, int cardID3, int cardID4)
        {
            List<int> CardIDs = new List<int>();
            CardIDs.Add(cardID1);
            CardIDs.Add(cardID2);
            CardIDs.Add(cardID3);
            CardIDs.Add(cardID4);
            List<string> cardNames = icardBL.GetCardNames(CardIDs);

            return cardNames;
        }

    }
}