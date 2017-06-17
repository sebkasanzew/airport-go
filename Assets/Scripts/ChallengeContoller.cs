using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeContoller : MonoBehaviour {

    private List<Puzzles> puzzles;
    public GameObject puzzleObject;
    public Transform content;
    
    private PuzzleListElement[] puzzleList;

    public GameObject PuzzleDetailView;

    public void Initialize(List<Puzzles> p)
    {
        puzzles = p;
        puzzleList = new PuzzleListElement[puzzles.Count];

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < puzzles.Count; i++)
        {
            puzzleList[i] = Instantiate(puzzleObject, content).GetComponent<PuzzleListElement>();
            puzzleList[i].Initialize(puzzles[i], i, this);
        }

        AddBackFunctionality();
    }

    public void OnClickListElement(int id)
    {
        PuzzleDetailView.SetActive(true);
        PuzzleDetailView.GetComponent<PuzzleController>().Initialize(puzzleList[id]);
    }

    public void AddBackFunctionality()
    {
        ApplicationManager.Instance.AddListenerToBackButton(CloseChallengeController);
    }

    public void CloseChallengeController()
    {
        ApplicationManager.Instance.HideBackbutton();
        gameObject.SetActive(false);
    }
}
