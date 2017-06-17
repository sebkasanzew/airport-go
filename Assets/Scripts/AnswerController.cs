using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour {

    public Text answer;
    public bool isCorrect;
    private PuzzleController pz;

    public void Initialize(string answer, bool isCorrect, PuzzleController pz)
    {
        this.answer.text = answer;
        this.isCorrect = isCorrect;
        this.pz = pz;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            pz.AnsweredCorrectly();
        }
        else
        {
            pz.AnsweredIncorrectly();
        }
    }
}
