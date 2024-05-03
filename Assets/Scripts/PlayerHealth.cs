using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье игрока
    private int currentHealth; // Текущее здоровье игрока

    public Slider healthSlider; // Ссылка на UI Slider для отображения здоровья

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное здоровье равным максимальному

        // Устанавливаем максимальное значение для Slider равным максимальному здоровью
        healthSlider.maxValue = maxHealth;
        // Устанавливаем текущее значение для Slider равным текущему здоровью
        healthSlider.value = currentHealth;
    }

    // Функция для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье на величину полученного урона
        Debug.Log(gameObject.name + " получил урон: " + damage + ". Осталось здоровья: " + currentHealth);

        // Обновляем значение Slider при получении урона
        healthSlider.value = currentHealth;

        // Проверяем, осталось ли здоровье у игрока
        if (currentHealth <= 0)
        {
            Die(); // Если здоровье меньше или равно нулю, игрок умирает
        }
    }

    // Функция для смерти игрока
    void Die()
    {
        Debug.Log(gameObject.name + " умер.");

        // Останавливаем и сбрасываем таймер
        Timer.StopTimer();
        Timer.ResetTimer();

        // Переключаемся на первую сцену
        SceneSwitcher.SwitchToFirst();
    }
}
