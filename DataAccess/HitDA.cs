using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class HitDA: IHitDA
    {
        private IDb conn;
        public HitDA(IDb conn)
        {
            this.conn = conn;
        }
    }
}
