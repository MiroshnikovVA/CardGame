using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Cards.UI
{
    public class CardDropPlace : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            var dragHandler = eventData.pointerDrag.GetComponent<CardDragHandler>();
            if (dragHandler) dragHandler.Drop(this);
        }
    }
}
