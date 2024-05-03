using UnityEngine;

public class DisplayGameData : MonoBehaviour
{
    // ������ �� ��������� ���� ��� ����������� ������
    public TextMesh lapsTM; // ��������� ���� ��� ����������� ���������� ������
    public TextMesh enemiesKilledTM; // ��������� ���� ��� ����������� ���������� ������ ������
    public TextMesh completionTimeTM; // ��������� ���� ��� ����������� ������� ���������� ������

    void Update()
    {
        // �������� ������ �� GameData
        int laps = GameData.GetLaps(); // �������� ���������� ���������� ������
        int enemiesKilled = GameData.GetEnemiesKilled(); // �������� ���������� ������ ������
        float completionTime = GameData.GetCompletionTime(); // �������� ����� ���������� ������

        // ��������� ��������� ���� � ����������� �������
        lapsTM.text = laps.ToString(); // ��������� ��������� ���� � ����������� ������
        enemiesKilledTM.text = enemiesKilled.ToString(); // ��������� ��������� ���� � ����������� ������ ������
        completionTimeTM.text = completionTime.ToString("F2"); // ��������� ��������� ���� � �������� ���������� ������, �������� ��� �� ���� ������ ����� �������
    }
}
