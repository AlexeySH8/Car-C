using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

public class CardSO : ScriptableObject
{
    public Sprite CardImage;
    public string CardText;
    public CardEffect CardEffect;
}

public enum CardEffect
{
    ReduceHPTo1,
    IncreaseHP,
    PowerupExtender,
    TimeDilator
}
