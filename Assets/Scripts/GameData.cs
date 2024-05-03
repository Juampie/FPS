using UnityEngine;

public static class GameData
{
    // ���������� ��� �������� ������
    private static int laps = 0; // ���������� ���������� ������
    private static int enemiesKilled = 0; // ���������� ������ ������
    private static float completionTime = 0f; // ����� ���������� ������

    // ��������� ���������� ���������� ������
    public static void IncreaseLaps()
    {
        laps++;
    }

    // ��������� ���������� ������ ������
    public static void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
    }

    // ���������� ����� ���������� ������
    public static void SetCompletionTime(float time)
    {
        completionTime = time;
    }

    // �������� ���������� ���������� ������
    public static int GetLaps()
    {
        return laps;
    }

    // �������� ���������� ������ ������
    public static int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    // �������� ����� ���������� ������
    public static float GetCompletionTime()
    {
        return completionTime;
    }
}
