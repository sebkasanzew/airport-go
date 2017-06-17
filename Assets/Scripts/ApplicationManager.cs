using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : Singleton<ApplicationManager>{

    bool gpsConnected = false;
    LocationInfo lastInfo;

    public int points;

    // Use this for initialization
    void Start () {
        points = 0;
        StartCoroutine(StartLocationService());
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
}
