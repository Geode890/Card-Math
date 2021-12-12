using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardManager : MonoBehaviour
{
    private List<GameObject> cards = new List<GameObject>();
    private List<GameObject> usedCards = new List<GameObject>();

    private int maxCardValue = 13;
    private float moveSpeed = 0.5f;

    [SerializeField] SpriteAtlas cardSprites;
    [SerializeField] Sprite cardBack;
    [SerializeField] GameObject cardPrefab;

    [SerializeField] GameObject cardSpot1;
    [SerializeField] GameObject cardSpot2;

    private enum Suits
    {
        Spade = 0,
        Heart = 1,
        Diamond = 2,
        Club = 3
    }

    private void Start()
    {
        foreach (Suits suit in System.Enum.GetValues(typeof(Suits)))
        {
            for (int i = 1; i <= maxCardValue; i++)
            {
                Sprite cardFront = cardSprites.GetSprite("playingCards_" + GetCardCode(suit, i));

                GameObject card = Instantiate(cardPrefab);
                var cardClass = card.GetComponent<Card>();
                cardClass.SetSuit((int)suit);
                cardClass.SetValue(i);
                cardClass.SetCardFront(cardFront);
                cardClass.SetCardBack(cardBack);

                cards.Add(card);
            }
        }
    }

    private void ResetCards()
    {
        foreach (GameObject c in usedCards)
        {
            cards.Add(c);
            ShuffleCards(cards);
        }

        usedCards = new List<GameObject>();
    }

    private void ShuffleCards(List<GameObject> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private int GetCardCode(Suits suit, int value)
    {
        return ((int)suit * 13) + value;
    }

    public void DealCards()
    {
        Debug.Log(cards.Count);

        var card1 = cards[0];
        var card2 = cards[1];

        StartCoroutine(MoveCard(card1, cardSpot1));
        StartCoroutine(MoveCard(card2, cardSpot2));

        card1.GetComponent<Card>().Flip();
        card2.GetComponent<Card>().Flip();
    }

    private IEnumerator MoveCard(GameObject card, GameObject pos)
    {
        while (card.transform.position != pos.transform.position)
        {
            card.transform.position = Vector3.Lerp(card.transform.position, pos.transform.position, Time.deltaTime * moveSpeed);

            yield return null;
        }
    }
}
