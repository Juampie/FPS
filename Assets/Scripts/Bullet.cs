using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ����, ��������� �����
    public int damage = 10;

    // ����� �������� ����� ������������ ����
    public float destroyDelay = 3f;

    // ����, �����������, ����������� �� ���� � ���-��
    private bool collided = false;

    // ���������� ��� ������ �������
    private void Start()
    {
        // ��������� �������� ��� ����������� ���� ����� ��������
        StartCoroutine(DestroyAfterDelay());
    }

    // ���������� ��� ������������ � ������ �����������
    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� ���� �����
        if (!collided)
        {
            // ������������� ���� ������������ � true
            collided = true;

            // �������� ��������� Health ����, ���� �� ����������
            EnemyHealth targetHealth = collision.gameObject.GetComponent<EnemyHealth>();

            // ���� ���� ����� ��������� Health, ������� �� ����
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }

            // �������� ��������� Health ������, ���� �� ����������
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }

            // ���������� ���� ����� ������������
            Destroy(gameObject);
        }
    }

    // �������� ��� ����������� ���� ����� ��������
    IEnumerator DestroyAfterDelay()
    {
        // ���� �������� �����
        yield return new WaitForSeconds(destroyDelay);

        // ���������, �� ����������� �� ���� �� ��� �����
        if (!collided)
        {
            // ���� ���� �� �����������, ���������� �
            Destroy(gameObject);
        }
    }
}
