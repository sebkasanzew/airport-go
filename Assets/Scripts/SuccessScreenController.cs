using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessScreenController : MonoBehaviour {

    public Text pointText;

    public Text shopTitle;
    public Text openingHours;
    public Text descriptionText;

    public GameObject extra;
    PuzzleListElement plz;

    public void Initialize(PuzzleListElement plz, int points)
    {
        ApplicationManager.Instance.AddListenerToBackButton(CloseSuccessView);
        pointText.text = points.ToString() + " Points";
        this.plz = plz;
        Puzzles puzzle = plz.puzzle;

        if (puzzle.type == "find")
        {
            extra.SetActive(true);
            shopTitle.text = puzzle.shopTitle;
            openingHours.text = puzzle.shopOpeningHours;
            descriptionText.text = puzzle.shopDescription;
        }
        else
        {
            extra.SetActive(false);
        }
    }

    public void CloseSuccessView()
    {
        gameObject.SetActive(false);
        ApplicationManager.Instance.AddListenerToBackButton(plz.cc.CloseChallengeController);
    }
}
