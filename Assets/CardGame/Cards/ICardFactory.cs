using System.Collections.Generic;

namespace CardGame.Cards
{
    public interface ICardFactory
    {
        ICard CreateCard();
        IReadOnlyList<ICard> CreateCards(int count);
    }
}
