using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Cards.UI
{
    [RequireComponent(typeof(CardSet))]
    public class CardDropPlace : MonoBehaviour, IDropHandler
    {
        CardSet _cardSet;

        private void Awake()
        {
            _cardSet = GetComponent<CardSet>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var dragHandler = eventData.pointerDrag.GetComponent<CardDragHandler>();
            if (dragHandler) dragHandler.Drop(_cardSet);
        }
    }
}
