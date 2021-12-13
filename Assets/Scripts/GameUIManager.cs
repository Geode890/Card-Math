using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TMP_InputField answerInput;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text mathText;
    [SerializeField] private Image correctScreen;
    [SerializeField] private Image incorrectScreen;

    private float resultDisplayTime = 1.0f;

    public void SetTimerText(int timeLeft)
    {
        timerText.text = "Time: " + timeLeft;
    }

    public void SetMathText(string symbol)
    {
        mathText.text = symbol;
    }

    public void DisplaySymbol(bool correct)
    {
        if (correct)
        {
            StartCoroutine(ShowScreen(correctScreen.gameObject));
        }
        else
        {
            StartCoroutine(ShowScreen(incorrectScreen.gameObject));
        }

        answerInput.text = "";
    }

    private IEnumerator ShowScreen(GameObject screen)
    {
        screen.SetActive(true);

        yield return new WaitForSeconds(resultDisplayTime);

        screen.SetActive(false);
    }
}
