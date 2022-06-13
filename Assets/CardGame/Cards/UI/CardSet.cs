using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Cards.UI
{
    [RequireComponent(typeof(LayoutGroup))]
    public class CardSet : MonoBehaviour, ICardSet
    {
        List<ICard> _cards;
        RectTransform _rectTransform;

        private void Awake()
        {
            _cards = new List<ICard>();
            _rectTransform = transform as RectTransform;
        }

        public YieldInstruction MoveCards(List<ICard> cards)
        {
            _cards.AddRange(cards);
            foreach (var card in cards)
            {
                card.SetPlace(transform, OnRemoveCard);
            }

            return PlayAnimation();
        }

        public YieldInstruction MoveCard(ICard card)
        {
            _cards.Add(card);
            card.SetPlace(transform, OnRemoveCard);

            return PlayAnimation();
        }

        private void OnRemoveCard(ICard card)
        {
            _cards.Remove(card);
            PlayAnimation();
        }

        private YieldInstruction PlayAnimation()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);

            YieldInstruction onlyOneAnimation = null;

            foreach (var card in _cards)
            {
                onlyOneAnimation = card.MoveToPlace();
            }

            return onlyOneAnimation;
        }
    }
}
