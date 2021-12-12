using UnityEngine;

public class Card : MonoBehaviour
{
    private int suit;
    private int value;

    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ENCAPSULATION
    public int GetSuit()
    {
        return suit;
    }

    // ENCAPSULATION
    public void SetSuit(int suit)
    {
        if (suit < 0 || suit > 3)
        {
            Debug.Log("Invalid suit submitted: " + suit);
            this.suit = 0;
        }
        else
        {
            this.suit = suit;
        }
    }

    // ENCAPSULATION
    public int GetValue()
    {
        return value;
    }

    // ENCAPSULATION
    public void SetValue(int value)
    {
        if (value < 0 || value > 13)
        {
            Debug.Log("Invalid value submitted:" + value);
            this.value = 0;
        }
        else
        {
            this.value = value;
        }
    }

    public void SetCardFront(Sprite cardFront)
    {
        this.cardFront = cardFront;
    }

    public void SetCardBack(Sprite cardBack)
    {
        this.cardBack = cardBack;
        spriteRenderer.sprite = cardBack;
    }

    public void Flip()
    {
        //Play sounds here
        if (spriteRenderer.sprite == cardFront)
        {
            spriteRenderer.sprite = cardBack;
        }
        else
        {
            spriteRenderer.sprite = cardFront;
        }
    }
}
