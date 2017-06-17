using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSTest : MonoBehaviour {

    Text thisText;
    bool gpsConnected = false;
	// Use this for initialization
	void Start () {
        thisText = GetComponent<Text>();
        StartCoroutine(StartLocationService());
	}

    // Update is called once per frame
    void Update()
    {
        if (gpsConnected)
        {
            LocationInfo info = Input.location.lastData;
            thisText.text = info.longitude.ToString() + " " + info.latitude.ToString() + " " + info.verticalAccuracy.ToString() + " " + info.horizontalAccuracy.ToString();
        }
    }

    IEnumerator StartLocationService()
    {
        Input.location.Start();
        float timer = 0f;

        if (Input.location.status == LocationServiceStatus.Failed) {
            timer += Time.deltaTime;
            if(timer > 10)
            {
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
