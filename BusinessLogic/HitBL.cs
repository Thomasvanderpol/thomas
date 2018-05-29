using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class HitBL: IHitBL
    {
        private IHitDA conn;
        public HitBL(IHitDA conn)
        {
            this.conn = conn;
        }
    }
}
