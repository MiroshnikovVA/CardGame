using System.Collections;
using UnityEngine;
using CardGame.Cards;
using CardGame.Cards.UI;

namespace CardGame.Game
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadScreen;
        [SerializeField]
        private CardFactory _cardFactory;
        [SerializeField]
        private CardSet _hand;
        [SerializeField]
        private CardSet _table;

        void OnValidate()
        {
            if (!_cardFactory) Debug.LogError("cardFactory is null", this);
            if (!_hand) Debug.LogError("_hand is null", this);
            if (!_table) Debug.LogError("_table is null", this);
        }

        void Start() => StartCoroutine(StatGame(_loadScreen, _cardFactory, _hand, _table));

        IEnumerator StatGame(GameObject loadScreen, ICardFactory cardFactory, ICardSet hand, ICardSet table)
        {
            const int startHandCardCount = 6;

            loadScreen.SetActive(true);
            yield return null;
            var cards = cardFactory.CreateCards(startHandCardCount);
            foreach (var card in cards)
            {
                yield return card.WaitingForInitialization();
            }
            loadScreen.SetActive(false);

            foreach (var card in cards)
            {
                yield return hand.MoveCard(card);
            }
        }
    }
}
