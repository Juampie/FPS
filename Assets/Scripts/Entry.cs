using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    // ����������, ����� ������ ��������� ������ � �������
    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� �������� ������ �������
        if (other.CompareTag("Player"))
        {
            // ������������� �� ��������� ����� ��� ������
            SceneSwitcher.SwitchToNextSceneOrFirst();

            // ��������� ������
            Timer.StartTimer();
        }
    }
}
