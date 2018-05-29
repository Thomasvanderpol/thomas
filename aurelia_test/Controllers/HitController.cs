using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factory;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RikApplication.Controllers
{
    [Route("api/[controller]")]
    public class HitController : Controller
    {
        IHitBL hitbl = BussinessLayerFactory.CreateHitBL();
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("[action]")]
        public void Delay()
        {
            System.Threading.Thread.Sleep(1500);
        }
        
        [HttpGet("[action]/{CardPlayer1}/{CardPlayer2}/{CardPlayer3}/{CardPlayer4}")]
        public void WhoWonBid(string CardPlayer1, string CardPlayer2, string CardPlayer3, string CardPlayer4)
        {
            //methode in busines logic aanroepen met lijst van de kaarten erin.
        }
        [HttpGet("[action]/{CurrentGameID}/{PlayerID}/{Card}")]
        public void PlayedCard(int CurrentGameID, int PlayerID, string Card)
        {
            hitbl.AddCard(CurrentGameID, PlayerID, Card);
        }
    }
}