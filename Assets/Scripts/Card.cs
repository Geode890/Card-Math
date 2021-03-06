using UnityEngine;

// INHERITANCE
public class Card : GamePiece
{
    private int suit;
    private int value;

    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    private SpriteRenderer spriteRenderer;

    private AudioSource flipSound;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flipSound = GetComponent<AudioSource>();
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

    // POLYMORPHISM
    public void Flip()
    {
        if (spriteRenderer.sprite == cardFront)
        {
            spriteRenderer.sprite = cardBack;
        }
        else
        {
            spriteRenderer.sprite = cardFront;
        }
        
        flipSound.Play();
    }

    // POLYMORPHISM
    public void Flip(bool playSound)
    {
        if (spriteRenderer.sprite == cardFront)
        {
            spriteRenderer.sprite = cardBack;
        }
        else
        {
            spriteRenderer.sprite = cardFront;
        }

        if (playSound)
        {
            flipSound.Play();
        }
    }
}
