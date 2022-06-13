using UnityEngine;

namespace CardGame.Cards
{
    public interface ICardSet
    {
        YieldInstruction MoveCard(ICard card);
    }
}
