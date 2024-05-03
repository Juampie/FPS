using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Урон, наносимый пулей
    public int damage = 10;

    // Время задержки перед уничтожением пули
    public float destroyDelay = 3f;

    // Флаг, указывающий, столкнулась ли пуля с чем-то
    private bool collided = false;

    // Вызывается при старте объекта
    private void Start()
    {
        // Запускаем корутину для уничтожения пули после задержки
        StartCoroutine(DestroyAfterDelay());
    }

    // Вызывается при столкновении с другим коллайдером
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулась ли пуля ранее
        if (!collided)
        {
            // Устанавливаем флаг столкновения в true
            collided = true;

            // Получаем компонент Health цели, если он существует
            EnemyHealth targetHealth = collision.gameObject.GetComponent<EnemyHealth>();

            // Если цель имеет компонент Health, наносим ей урон
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            // Получаем компонент Health игрока, если он существует
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }

            // Уничтожаем пулю после столкновения
            Destroy(gameObject);
        }
    }

    // Корутина для уничтожения пули после задержки
    IEnumerator DestroyAfterDelay()
    {
        // Ждем заданное время
        yield return new WaitForSeconds(destroyDelay);

        // Проверяем, не столкнулась ли пуля за это время
        if (!collided)
        {
            // Если пуля не столкнулась, уничтожаем её
            Destroy(gameObject);
        }
    }
}
