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
        for(int i = 0; i < puzzles.Count; i++)
        {
            puzzleList[i] = Instantiate(puzzleObject, content).GetComponent<PuzzleListElement>();
            puzzleList[i].Initialize(puzzles[i], i, this);
        }
    }

    public void OnClickListElement(int id)
    {
        PuzzleDetailView.SetActive(true);
        PuzzleDetailView.GetComponent<PuzzleController>().Initialize(puzzleList[id]);
    }
}
