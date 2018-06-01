using BusinessObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICardDA
    {
        List<CardBO> GetCards();
        string getCardNameByID(int card);
    }
}
