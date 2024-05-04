using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка выстрела
    public Camera mainCamera; // Ссылка на основную камеру
    public float bulletSpeed = 50f; // Скорость пули
    public int maxAmmo = 12; // Максимальное количество патронов
    public float reloadTime = 1f; // Время перезарядки
    public float fireRate = 0.1f; // Частота стрельбы

    private int currentAmmo; // Текущее количество патронов в магазине
    private int remainingAmmo; // Оставшееся количество патронов
    private bool isReloading = false; // Флаг перезарядки
    private bool canShoot = true; // Можно ли стрелять
    private Recoil recoilController; // Ссылка на компонент управления отдачей
    private Animation reloadAnimation; // Анимация перезарядки
    private AudioSource audio; // Звук выстрела

    public Text ammoText; // Текстовое поле для отображения текущего количества патронов
    public Text remainingAmmoText; // Текстовое поле для отображения оставшегося количества патронов
    public ParticleSystem muzzleFlash; // Эффект музы

    void Start()
    {
        currentAmmo = maxAmmo; // Устанавливаем начальное количество патронов
        remainingAmmo = maxAmmo * 5; // Устанавливаем оставшееся количество патронов
        UpdateAmmoUI(); // Обновляем UI

        recoilController = GetComponentInChildren<Recoil>(); // Получаем компонент управления отдачей
        reloadAnimation = GetComponent<Animation>(); // Получаем компонент анимации перезарядки
        audio = GetComponent<AudioSource>();

        if (recoilController == null)
        {
            Debug.LogError("Recoil component not found in children."); // Выводим ошибку, если компонент управления отдачей не найден
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Если ссылка на основную камеру не установлена, пытаемся найти ее автоматически
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found. Please assign the main camera in the inspector."); // Выводим ошибку, если основная камера не найдена
            }
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && remainingAmmo != 0)
        {
            StartCoroutine(Reload()); // Запускаем перезарядку, если закончились патроны в магазине и есть запасные
            return;
        }

        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShootWithDelay()); // Запускаем стрельбу с задержкой между выстрелами
        }

        if (Input.GetKeyDown(KeyCode.R) && remainingAmmo != 0)
        {
            StartCoroutine(Reload()); // Запускаем перезарядку при нажатии на кнопку R, если есть запасные патроны
        }

        // Проверяем центр экрана и направляем firePoint в эту точку
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            firePoint.LookAt(hit.point);
        }
        else
        {
            firePoint.LookAt(ray.GetPoint(1000)); // Если луч не пересек объекты, направляем firePoint на расстояние 1000 единиц
        }
    }

    // Функция стрельбы
    void Shoot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--; // Уменьшаем количество патронов в магазине
            muzzleFlash.Play(); // Воспроизводим эффект выстрела
            audio.Play(); // Воспроизводим звук выстрела

            // Создаем пулю и задаем ей скорость
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }

            recoilController.ApplyRecoil(); // Применяем отдачу
            UpdateAmmoUI(); // Обновляем UI
        }
        else
        {
            Debug.Log("Out of ammo!"); // Выводим сообщение, если закончились патроны в магазине
        }
    }



    // Корутина для перезарядки
    IEnumerator Reload()
    {
        reloadAnimation.Play(); // Воспроизводим анимацию перезарядки
        isReloading = true; // Устанавливаем флаг перезарядки

        Debug.Log("Reloading..."); // Выводим сообщение о перезарядке

        yield return new WaitForSeconds(reloadTime); // Ждем время перезарядки

        // Вычисляем доступное количество патронов для перезарядки
        int availableAmmo = maxAmmo - currentAmmo;
        // Прибавляем к текущему количеству патронов минимум из оставшихся и доступных для перезарядки
        currentAmmo += Mathf.Min(remainingAmmo, availableAmmo);
        // Уменьшаем оставшиеся патроны на количество использованных для перезарядки
        remainingAmmo = Mathf.Max(0, remainingAmmo - availableAmmo);

        isReloading = false; // Снимаем флаг перезарядки
        UpdateAmmoUI(); // Обновляем UI
    }

    // Обновление информации о патронах на UI
    void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo.ToString(); // Обновляем количество патронов в магазине
        remainingAmmoText.text = remainingAmmo.ToString(); // Обновляем количество оставшихся патронов
    }

    // Корутина для стрельбы с задержкой
    IEnumerator ShootWithDelay()
    {
        canShoot = false; // Устанавливаем флаг, запрещающий стрельбу
        Shoot(); // Выполняем стрельбу

        yield return new WaitForSeconds(fireRate); // Ждем заданное время между выстрелами

        canShoot = true; // Разрешаем снова стрелять
    }
}
