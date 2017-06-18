using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour {

    public RectTransform content;
    public RectTransform indicator;

    public void OnClickPuzzle()
    {
        Debug.Log("Puzzle Tab");
        content.anchoredPosition = new Vector2(0, 860);
        indicator.anchoredPosition = new Vector2(-255, -90);
    }

    public void OnClickStats()
    {
        Debug.Log("Stats Tab");
        content.anchoredPosition = new Vector2(-1080, 860);
        indicator.anchoredPosition = new Vector2(38, -90);
    }

    public void OnClickRewards()
    {
        Debug.Log("Rewards Tab");
        content.anchoredPosition = new Vector2(-2160, 860);
        indicator.anchoredPosition = new Vector2(341, -90);
    }
}
