using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardPositionOne;
    [SerializeField] private Transform _cardPositionTwo;
    [SerializeField] private List<CardSO> _deck;
    private GameObject _cardOne, _cardTwo;
    private List<CardSO> _alreadySelectedCards = new List<CardSO>();
    private Dictionary<CardEffect, ICardEffect> _cardEffects = new Dictionary<CardEffect, ICardEffect>
    {
        {CardEffect.ReduceHPTo1 , new ReduceHPTo1 () },
        {CardEffect.IncreaseHP, new IncreaseHP () },
        {CardEffect.PowerupExtender, new PowerupExtender () },
        {CardEffect.TimeDilator, new TimeDilator () },
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        RandomizeNewCards();
    }

    public void RandomizeNewCards()
    {
        if (_cardOne != null) Destroy(_cardOne);
        if (_cardTwo != null) Destroy(_cardTwo);

        var randomaizedCards = new List<CardSO>();
        var availableCards = new List<CardSO>(_deck);

        availableCards.RemoveAll(card => _alreadySelectedCards.Contains(card));

        while (randomaizedCards.Count < 2 && availableCards.Count > 0)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            if (!_alreadySelectedCards.Contains(randomCard) &&
                !randomaizedCards.Contains(randomCard))
                randomaizedCards.Add(randomCard);
        }

        _cardOne = InstantiateCard(randomaizedCards[0], _cardPositionOne);
        _cardTwo = InstantiateCard(randomaizedCards[1], _cardPositionTwo);
    }

    public void FinishCardSelection()
    {
        RandomizeNewCards();
        UIManager.Instance.ScreenDimmingOff();
        UIManager.Instance.HideCardSelection();
        TimeManager.Instance.ContinueGame();
    }

    private GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGO = Instantiate(_cardPrefab, position.position, position.rotation, position);
        Card card = cardGO.GetComponent<Card>();
        card.Setup(cardSO, _cardEffects[cardSO.CardEffect]);
        return cardGO;
    }

    public void AddCardToAlreadySelected(CardSO card) => _alreadySelectedCards.Add(card);
}
