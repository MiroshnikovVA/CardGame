using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace CardGame.Cards.UI
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField]
        Image _art;

        [SerializeField]
        TextMeshProUGUI _attackText;
        [SerializeField]
        TextMeshProUGUI _manaText;
        [SerializeField]
        TextMeshProUGUI _hpText;

        RectTransform _cardPlace;
        const float _moveTime = 0.25f;

        Func<ICard, Tween> _onRemoveFromSetCallback = null;

        int _attack;
        int _mana;
        int _hp;

        public int Attack { 
            get => _attack; 
            set 
            { 
                _attack = value; 
                _attackText.text = value.ToString();
                ScaleTween(_attackText);
            } 
        }
        public int Mana {
            get => _mana; 
            set 
            { 
                _mana = value; 
                _manaText.text = value.ToString();
                ScaleTween(_manaText);
            } 
        }
        public int HP { 
            get => _hp; 
            set 
            { 
                _hp = value; 
                _hpText.text = value.ToString();
                ScaleTween(_hpText);
            } 
        }

        private void OnValidate()
        {
            if (!_art) Debug.LogError("_art is null", this);
            if (!_attackText) Debug.LogError("_attackText is null", this);
            if (!_manaText) Debug.LogError("_manaText is null", this);
            if (!_hpText) Debug.LogError("_hpText is null", this);
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
            return new WaitUntil(() => Initialized);
        }

        public bool Initialized => _cardPlace && _art.sprite;

        public void SetPlace(Transform transform, Func<ICard, Tween> onRemoveFromSetCallback)
        {
            if (!Initialized) Debug.LogError("Card not initialized", this);
            _cardPlace.transform.rotation = Quaternion.identity;
            _cardPlace.SetParent(transform);

            _onRemoveFromSetCallback?.Invoke(this);
            _onRemoveFromSetCallback = onRemoveFromSetCallback;
        }

        public void ToForeground()
        {
            var parent = transform.parent;
            transform.SetParent(parent.parent);
            transform.SetParent(parent);
        }

        public Tween Destroy()
        {
            ToForeground();

            return DOTween.Sequence()
                .Append(transform.DOScale(0.9f, 0.5f))
                .Append(transform.DOScale(1.2f, 0.2f))
                .Append(transform.DOScale(0f, 0.5f))
                .AppendCallback(() => GameObject.Destroy(_cardPlace.gameObject))
                .Append(_onRemoveFromSetCallback(this))
                .AppendCallback(() => Destroy(this.gameObject));
        }

        public Tween MoveToPlace()
        {
            return DOTween.Sequence().AppendCallback(() =>
            {
                transform.DOMove(_cardPlace.position, _moveTime);
                transform.DORotateQuaternion(_cardPlace.rotation, _moveTime);
            }).AppendInterval(_moveTime);
        }

        void ScaleTween(MonoBehaviour obj) 
        {
            DOTween.Sequence()
                .Append(obj.transform.DOScale(1.1f, 0.05f))
                .Append(obj.transform.DOScale(0.9f, 0.1f))
                .Append(obj.transform.DOScale(1.1f, 0.1f))
                .Append(obj.transform.DOScale(1.0f, 0.05f));
        }
    }
}
