using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    // Вызывается, когда другой коллайдер входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли вошедший объект игроком
        if (other.CompareTag("Player"))
        {
            // Переключаемся на следующую сцену или первую
            SceneSwitcher.SwitchToNextSceneOrFirst();

            // Запускаем таймер
            Timer.StartTimer();
        }
    }
}
