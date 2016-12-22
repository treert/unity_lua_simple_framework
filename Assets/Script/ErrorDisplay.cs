using UnityEngine;
using System.Collections;

public class ErrorDisplay : MonoBehaviour
{
    internal void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }
    internal void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }
    private string m_logs;
    /// <summary>
    /// 
    /// /// </summary>    
    /// /// <param name="logString">错误信息</param>    /// 
    /// <param name="stackTrace">跟踪堆栈</param>    /// 
    /// <param name="type">错误类型</param>    
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        m_logs += logString + "\n";
    }
    public bool Log;
    private Vector2 m_scroll;
    internal void OnGUI_()
    {
        if (!Log)
            return;
        m_scroll = GUILayout.BeginScrollView(m_scroll);
        GUI.skin.label.normal.textColor = Color.red;
        GUILayout.Label(m_logs);
        GUILayout.EndScrollView();
    }


    internal void OnGUI()
    {
        if (!Log)
            return;

        string s = string.Empty;
        foreach(System.Reflection.PropertyInfo info in typeof(Application).GetProperties())
        {
            s += info.Name + " = " + info.GetValue(null, null) + "\n";
        }
        m_scroll = GUILayout.BeginScrollView(m_scroll);
        //GUILayout.BeginScrollView(new Vector2());
        GUI.skin.label.normal.textColor = Color.red;
        GUILayout.Label(s);
        GUILayout.EndScrollView();
    }
}