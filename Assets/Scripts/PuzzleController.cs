using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

    PuzzleListElement ple;
    public Text title;
    public Text description;

    public GameObject image;
    public GameObject answers;

    public GameObject answerObject;

    public void Initialize(PuzzleListElement ple)
    {
        this.ple = ple;
        title.text = ple.puzzle.title;
        description.text = ple.puzzle.description;

        if (ple.puzzle.type == "find") {
            image.SetActive(true);
            answers.SetActive(false);
        }
        else if (ple.puzzle.type == "question")
        {
            image.SetActive(false);
            answers.SetActive(true);

            foreach(Transform child in answers.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < ple.puzzle.answers.Count; i++)
            {
                Instantiate(answerObject, answers.transform).GetComponent<AnswerController>().Initialize(ple.puzzle.answers[i], ple.puzzle.correctAnswer == i? true : false);
            }
        }
    }

}
