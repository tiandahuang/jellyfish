using UnityEngine;
using TMPro;

public class LogToText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI debugText; // �α׸� ǥ���� TextMeshProUGUI ����

    private void OnEnable()
    {
        // Application.logMessageReceived �ݹ� ���
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        // �ݹ� ����
        Application.logMessageReceived -= HandleLog;
    }

    
    private int maxLines = 100; // ����

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        debugText.text += logString + "\n";

        // ������ ���: ���� �ʹ� ������ �� �κк��� ����
        string[] lines = debugText.text.Split('\n');
        if (lines.Length > maxLines)
        {
            // �պκ� �Ϻ� �߶󳻼� �ٽ� ��ġ��
            int diff = lines.Length - maxLines;
            System.Array.Copy(lines, diff, lines, 0, maxLines);
            debugText.text = string.Join("\n", lines, 0, maxLines);
        }
    }

}
