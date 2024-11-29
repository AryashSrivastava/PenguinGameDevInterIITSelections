using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Boo");
    }
    public TextMeshProUGUI debugText;  // Reference to the TMP text area where logs will appear
    private string logMessages = "";   // Store all log messages

    void OnEnable()
    {
        // Register to listen to Unity's log messages
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        // Unregister the listener when not needed
        Application.logMessageReceived -= HandleLog;
    }

    // This function will be called whenever a log message is received
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        logMessages += logString + "\n";  // Append the log message
        debugText.text = logMessages;     // Update the displayed text in the UI
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Press C to clear logs
        {
            logMessages = "";
            debugText.text = "";
        }
    }

}
