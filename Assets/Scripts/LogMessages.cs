using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogMessages : MonoBehaviour
{

    public bool debugActive;
    public Text debugText;
    private string myLog = "";
    private string stack;
    private string output;

    void Start()
    {
        gameObject.SetActive(debugActive);
        debugText.fontSize = 24;
    }

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        myLog = output + "\n" + myLog;
        if (myLog.Length > 500)
        {
            myLog = myLog.Substring(0, 500);
        }
    }

    void Update()
    {
        debugText.text = myLog;
    }
}
