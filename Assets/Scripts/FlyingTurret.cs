using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public string playerTag = "Player";
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float distanceDetection = 15f;
    public float stopDistance = 2f; // Ќова€ переменна€ дл€ рассто€ни€, на котором дрон остановитс€
    public float fireRate = 1f;
    public float bulletSpeed = 50f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    private Transform player;
    private bool isPlayerInRange = false;
    private bool canShoot = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        if (player == null)
        {
            Debug.LogWarning("Player with tag " + playerTag + " not found.");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= distanceDetection;

        // ѕровер€ем, если дистанци€ меньше или равна stopDistance, то останавливаем дрона
        if (distanceToPlayer <= stopDistance)
        {
            RotateTowardsPlayer();

            if (canShoot)
            {
                Shoot();
            }
        }
        else if (isPlayerInRange)
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
            if (canShoot)
            {
                Shoot();
            }
        }
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // —оздаем пулю
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // ѕолучаем коллайдер пули
            Collider bulletCollider = bullet.GetComponent<Collider>();

            // ѕолучаем коллайдер дрона
            Collider droneCollider = GetComponent<Collider>();

            // »гнорируем столкновение между коллайдером пули и коллайдером дрона
            Physics.IgnoreCollision(bulletCollider, droneCollider);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }

            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            canShoot = false;
            Invoke("ResetShoot", fireRate);
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}