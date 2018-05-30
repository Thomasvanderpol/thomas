﻿using System;
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
        IGameBL gamebl = BussinessLayerFactory.CreateGameBL();
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
        public void WhoWonBid(int CurrentGameID )
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
    }
}