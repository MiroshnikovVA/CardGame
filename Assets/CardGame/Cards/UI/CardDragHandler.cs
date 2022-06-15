using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardGame.Cards.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Card))]
    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        Camera _camera;
        Vector3 _offset;
        Vector3 _startPosition;
        CanvasGroup _canvasGroup;
        CardSet _dropPlace;
        Card _card;
        Transform _startParent;

        [SerializeField]
        Outline _illumination;

        private void OnValidate()
        {
            if (!_illumination) Debug.LogError("illumination is null", this);
        }

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _card = GetComponent<Card>(); ;
        }

        public void Drop(CardSet dropPlace)
        {
            _dropPlace = dropPlace;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _camera = eventData.enterEventCamera;
            _startPosition = transform.position;
            _offset = _startPosition - _camera.ScreenToWorldPoint(eventData.position);
            _canvasGroup.blocksRaycasts = false;

            transform.localRotation = Quaternion.identity;

            _startParent = transform.parent;
            var moveParent = _startParent.parent;
            transform.SetParent(moveParent);

            _illumination.enabled = true;

            _dropPlace = null;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            var newpos = _camera.ScreenToWorldPoint(eventData.position);
            transform.position = newpos + _offset;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _illumination.enabled = false;

            transform.SetParent(_startParent);

            if (_dropPlace)
            {
                _dropPlace.MoveCard(_card);
                enabled = false;
            }
            else
            {
                _card.MoveToPlace();
                _canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
