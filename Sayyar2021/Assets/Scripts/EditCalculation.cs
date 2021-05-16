using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ArabicSupport;


public class EditCalculation : MonoBehaviour
{
    public GameObject profileInfo;
    public GameObject EditableProfile;
    public GameObject Cal;

    public InputField answer;

    public Text Doperand1;
    public Text Doperand2;
    public TextMeshProUGUI ErrorMsg;

    private int operand1;
    private int operand2;
    public Text opreator;
    private int correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        generateNew();
    }

    public void generateNew()
    {
        operand1 = Random.Range(1, 100);
        operand2 = Random.Range(1, 100);
        Debug.Log("IN Start  operand1" + operand1);
        Debug.Log("IN Start  operand2" + operand2);

        if (operand1 < operand2)
        {
            int tmp = operand2;
            operand2 = operand1;
            operand1 = tmp;
        }

        Doperand1.text = operand1 + "";
        Doperand2.text = operand2 + "";

        int selectoperand = Random.Range(1, 4);
        Debug.Log("IN Start  selectoperand" + selectoperand);

        if (selectoperand == 1)
        {
            opreator.text = "+";
            correctAnswer = operand1 + operand2;
            Debug.Log("IN Start  correctAnswer" + correctAnswer);
        }
        else if (selectoperand == 2)
        {
            opreator.text = "X";
            correctAnswer = operand1 * operand2;
            Debug.Log("IN Start  correctAnswer" + correctAnswer);
        }
        else
        {
            opreator.text = "-";
            correctAnswer = operand1 - operand2;
            Debug.Log("IN Start  correctAnswer" + correctAnswer);
        }

    }

    public void OKButton()
    {
        Debug.Log("IN OKButton()  correctAnswer" + correctAnswer);
        if (checkAnswer())
        {
            Cal.SetActive(false);
            Debug.Log("IN OKButton()  true");
            EditableProfile.SetActive(true);
            generateNew();
            answer.text = "";
            ErrorMsg.text = "";
        }
        else
        {
            Debug.Log("IN OKButton()  flase");
            ErrorMsg.color = Color.red;
            ErrorMsg.text = ArabicFixer.Fix("الإجابة خاطئة حاول من جديد");
        }
    }

    public void EditButtton()
    {
        profileInfo.SetActive(false);
        Cal.SetActive(true);
    }

    public bool checkAnswer()
    {
        Debug.Log("IN checkAnswer()  answer.text" + answer.text);
        Debug.Log("IN checkAnswer()  correctAnswer" + correctAnswer);


        if (answer.text == "" + correctAnswer)
        {
            Debug.Log("IN checkAnswer()  true");

            return true;
        }
        else
        {
            Debug.Log("IN checkAnswer()  false");

            return false;
        }
    }

    public void closeCalPanel()
    {
        Cal.SetActive(false);
        generateNew();
        answer.text = "";
        ErrorMsg.text = "";
    }

}
