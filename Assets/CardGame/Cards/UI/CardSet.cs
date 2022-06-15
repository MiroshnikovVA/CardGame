using DG.Tweening;
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

        int _currentIndex;

        const float _animationTime = 0.3f;

        private void Awake()
        {
            _cards = new List<ICard>();
            _rectTransform = transform as RectTransform;
        }

        public Tween MoveCards(List<ICard> cards)
        {
            _cards.AddRange(cards);
            foreach (var card in cards)
            {
                card.SetPlace(transform, OnRemoveCard);
            }

            return PlayAnimation();
        }

        public Tween MoveCard(ICard card)
        {
            _cards.Add(card);
            card.SetPlace(transform, OnRemoveCard);

            return PlayAnimation();
        }

        private Tween OnRemoveCard(ICard card)
        {
            if (_currentIndex > _cards.IndexOf(card)) _currentIndex--;
            _cards.Remove(card);
            return PlayAnimation();
        }

        private Tween PlayAnimation()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            var s = DOTween.Sequence().AppendInterval(0).AppendCallback(() =>
            {
                foreach (var card in _cards)
                {
                    card.MoveToPlace();
                }
            }).AppendInterval(_animationTime);

            return s;
        }

        public ICard GetNextCard()
        {
            if (_cards.Count == 0) return null;
            if (_currentIndex >= _cards.Count) _currentIndex = 0;
            return _cards[_currentIndex++];
        }
    }
}
