using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    private string log = "---Begin Log---";
    public bool showConsole = true;
    private int maxChars = 700;

    void OnEnable() { Application.logMessageReceived += Log; }
    void OnDisable() { Application.logMessageReceived -= Log; }
    void Update() { if (Input.GetKeyDown(KeyCode.F2)) { showConsole = !showConsole; } }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        log += "\n" + logString;
        if (log.Length > maxChars) { log = log.Substring(log.Length - maxChars); }
    }

    void OnGUI()
    {
        if (!showConsole) { return; }

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
           new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));

        GUI.TextArea(new Rect(10, 10, 540, 370), log);
    }
}
