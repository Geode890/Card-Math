using UnityEngine;

public class MathManager : MonoBehaviour
{
    private MathOptions selectedOption;

    private enum MathOptions
    {
        Add = 0,
        Subtract = 1,
        Multiply = 2
    }

    public string SetMathOption()
    {
        var values = System.Enum.GetValues(typeof(MathOptions));
        var option = (MathOptions)values.GetValue(Random.Range(0, values.Length));
        selectedOption = option;

        switch ((int)option)
        {
            case 0:
                return "+";
            case 1:
                return "-";
            case 2:
                return "x";
            default:
                return "Error";
        }
    }

    public bool EvaluateAnswer(string answer)
    {
        var activeCards = GameObject.FindGameObjectsWithTag("ActiveCard");

        var card1 = activeCards[0].GetComponent<Card>();
        var card2 = activeCards[1].GetComponent<Card>();

        if (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out _))
        {
            return false;
        }

        switch ((int)selectedOption)
        {
            case 0:
                return card1.GetValue() + card2.GetValue() == System.Convert.ToInt32(answer);
            case 1:
                return card1.GetValue() - card2.GetValue() == System.Convert.ToInt32(answer);
            case 2:
                return card1.GetValue() * card2.GetValue() == System.Convert.ToInt32(answer);
            default:
                return false;
        }
    }
}
