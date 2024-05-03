using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher
{
    // Функция для переключения на указанную сцену по имени
    public static void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Функция для переключения на следующую сцену или первую, если нет следующей
    public static void SwitchToNextSceneOrFirst()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Получаем индекс текущей сцены
        int nextSceneIndex = currentSceneIndex + 1; // Получаем индекс следующей сцены

        // Проверяем, есть ли следующая сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Загружаем следующую сцену
        }
        else
        {
            // Увеличиваем счетчик пройденных кругов
            GameData.IncreaseLaps();

            // Останавливаем таймер и устанавливаем время завершения уровня
            Timer.StopTimer();
            GameData.SetCompletionTime(Timer.GetElapsedTime());
            Timer.ResetTimer();

            SceneManager.LoadScene(0); // Загружаем самую первую сцену
        }
    }

    // Функция для переключения на первую сцену
    public static void SwitchToFirst()
    {
        SceneManager.LoadScene(0);
    }
}
