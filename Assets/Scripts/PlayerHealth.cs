using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ �������� ������
    private int currentHealth; // ������� �������� ������

    public Slider healthSlider; // ������ �� UI Slider ��� ����������� ��������

    void Start()
    {
        currentHealth = maxHealth; // ������������� ��������� �������� ������ �������������

        // ������������� ������������ �������� ��� Slider ������ ������������� ��������
        healthSlider.maxValue = maxHealth;
        // ������������� ������� �������� ��� Slider ������ �������� ��������
        healthSlider.value = currentHealth;
    }

    // ������� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� �������� �� �������� ����������� �����
        Debug.Log(gameObject.name + " ������� ����: " + damage + ". �������� ��������: " + currentHealth);

        // ��������� �������� Slider ��� ��������� �����
        healthSlider.value = currentHealth;

        // ���������, �������� �� �������� � ������
        if (currentHealth <= 0)
        {
            Die(); // ���� �������� ������ ��� ����� ����, ����� �������
        }
    }

    // ������� ��� ������ ������
    void Die()
    {
        Debug.Log(gameObject.name + " ����.");

        // ������������� � ���������� ������
        Timer.StopTimer();
        Timer.ResetTimer();

        // ������������� �� ������ �����
        SceneSwitcher.SwitchToFirst();
    }
}
