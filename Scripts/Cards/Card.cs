using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _cardImageRenderer;
    [SerializeField] private TextMeshProUGUI _cardTextRenderer;
    [SerializeField] private GameObject _cardHighlighting;
    private CardSO _cardInfo;
    private ICardEffect _cardEffect;

    public void Setup(CardSO card, ICardEffect cardEffect)
    {
        _cardInfo = card;
        _cardImageRenderer.sprite = card.CardImage;
        _cardTextRenderer.text = card.CardText;
        _cardEffect = cardEffect;
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlayClickSFX();
        CardManager.Instance.AddCardToAlreadySelected(_cardInfo);
        _cardEffect.ExecuteEffect();
        CardManager.Instance.FinishCardSelection();
    }

    private void OnMouseEnter() => _cardHighlighting.SetActive(true);

    private void OnMouseExit() => _cardHighlighting.SetActive(false);
}
