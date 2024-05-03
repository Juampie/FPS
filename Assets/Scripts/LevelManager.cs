using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int totalEnemies; // ����� ���������� ������ �� ������
    private int enemiesKilled = 0; // ���������� ������ ������

    void Start()
    {
        // ������� ��� ������� � ����� "Enemy" � ���������� ����� ���������� ������
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = enemies.Length;

        // ������������� �� ������� EnemyDied ��� ������������ ������ ������
        EnemyHealth.EnemyDied += EnemyKilled;
    }

    // ������� ���������� ��� ������ �����
    void EnemyKilled()
    {
        enemiesKilled++; // ����������� ������� ������ ������

        // ���� ����� ��� �����, ��������� ��������� �����
        if (enemiesKilled >= totalEnemies)
        {
            LoadNextScene();
        }
    }

    // ������� ��� �������� ��������� �����
    private void LoadNextScene()
    {
        SceneSwitcher.SwitchToNextSceneOrFirst(); // ������������� �� ��������� ����� ��� ������
    }

    // ������� ���������� ��� ������ �� �����
    private void OnDisable()
    {
        // ������������ �� ������� EnemyDied
        EnemyHealth.EnemyDied -= EnemyKilled;
    }
}
