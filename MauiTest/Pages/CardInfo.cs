using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTest
{
    internal class CardInfo
    {
        private string cardName;
        private string cardUrl;

        public string CardName { get { return cardName; } }
        public string CardUrl { get { return cardUrl; } }


        public CardInfo(string CardName,string CardUrl)
        {
            cardName = CardName;
            cardUrl = CardUrl;
        }

    }
}
