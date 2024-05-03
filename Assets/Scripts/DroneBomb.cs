using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBomb : MonoBehaviour
{
    public string playerTag = "Player"; // ��� ������
    public float movementSpeed = 5f; // �������� �������� �����
    public float rotationSpeed = 5f; // �������� �������� �����
    public float distanceDetections = 15f; // ��������� ��� �����������
    public float detonationDistance = 1f; // ��������� ��� ������
    public int damage = 20; // ���� ������
    public GameObject model; // ������ �����

    public ParticleSystem explosionEffect; // ������ ������

    private bool isPlayerInRange = false; // ����� � ������� ���������
    private bool isDetonated = false; // ���� ������
    private Transform player; // ������ �� ��������� ������
    private EnemyHealth health; // ������ �� �������� �����
    private bool isChasingPlayer = false; // ���� ��� ������������ ������������� ������

    void Start()
    {
        health = GetComponent<EnemyHealth>(); // �������� ��������� EnemyHealth
        // ������� ������ �� ����
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        if (player == null)
        {
            Debug.LogWarning("�� ������ ����� � ����� " + playerTag);
        }
    }

    void Update()
    {
        if (player != null)
        {
            // ���������, ��������� �� ����� � ��������� ���������
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= distanceDetections)
            {
                isPlayerInRange = true;
            }
            if (distanceToPlayer <= detonationDistance && !isDetonated)
            {
                Detonate(); // ����������, ���� ����� � ���� ���������
            }

            // ���� ����� � ������� ���������, ����� � ���� � ������� �� ����
            if (isPlayerInRange)
            {
                if (!isChasingPlayer) // ���� �� ������ �� �������, �������� �������
                {
                    isChasingPlayer = true;
                    StartCoroutine(ChasePlayer());
                }
            }
        }
    }

    // ������� ��� ������ �����
    void Detonate()
    {
        // ������������� ���� ������
        isDetonated = true;

        // ������� ���� ������
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // ���������� ������ �����
        Destroy(model);

        // ��������� �������� ������ � ���������� ����
        StartCoroutine(DetonateDrone());
    }

    // ������� ��� ����������� � ������
    void FlyTowardsPlayer()
    {
        // ����������� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // �������� � ����������� ������
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // ������� � ������
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // �������� ��� ������ �����
    IEnumerator DetonateDrone()
    {
        // ������������� ������ ������
        if (explosionEffect != null)
        {
            explosionEffect.Play();
        }

        // ���� ��������� ����� ����� ������������ �����
        yield return new WaitForSeconds(1f);

        // ������� �����
        health.Die();
    }

    // �������� ��� ������������� ������
    IEnumerator ChasePlayer()
    {
        while (isChasingPlayer)
        {
            if (player != null)
            {
                // ����� � ������
                FlyTowardsPlayer();
            }
            else
            {
                // ���� ����� ���������, ���������� �������������
                isChasingPlayer = false;
            }
            yield return null;
        }
    }
}
