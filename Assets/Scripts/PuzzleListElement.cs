using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleListElement : MonoBehaviour {

    public Image icon;
    public Text title;

    public Sprite[] iconSprites;

    private int id;
    private ChallengeContoller cc;

    public void Initialize(Puzzles p, int i, ChallengeContoller cc)
    {
        id = i;
        this.cc = cc;
        icon.sprite = iconSprites[GetSpriteID(p.type)];
        title.text = p.title;
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private int GetSpriteID(string t)
    {
        int id = 0;
        switch (t)
        {
            case "find":
                id = 0;
                break;
            case "question":
                id = 1;
                break;
            case "quest":
                id = 2;
                break;
            default:
                id = -1;
                break;
        }
        return id;
    }

    public void OnClick()
    {
        cc.OnClickListElement(id);
    }
}
