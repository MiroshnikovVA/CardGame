using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Cards
{
    public interface ICardFactory
    {
        List<ICard> CreateCards(int count);

        ICard CreateCard();
    }
}
