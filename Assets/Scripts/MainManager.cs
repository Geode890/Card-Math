using System.Collections;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private CardManager cardManager;
    private GameUIManager uiManager;
    private MathManager mathManager;

    private float maxTime = 10;
    private float minTime = 3;
    private float timeDecrement = 0.2f;
    private float timeLeft;

    private float timeBetweenRounds = 1.5f;

    private Coroutine currentRound;

    private void Start()
    {
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<GameUIManager>();
        mathManager = GameObject.FindGameObjectWithTag("MathManager").GetComponent<MathManager>();

        uiManager.answerInput.onSubmit.AddListener(SubmitAnswer);

        currentRound = StartCoroutine(StartRound());
    }

    // ABSTRACTION
    private IEnumerator StartRound()
    {
        SetMathOption();

        cardManager.DealCards();

        timeLeft = maxTime;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            uiManager.SetTimerText(Mathf.CeilToInt(timeLeft));

            yield return null;
        }

        //Shows the incorrect answer symbol when timer expires
        uiManager.DisplaySymbol(false);

        yield return new WaitForSeconds(timeBetweenRounds);

        currentRound = StartCoroutine(StartRound());
    }

    private void SetMathOption()
    {
        uiManager.SetMathText(mathManager.SetMathOption());
    }

    private void SubmitAnswer(string answer)
    {
        StopCoroutine(currentRound);

        var correct = mathManager.EvaluateAnswer(answer);

        if (correct && maxTime > minTime)
        {
            maxTime -= timeDecrement;
        }

        uiManager.DisplaySymbol(correct);

        cardManager.DiscardCurrentCards();

        currentRound = StartCoroutine(StartRound());
    }
}
