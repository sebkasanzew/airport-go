using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolvedPuzzleController : MonoBehaviour {

    public Text title;
    public Text points;

    public void Initialize(string title, int points)
    {
        this.title.text = title;
        this.points.text = points.ToString();
    }
}
