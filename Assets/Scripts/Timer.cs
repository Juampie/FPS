using UnityEngine;

public static class Timer
{
    private static float startTime;
    private static float elapsedTime;

    private static bool isRunning = false;

    // Запускаем таймер
    public static void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    // Останавливаем таймер
    public static void StopTimer()
    {
        if (isRunning)
        {
            elapsedTime = Time.time - startTime;
            isRunning = false;
        }
    }

    // Получаем прошедшее время
    public static float GetElapsedTime()
    {
        if (isRunning)
        {
            return Time.time - startTime;
        }
        else
        {
            return elapsedTime;
        }
    }

    // Сбрасываем таймер
    public static void ResetTimer()
    {
        startTime = Time.time;
        elapsedTime = 0f;
    }
}
