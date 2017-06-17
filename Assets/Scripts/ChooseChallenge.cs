using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ChooseChallenge : MonoBehaviour {

    public GameObject challenge;

    private enum Mode
    {
        publicA,
        passenger,
        security
    }

    string publicAreaS = "https://airport-go.herokuapp.com/puzzles/public-area";
    string passengerZoneS = "https://airport-go.herokuapp.com/puzzles/passenger-zone";
    string securityLine = "https://airport-go.herokuapp.com/puzzles/security-check";

    public void ChoosePublicArea()
    {
        StartCoroutine(GetRequest(Mode.publicA));
    }

    public void ChoosePassengerZone()
    {
        StartCoroutine(GetRequest(Mode.passenger));
    }

    public void ChooseSecurityLine()
    {
        StartCoroutine(GetRequest(Mode.security));
    }

    IEnumerator GetRequest(Mode m)
    {
        Debug.Log("Create Unity Web Request");
        UnityWebRequest get = UnityWebRequest.Get(publicAreaS);
        switch (m)
        {
            case Mode.publicA:
                get = UnityWebRequest.Get(publicAreaS);
                break;
            case Mode.passenger:
                get = UnityWebRequest.Get(passengerZoneS);
                break;
            case Mode.security:
                get = UnityWebRequest.Get(securityLine);
                break;
            default:
                Debug.LogError("Wrong Mode");
                break;
        }
        yield return get.Send();
        if (get.isError) {
            Debug.LogError("Error");
        }
        else
        {
            string text = get.downloadHandler.text;
            Debug.Log(text);
            //string jsonString = "{ \"Items\": [ { \"type\": \"8484239823\" }, {  \"type\": \"512343283\"} ] }";
            var n = JSON.Parse(text);

            List<Puzzles> puzzles = new List<Puzzles>();
            string area = "";
            switch (m)
            {
                case Mode.publicA:
                    area = "publicArea";
                    break;
                case Mode.passenger:
                    area = "passengerZone";
                    break;
                case Mode.security:
                    area = "securityCheck";
                    break;
                default:
                    Debug.LogError("Wrong Mode");
                    break;
            }

            for (int i = 0; i < n[area].Count; i++)
            {
                Puzzles puzzle = new Puzzles(n[area][i]["type"].Value);
                puzzle.title = n[area][i]["title"].Value;
                puzzle.points = n[area][i]["points"].AsInt;
                puzzle.description = n[area][i]["description"].Value;
                puzzle.beaconID = -1;
                puzzle.correctAnswer = -1;

                switch (puzzle.type)
                {
                    case "find":
                        puzzle.url = n[area][i]["image"].Value;
                        puzzle.beaconID = n[area][i]["beaconID"].AsInt;
                        break;
                    case "question":
                        puzzle.correctAnswer = n[area][i]["correctAnswer"].AsInt;
                        for (int j = 0; j < n[area][i]["answers"].Count; j++)
                        {
                            puzzle.answers.Add(n[area][i]["answers"][j]["text"].Value);
                        }

                        break;
                    case "quest":
                        break;
                    default:
                        break;
                }

                puzzles.Add(puzzle);
            }


            foreach (Puzzles p in puzzles)
            {
                p.printString();
            }

            challenge.SetActive(true);
            challenge.GetComponent<ChallengeContoller>().Initialize(puzzles);

        }
    }
}
