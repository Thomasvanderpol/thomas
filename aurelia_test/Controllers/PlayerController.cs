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
        public void AddPlayers(string player)
        {
            bl.AddPlayer(player, 2.00);
        }
        
        [HttpGet("[action]")]
        public List<string> allplayers()
        {
            return bl.GetPlayers();

        }
    }
}