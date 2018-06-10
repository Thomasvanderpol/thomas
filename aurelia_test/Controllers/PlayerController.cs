using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessObject;
using Factory;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RikApplication.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        IPlayerBL bl = BussinessLayerFactory.CreatePlayerBL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]/{player}")]
        public List<string> AddPlayers(string player)
        {
            return bl.AddPlayer(player, 2.00);
        }

        [HttpGet("[action]")]
        public List<string> allplayers()
        {
            return bl.GetPlayers();

        }
        [HttpGet("[action]/{player1ID}/{player2ID}/{player3ID}/{player4ID}")]
        public List<string> GetPlayerNames(int player1ID, int player2ID, int player3ID, int player4ID)
        {
            List<int> playerids = new List<int>();
            playerids.Add(player1ID);
            playerids.Add(player2ID);
            playerids.Add(player3ID);
            playerids.Add(player4ID);


            return bl.GetPlayerNames(playerids);

        }

    }
}