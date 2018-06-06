using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class BidLevelBO
    {
        public string GameTypeName { get; set; }
        public int LevelBidsID { get; set; } 
        public BidLevelBO()
        {

        }
        public BidLevelBO(int levelBidsID, string gameTypeName)
        {
            GameTypeName = gameTypeName;
            LevelBidsID = levelBidsID;
        }
    }
}
