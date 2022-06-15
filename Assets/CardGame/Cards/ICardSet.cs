using DG.Tweening;

namespace CardGame.Cards
{
    public interface ICardSet
    {
        Tween MoveCard(ICard card);
        ICard GetNextCard();
    }
}
