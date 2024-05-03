using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBomb : MonoBehaviour
{
    public string playerTag = "Player"; // Тег игрока
    public float movementSpeed = 5f; // Скорость движения дрона
    public float rotationSpeed = 5f; // Скорость поворота дрона
    public float distanceDetections = 15f; // Дистанция для обнаружения
    public float detonationDistance = 1f; // Дистанция для взрыва
    public int damage = 20; // Урон взрыва
    public GameObject model; // Модель дрона

    public ParticleSystem explosionEffect; // Эффект взрыва

    private bool isPlayerInRange = false; // Игрок в радиусе детонации
    private bool isDetonated = false; // Флаг взрыва
    private Transform player; // Ссылка на трансформ игрока
    private EnemyHealth health; // Ссылка на здоровье врага
    private bool isChasingPlayer = false; // Флаг для отслеживания преследования игрока

    void Start()
    {
        health = GetComponent<EnemyHealth>(); // Получаем компонент EnemyHealth
        // Находим игрока по тегу
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        if (player == null)
        {
            Debug.LogWarning("Не найден игрок с тегом " + playerTag);
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Проверяем, находится ли игрок в дистанции детонации
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= distanceDetections)
            {
                isPlayerInRange = true;
            }
            if (distanceToPlayer <= detonationDistance && !isDetonated)
            {
                Detonate(); // Взрываемся, если игрок в зоне детонации
            }

            // Если игрок в радиусе детонации, летим к нему и смотрим на него
            if (isPlayerInRange)
            {
                if (!isChasingPlayer) // Если не следим за игроком, начинаем следить
                {
                    isChasingPlayer = true;
                    StartCoroutine(ChasePlayer());
                }
            }
        }
    }

    // Функция для взрыва дрона
    void Detonate()
    {
        // Устанавливаем флаг взрыва
        isDetonated = true;

        // Наносим урон игроку
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Уничтожаем модель дрона
        Destroy(model);

        // Запускаем взрывной эффект и уничтожаем дрон
        StartCoroutine(DetonateDrone());
    }

    // Функция для перемещения к игроку
    void FlyTowardsPlayer()
    {
        // Направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // Движение в направлении игрока
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Поворот к игроку
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Корутина для взрыва дрона
    IEnumerator DetonateDrone()
    {
        // Воспроизводим эффект взрыва
        if (explosionEffect != null)
        {
            explosionEffect.Play();
        }

        // Ждем некоторое время перед уничтожением дрона
        yield return new WaitForSeconds(1f);

        // Убиваем дрона
        health.Die();
    }

    // Корутина для преследования игрока
    IEnumerator ChasePlayer()
    {
        while (isChasingPlayer)
        {
            if (player != null)
            {
                // Летим к игроку
                FlyTowardsPlayer();
            }
            else
            {
                // Если игрок уничтожен, прекращаем преследование
                isChasingPlayer = false;
            }
            yield return null;
        }
    }
}
