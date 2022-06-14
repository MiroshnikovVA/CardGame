using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Cards.UI
{
    public class CardFactory : MonoBehaviour, ICardFactory
    {
        [SerializeField]
        Card _cardPrefab;
        [SerializeField]
        RectTransform _cardPlacePrefab;

        [SerializeField]
        ArtLoader _artLoader;

        [SerializeField]
        RectTransform _cardsParentTransform;

        void OnValidate()
        {
            if (!_cardPrefab) Debug.LogError("ñardPrefab is null", this);
            if (!_cardPlacePrefab) Debug.LogError("cardPlacePrefab is null", this);
            if (!_cardsParentTransform) Debug.LogError("cardsParentTransform is null", this);
            if (!_artLoader) Debug.LogError("artLoader is null", this);
        }

        private ICard CreateCard()
        {
            var card = Instantiate(_cardPrefab, _cardsParentTransform);
            var cardPlace = Instantiate(_cardPlacePrefab, _cardsParentTransform);
            card.Init(cardPlace);

            card.SetArt(null);
            _artLoader.LoadSprite(sprite => card.SetArt(sprite));

            return card;
        }

        public List<ICard> CreateCards(int count)
        {
            return Enumerable.Range(0, count).Select(i => CreateCard()).ToList();
        }
    }
}
