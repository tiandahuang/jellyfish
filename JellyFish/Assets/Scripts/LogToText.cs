using UnityEngine;
using TMPro;

public class LogToText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI debugText; // 로그를 표시할 TextMeshProUGUI 참조

    private void OnEnable()
    {
        // Application.logMessageReceived 콜백 등록
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        // 콜백 해제
        Application.logMessageReceived -= HandleLog;
    }

    
    private int maxLines = 100; // 예시

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        debugText.text += logString + "\n";

        // 간단한 방법: 줄이 너무 많으면 앞 부분부터 삭제
        string[] lines = debugText.text.Split('\n');
        if (lines.Length > maxLines)
        {
            // 앞부분 일부 잘라내서 다시 합치기
            int diff = lines.Length - maxLines;
            System.Array.Copy(lines, diff, lines, 0, maxLines);
            debugText.text = string.Join("\n", lines, 0, maxLines);
        }
    }

}
