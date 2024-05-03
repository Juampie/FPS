using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ �������� �����
    private int currentHealth; // ������� �������� �����
    public static event Action EnemyDied; // ������� ������ �����

    void Start()
    {
        currentHealth = maxHealth; // ������������� ��������� �������� ������ �������������
    }

    // ������� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� �������� �� �������� ����������� �����
        Debug.Log(gameObject.name + " ������� ����: " + damage + ". �������� ��������: " + currentHealth);

        if (currentHealth <= 0) // ���� �������� ������ ��� ����� ����, �������� ������� ������
        {
            Die();
        }
    }

    // ������� ��� ������ �����
    public void Die()
    {
        Debug.Log(gameObject.name + " ����.");
        EnemyDied?.Invoke(); // �������� ������� ������ �����
        GameData.IncreaseEnemiesKilled(); // ����������� ������� ������ ������ � GameData
        Destroy(gameObject); // ���������� ������ �����
    }
}
