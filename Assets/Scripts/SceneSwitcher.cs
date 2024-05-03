using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    // ������� ��� ������������ �� ��������� ����� �� �����
    public static void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ������� ��� ������������ �� ��������� ����� ��� ������, ���� ��� ���������
    public static void SwitchToNextSceneOrFirst()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // �������� ������ ������� �����
        int nextSceneIndex = currentSceneIndex + 1; // �������� ������ ��������� �����

        // ���������, ���� �� ��������� �����
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ��������� ��������� �����
        }
        else
        {
            // ����������� ������� ���������� ������
            GameData.IncreaseLaps();

            // ������������� ������ � ������������� ����� ���������� ������
            Timer.StopTimer();
            GameData.SetCompletionTime(Timer.GetElapsedTime());
            Timer.ResetTimer();

            SceneManager.LoadScene(0); // ��������� ����� ������ �����
        }
    }

    // ������� ��� ������������ �� ������ �����
    public static void SwitchToFirst()
    {
        SceneManager.LoadScene(0);
    }
}
