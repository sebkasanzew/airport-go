using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ChooseChallenge : MonoBehaviour {

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
            switch (m)
            {
                case Mode.publicA:
                    for (int i = 0; i < n["publicArea"].Count; i++) {
                        Puzzles puzzle = new Puzzles(n["publicArea"][i]["type"].Value);
                        puzzle.points = n["publicArea"][i]["points"].AsInt;
                        puzzle.description = n["publicArea"][i]["description"].Value;
                        puzzle.beaconID = -1;
                        puzzle.correctAnswer = -1;

                        switch (puzzle.type)
                        {
                            case "find":
                                puzzle.url = n["publicArea"][i]["image"].Value;
                                puzzle.beaconID = n["publicArea"][i]["beaconID"].AsInt;
                                break;
                            case "question":
                                puzzle.correctAnswer = n["publicArea"][i]["correctAnswer"].AsInt;
                                for(int j = 0; j< n["publicArea"][i]["answers"].Count; j++)
                                {
                                    puzzle.answers.Add(n["publicArea"][i]["answers"][j]["text"].Value);
                                }
                                
                                break;
                            case "quest":
                                break;
                            default:
                                break;
                        }

                        puzzles.Add(puzzle);
                    }
                    break;
                case Mode.passenger:
                    break;
                case Mode.security:
                    break;
                default:
                    Debug.LogError("Wrong Mode");
                    break;
            }

            foreach(Puzzles p in puzzles)
            {
                p.printString();
            }

        }
    }
}
