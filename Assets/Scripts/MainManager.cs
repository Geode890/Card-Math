using UnityEngine;

public class MainManager : MonoBehaviour
{
    private CardManager cardManager;
    private GameUIManager uiManager;

    private void Start()
    {
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<GameUIManager>();

        uiManager.answerInput.onSubmit.AddListener(SubmitAnswer);
    }

    private void StartRound()
    {
        //set math sign, etc
        cardManager.DealCards();
    }

    private void SubmitAnswer(string answer)
    {

    }
}
