using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int totalEnemies; // Общее количество врагов на уровне
    private int enemiesKilled = 0; // Количество убитых врагов

    void Start()
    {
        // Находим все объекты с тегом "Enemy" и определяем общее количество врагов
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

        // Подписываемся на событие EnemyDied для отслеживания убитых врагов
        EnemyHealth.EnemyDied += EnemyKilled;
    }

    // Функция вызывается при смерти врага
    void EnemyKilled()
    {
        enemiesKilled++; // Увеличиваем счетчик убитых врагов

        // Если убиты все враги, загружаем следующую сцену
        if (enemiesKilled >= totalEnemies)
        {
            LoadNextScene();
        }
    }

    // Функция для загрузки следующей сцены
    private void LoadNextScene()
    {
        SceneSwitcher.SwitchToNextSceneOrFirst(); // Переключаемся на следующую сцену или первую
    }

    // Функция вызывается при выходе из сцены
    private void OnDisable()
    {
        // Отписываемся от события EnemyDied
        EnemyHealth.EnemyDied -= EnemyKilled;
    }
}
