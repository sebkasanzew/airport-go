using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour {

    public RectTransform content;

    public void OnClickPuzzle()
    {
        Debug.Log("Puzzle Tab");
        content.anchoredPosition = new Vector2(0, 860);
    }

    public void OnClickStats()
    {
        Debug.Log("Stats Tab");
        content.anchoredPosition = new Vector2(-1080, 860);
    }

    public void OnClickRewards()
    {
        Debug.Log("Rewards Tab");
        content.anchoredPosition = new Vector2(-2160, 860);
    }
}
