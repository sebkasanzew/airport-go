using OMobile.EstimoteUnity;
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

    public GameObject successView;

    public void Initialize(PuzzleListElement ple)
    {
        this.ple = ple;
        title.text = ple.puzzle.title;
        description.text = ple.puzzle.description;

        if (ple.puzzle.type == "find") {
            image.SetActive(true);
            answers.SetActive(false);
            ApplicationManager.Instance.StartScanning();
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
                Instantiate(answerObject, answers.transform).GetComponent<AnswerController>().Initialize(ple.puzzle.answers[i], ple.puzzle.correctAnswer == i? true : false, this);
            }
        }

        ApplicationManager.Instance.AddListenerToBackButton(ClosePuzzleController);
    }

    void Update()
    {
        if (ple.puzzle.type == "find")
        {
            if (ApplicationManager.Instance.beacons != null)
            {
                foreach (EstimoteUnityBeacon b in ApplicationManager.Instance.beacons)
                {
                    if (b.Major == ple.puzzle.beaconID && b.Accuracy < 1f)
                    {
                        AnsweredCorrectly();
                    }
                }
            }
        }
    }

    public void AnsweredCorrectly()
    {
        ApplicationManager.Instance.AddPoints(ple.currentPoints);
        ApplicationManager.Instance.SolvePuzzle(ple.title.text, ple.currentPoints);
        ple.IsSolved();
        gameObject.SetActive(false);
        Debug.Log("points: " + ApplicationManager.Instance.points);
        successView.SetActive(true);
        successView.GetComponent<SuccessScreenController>().Initialize(ple, ple.currentPoints);
    }

    public void AnsweredIncorrectly()
    {
        ple.currentPoints /= 2; 
    }

    public void ClosePuzzleController()
    {
        ApplicationManager.Instance.StopScanning();
        ple.cc.AddBackFunctionality();
        gameObject.SetActive(false);
    }
}
