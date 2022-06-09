using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Cards.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        Camera _camera;
        Vector3 _offset;
        Vector3 _startPosition;
        CanvasGroup _canvasGroup;
        CardDropPlace _dropPlace;
        Transform _startPlaceTransform;


        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>(); 
        }

        public void Drop(CardDropPlace dropPlace)
        {
            _dropPlace = dropPlace;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _camera = eventData.enterEventCamera;
            _startPosition = transform.position;
            _offset = _startPosition - _camera.ScreenToWorldPoint(eventData.position);
            _canvasGroup.blocksRaycasts = false;

            _startPlaceTransform = transform.parent;
            var movePanelTransform = transform.parent.parent;
            transform.SetParent(movePanelTransform);

            _dropPlace = null;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            var newpos = _camera.ScreenToWorldPoint(eventData.position);
            transform.position = newpos + _offset;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (_dropPlace)
            {
                transform.SetParent(_dropPlace.transform);
                enabled = false;
            }
            else
            {
                transform.SetParent(_startPlaceTransform);
            }

            _canvasGroup.blocksRaycasts = true;
        }
    }
}
