using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour {

    public Text answer;
    public bool isCorrect;

    public void Initialize(string answer, bool isCorrect)
    {
        this.answer.text = answer;
        this.isCorrect = isCorrect;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            //correct
        }
        else
        {
            //not correct
        }
    }
}
