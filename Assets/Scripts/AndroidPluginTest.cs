using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPluginTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var ajc = new AndroidJavaClass("com.androidaddin.androidaddin.Helper"); //(1)
        ajc.CallStatic("DoSthInAndroid");                                       //(2)
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
