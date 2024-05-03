using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public string playerTag = "Player"; // ��� ������
    public float rotationSpeed = 10f; // �������� �������� ������
    public float DistanceDetections = 15f; // ��������� ����������� ������
    public Transform[] firePoints; // ������ ����� ��������
    public float fireRate = 0.1f; // ������� ��������
    public GameObject bulletPrefab; // ������ ����
    public float bulletSpeed = 50f; // �������� ����
    public ParticleSystem muzzleFlash; // ������ ����

    private Transform player; // ������ �� ��������� ������
    private bool canShoot = true; // ����, ����������� ��������

    void Start()
    {
        // ������� ������ �� ����
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform; // �������� ������ �� ��������� ������
        }
        else
        {
            Debug.LogWarning("�� ������ ����� � ����� " + playerTag);
        }
    }

    void FixedUpdate()
    {
        if (IsPlayerInRange())
        {
            // ������������ ������ �� ������
            RotateTurretTowardsPlayer();
            if (canShoot)
            {
                StartCoroutine(ShootWithDelay()); // ��������� �������� � ���������
            }
        }
    }

    // ������� ������ �� ������
    void RotateTurretTowardsPlayer()
    {
        if (player != null)
        {
            // �������� ����������� � ������ ������ �� �����������
            Vector3 playerDirection = player.position - transform.position;
            playerDirection.y = 0f; // �������� Y-����������, ����� ������������ ������������ ��������

            // ������������ ������ �� ������, ����������� ���� �� ���������
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // ��������, ��������� �� ����� � ���� �����������
    bool IsPlayerInRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) < DistanceDetections;
    }

    // �������� � ��������� ����� ����������
    IEnumerator ShootWithDelay()
    {
        canShoot = false; // ������������� ����������� ��������
        foreach (Transform firePoint in firePoints)
        {
            Shoot(firePoint); // �������� �� ������ ����� ��������
            yield return new WaitForSeconds(fireRate); // �������� ����� ����������
        }
        canShoot = true; // ������������ ����������� ��������
    }

    // ������� ��������
    void Shoot(Transform firePoint)
    {
        if (muzzleFlash != null)
        {
            // ���������� ������ ����
            muzzleFlash.gameObject.transform.position = firePoint.position;
            muzzleFlash.Play();
        }

        if (bulletPrefab != null)
        {
            // ������� ���� � ������ �� ��������
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}
