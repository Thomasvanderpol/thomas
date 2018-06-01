using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICardBL
    {
        List<string> GetCards();
        List<string> GetCardNames(List<int> cardIDs);
    }
}
