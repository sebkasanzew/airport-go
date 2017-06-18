using OMobile.EstimoteUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationManager : Singleton<ApplicationManager>{

    bool gpsConnected = false;
    LocationInfo lastInfo;

    public EstimoteUnity _EstimoteUnity;

    public int points;

    public GameObject backButton;
    public Text pointText;

    public GameObject solvedList;

    public GameObject solvedPuzzleObject;

    public List<EstimoteUnityBeacon> beacons;

    // Use this for initialization
    void Start () {
        points = 0;
        StartCoroutine(StartLocationService());
        _EstimoteUnity.OnDidRangeBeacons += HandleDidRangeBeacons;
    }


    public void StartScanning()
    {
        _EstimoteUnity.StartScanning();
    }

    public void StopScanning()
    {
        _EstimoteUnity.StopScanning();
    }

    private void HandleDidRangeBeacons(List<EstimoteUnityBeacon> beacons)
    {
        this.beacons = beacons;
    }


    // Update is called once per frame
    void Update () {
        lastInfo = Input.location.lastData;
        //Debug.Log(lastInfo.latitude + " " + lastInfo.longitude);
    }

    IEnumerator StartLocationService()
    {
        Input.location.Start();
        float timer = 0f;

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            timer += Time.deltaTime;
            if (timer > 10)
            {
                Debug.Log("GPS couldn't be started");
                StopCoroutine(StartLocationService());
            }
            yield return 0;

        }
        else
        {
            gpsConnected = true;
        }
    }

    public void HideBackbutton()
    {
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.SetActive(false);
    }

    public void AddListenerToBackButton(UnityEngine.Events.UnityAction action)
    {
        backButton.SetActive(true);
        backButton.GetComponent<Button>().onClick.RemoveAllListeners();
        backButton.GetComponent<Button>().onClick.AddListener(action);
    }

    public void AddPoints(int num)
    {
        points += num;
        pointText.text = points.ToString();
    }

    public void SolvePuzzle(string title, int points)
    {
        Instantiate(solvedPuzzleObject, solvedList.transform).GetComponent<SolvedPuzzleController>().Initialize(title, points);
    }
}
