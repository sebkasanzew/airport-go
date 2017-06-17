using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Challenge
{
    public List<Puzzles> puzzles;

    public static Challenge CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Challenge>(jsonString);
    }
}

[System.Serializable]
public class Puzzles
{
    public string type;
    public string title;
    public int points;
    public string description;
    public string url;
    public int beaconID;
    public List<string> answers;
    public int correctAnswer;

    public Puzzles(string t)
    {
        type = t;
        answers = new List<string>();
    }

    public void printString()
    {
        string debugURL = url != null? url : "";
        string debugBeacon = beaconID > 0 ? beaconID.ToString() : "";
        string debugCorrectAnswer = correctAnswer >= 0 ? correctAnswer.ToString() : "";
        Debug.Log(type
            + " "
            + title
            + " " 
            + points.ToString() 
            + " " 
            + description 
            + " " 
            + debugURL
            + " " 
            + debugBeacon
            + " " 
            + debugCorrectAnswer);
    }

    //public static Puzzles CreateFromJSON(string jsonString)
    //{/
    //    return JsonUtility.FromJson<Puzzles>(jsonString);
    //}
}
