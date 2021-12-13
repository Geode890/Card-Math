using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CardManager : MonoBehaviour
{
    private List<GameObject> cards = new List<GameObject>();
    private List<GameObject> usedCards = new List<GameObject>();

    private int maxCardValue = 13;
    private float moveSpeed = 4.5f;

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

    private void Awake()
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

        ShuffleCards(cards);
    }

    private void ResetCards()
    {
        foreach (GameObject c in usedCards)
        {
            cards.Add(c);
            c.GetComponent<Card>().Flip();
            c.transform.position = cardPrefab.transform.position;
            c.SetActive(true);
        }

        ShuffleCards(cards);
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
        var card1 = cards[0];
        var card2 = cards[1];

        card1.tag = "ActiveCard";
        card2.tag = "ActiveCard";

        StartCoroutine(MoveCard(card1, cardSpot1));
        StartCoroutine(MoveCard(card2, cardSpot2));
    }

    public void DiscardCurrentCards()
    {
        var activeCards = GameObject.FindGameObjectsWithTag("ActiveCard");

        foreach (var c in activeCards)
        {
            usedCards.Add(c);
            cards.Remove(c);

            c.tag = "Untagged";
            c.SetActive(false);
        }

        if (cards.Count <= 0)
        {
            ResetCards();
        }
    }

    private IEnumerator MoveCard(GameObject card, GameObject pos)
    {
        while (Vector3.Distance(card.transform.position, pos.transform.position) > 0.1f)
        {
            card.transform.position = Vector3.Lerp(card.transform.position, pos.transform.position, Time.deltaTime * moveSpeed);

            yield return null;
        }

        card.GetComponent<Card>().Flip();
    }
}
