using System.Collections;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Vector3 recoilPosition = new Vector3(0f, 0f, -0.1f); // Вектор для эффекта отдачи по позиции
    public Vector3 recoilRotation = new Vector3(2f, 0f, 0f); // Вектор для эффекта отдачи по повороту
    public float recoilDuration = 0.3f; // Длительность эффекта отдачи

    private Vector3 initialPosition; // Начальная позиция объекта
    private Quaternion initialRotation; // Начальный поворот объекта

    void Start()
    {
        initialPosition = transform.localPosition; // Сохраняем начальную позицию объекта
        initialRotation = transform.localRotation; // Сохраняем начальный поворот объекта
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
            // Изменяем позицию объекта на короткое расстояние в обратном направлении от вектора отдачи по позиции
            transform.localPosition = Vector3.Lerp(initialPosition, initialPosition + recoilPosition, elapsedTime / recoilDuration);

            // Изменяем поворот объекта на короткое расстояние в обратном направлении от вектора отдачи по повороту
            transform.localRotation = Quaternion.Lerp(initialRotation, Quaternion.Euler(initialRotation.eulerAngles + recoilRotation), elapsedTime / recoilDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialPosition; // Возвращаем объект в начальную позицию
        transform.localRotation = initialRotation; // Возвращаем объект в начальный поворот
    }
}
