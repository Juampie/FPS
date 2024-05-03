using System.Collections;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Vector3 recoilPosition = new Vector3(0f, 0f, -0.1f); // ������ ��� ������� ������ �� �������
    public Vector3 recoilRotation = new Vector3(2f, 0f, 0f); // ������ ��� ������� ������ �� ��������
    public float recoilDuration = 0.3f; // ������������ ������� ������

    private Vector3 initialPosition; // ��������� ������� �������
    private Quaternion initialRotation; // ��������� ������� �������

    void Start()
    {
        initialPosition = transform.localPosition; // ��������� ��������� ������� �������
        initialRotation = transform.localRotation; // ��������� ��������� ������� �������
    }

    public void ApplyRecoil()
    {
        StartCoroutine(RecoilCoroutine());
    }

    IEnumerator RecoilCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            // �������� ������� ������� �� �������� ���������� � �������� ����������� �� ������� ������ �� �������
            transform.localPosition = Vector3.Lerp(initialPosition, initialPosition + recoilPosition, elapsedTime / recoilDuration);

            // �������� ������� ������� �� �������� ���������� � �������� ����������� �� ������� ������ �� ��������
            transform.localRotation = Quaternion.Lerp(initialRotation, Quaternion.Euler(initialRotation.eulerAngles + recoilRotation), elapsedTime / recoilDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition; // ���������� ������ � ��������� �������
        transform.localRotation = initialRotation; // ���������� ������ � ��������� �������
    }
}
