using System;
using UnityEngine;
using DG.Tweening;

namespace CardGame.Cards.UI
{
    public class Card : MonoBehaviour, ICard
    {
        RectTransform _cardPlace;
        const float _moveTime = 0.25f;

        Action<Card> onChangedThisPlaceCallback = null;

        public void Init(RectTransform cardPlace)
        {
            _cardPlace = cardPlace;
        }

        public void SetPlace(Transform transform, Action<ICard> onChangedThisPlace)
        {
            if (!_cardPlace) Debug.LogError("Card not initialized", this);
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
