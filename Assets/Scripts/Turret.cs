using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public string playerTag = "Player"; // Тег игрока
    public float rotationSpeed = 10f; // Скорость поворота турели
    public float DistanceDetections = 15f; // Дистанция обнаружения игрока
    public Transform[] firePoints; // Массив точек выстрела
    public float fireRate = 0.1f; // Частота стрельбы
    public GameObject bulletPrefab; // Префаб пули
    public float bulletSpeed = 50f; // Скорость пули
    public ParticleSystem muzzleFlash; // Эффект музы

    private Transform player; // Ссылка на трансформ игрока
    private bool canShoot = true; // Флаг, позволяющий стрелять

    void Start()
    {
        // Находим игрока по тегу
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform; // Получаем ссылку на трансформ игрока
        }
        else
        {
            Debug.LogWarning("Не найден игрок с тегом " + playerTag);
        }
    }

    void FixedUpdate()
    {
        if (IsPlayerInRange())
        {
            // Поворачиваем турель на игрока
            RotateTurretTowardsPlayer();
            if (canShoot)
            {
                StartCoroutine(ShootWithDelay()); // Запускаем стрельбу с задержкой
            }
        }
    }

    // Поворот турели на игрока
    void RotateTurretTowardsPlayer()
    {
        if (player != null)
        {
            // Получаем направление к игроку только по горизонтали
            Vector3 playerDirection = player.position - transform.position;
            playerDirection.y = 0f; // Обнуляем Y-координату, чтобы игнорировать вертикальное движение

            // Поворачиваем турель на игрока, ограничивая углы по вертикали
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Проверка, находится ли игрок в зоне обнаружения
    bool IsPlayerInRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) < DistanceDetections;
    }

    // Стрельба с задержкой между выстрелами
    IEnumerator ShootWithDelay()
    {
        canShoot = false; // Останавливаем возможность стрелять
        foreach (Transform firePoint in firePoints)
        {
            Shoot(firePoint); // Стреляем из каждой точки выстрела
            yield return new WaitForSeconds(fireRate); // Задержка между выстрелами
        }
        canShoot = true; // Возобновляем возможность стрелять
    }

    // Функция стрельбы
    void Shoot(Transform firePoint)
    {
        if (muzzleFlash != null)
        {
            // Показываем эффект музы
            muzzleFlash.gameObject.transform.position = firePoint.position;
            muzzleFlash.Play();
        }

        if (bulletPrefab != null)
        {
            // Создаем пулю и задаем ей скорость
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}
