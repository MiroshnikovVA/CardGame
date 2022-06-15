using System.Collections;
using UnityEngine;
using CardGame.Cards;
using CardGame.Cards.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

        [SerializeField]
        private Button _changeCharacteristicsButton;
        
        [SerializeField]
        private Button _restartButton;

        void OnValidate()
        {
            if (!_cardFactory) Debug.LogError("cardFactory is null", this);
            if (!_hand) Debug.LogError("_hand is null", this);
            if (!_table) Debug.LogError("_table is null", this);
            if (!_changeCharacteristicsButton) Debug.LogError("_changeCharacteristicsButton is null", this);
            if (!_restartButton) Debug.LogError("_restartButton is null", this);
            if (!_loadScreen) Debug.LogError("_loadScreen is null", this);
        }

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        }

        void Start() => StartCoroutine(StartGame(_loadScreen, _cardFactory, _hand, _table));

        IEnumerator StartGame(GameObject loadScreen, ICardFactory cardFactory, ICardSet hand, ICardSet table)
        {
            int startHandCardCount = Random.Range(4,6+1);

            loadScreen.SetActive(true);
            yield return null;
            var cards = cardFactory.CreateCards(startHandCardCount);
            foreach (var card in cards) InitCharacteristics(card);
            foreach (var card in cards) yield return card.WaitingForInitialization();
            loadScreen.SetActive(false);

            foreach (var card in cards) yield return hand.MoveCard(card).WaitForCompletion();

            _changeCharacteristicsButton.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick()
        {
            StartCoroutine(ButtonClickCoroutine());
        }

        IEnumerator ButtonClickCoroutine()
        {
            _changeCharacteristicsButton.enabled = false;

            var card = _hand.GetNextCard();

            if (card != null)
            {
                SetRandomCharacteristics(card);

                card.ToForeground();

                if (card.CurrentCounterAnimation!=null && !card.CurrentCounterAnimation.IsComplete())
                    yield return card.CurrentCounterAnimation.WaitForCompletion();

                if (card.HP <= 0)
                    yield return card.Destroy().WaitForCompletion();
            }

            _changeCharacteristicsButton.enabled = true;
        }

        void InitCharacteristics(ICard card)
        {
            card.Mana = Random.Range(-2, 10);
            card.HP = Random.Range(1, 10);
            card.Attack = Random.Range(-2, 10);
        }

        void SetRandomCharacteristics(ICard card)
        {
            switch (Random.Range(0, 3))
            {
                case 0: card.Mana = Random.Range(-2, 10); break;
                case 1: card.HP = Random.Range(-2, 10); break;
                default: card.Attack = Random.Range(-2, 10); break;
            }
        }
    }
}
