using UnityEngine;

public static class GameData
{
    // Переменные для хранения данных
    private static int laps = 0; // Количество пройденных кругов
    private static int enemiesKilled = 0; // Количество убитых врагов
    private static float completionTime = 0f; // Время завершения уровня

    // Увеличить количество пройденных кругов
    public static void IncreaseLaps()
    {
        laps++;
    }

    // Увеличить количество убитых врагов
    public static void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
    }

    // Установить время завершения уровня
    public static void SetCompletionTime(float time)
    {
        completionTime = time;
    }

    // Получить количество пройденных кругов
    public static int GetLaps()
    {
        return laps;
    }

    // Получить количество убитых врагов
    public static int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    // Получить время завершения уровня
    public static float GetCompletionTime()
    {
        return completionTime;
    }
}
