using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace CardGame.Cards.UI
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField]
        Image _art;

        RectTransform _cardPlace;
        const float _moveTime = 0.25f;

        Action<Card> onChangedThisPlaceCallback = null;

        private void OnValidate()
        {
            if (!_art) Debug.LogError("_art is null", this);
        }

        public void Init(RectTransform cardPlace)
        {
            _cardPlace = cardPlace;
        }

        public void SetArt(Sprite sprite)
        {
            _art.sprite = sprite;
        }

        public CustomYieldInstruction WaitingForInitialization()
        {
            return new WaitUntil(() => _cardPlace && _art.sprite);
        }

        public void SetPlace(Transform transform, Action<ICard> onChangedThisPlace)
        {
            if (!_cardPlace || !_art.sprite) Debug.LogError("Card not initialized", this);
            _cardPlace.transform.rotation = Quaternion.identity;
            _cardPlace.SetParent(transform);

            onChangedThisPlaceCallback?.Invoke(this);
            onChangedThisPlaceCallback = onChangedThisPlace;
        }

        public YieldInstruction MoveToPlace()
        {
            transform.DOMove(_cardPlace.position, _moveTime);
            return transform.DORotateQuaternion(_cardPlace.rotation, _moveTime).WaitForCompletion();
        }
    }
}
