using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье врага
    private int currentHealth; // Текущее здоровье врага
    public static event Action EnemyDied; // Событие смерти врага

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное здоровье равным максимальному
    }

    // Функция для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье на величину полученного урона
        Debug.Log(gameObject.name + " получил урон: " + damage + ". Осталось здоровья: " + currentHealth);

        if (currentHealth <= 0) // Если здоровье меньше или равно нулю, вызываем функцию смерти
        {
            Die();
        }
    }

    // Функция для смерти врага
    public void Die()
    {
        Debug.Log(gameObject.name + " умер.");
        EnemyDied?.Invoke(); // Вызываем событие смерти врага
        GameData.IncreaseEnemiesKilled(); // Увеличиваем счетчик убитых врагов в GameData
        Destroy(gameObject); // Уничтожаем объект врага
    }
}
