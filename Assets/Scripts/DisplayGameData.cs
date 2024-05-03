using UnityEngine;

public class DisplayGameData : MonoBehaviour
{
    // —сылки на текстовые пол€ дл€ отображени€ данных
    public TextMesh lapsTM; // “екстовое поле дл€ отображени€ количества кругов
    public TextMesh enemiesKilledTM; // “екстовое поле дл€ отображени€ количества убитых врагов
    public TextMesh completionTimeTM; // “екстовое поле дл€ отображени€ времени завершени€ уровн€

    void Update()
    {
        // ѕолучаем данные из GameData
        int laps = GameData.GetLaps(); // ѕолучаем количество пройденных кругов
        int enemiesKilled = GameData.GetEnemiesKilled(); // ѕолучаем количество убитых врагов
        float completionTime = GameData.GetCompletionTime(); // ѕолучаем врем€ завершени€ уровн€

        // ќбновл€ем текстовые пол€ с полученными данными
        lapsTM.text = laps.ToString(); // ќбновл€ем текстовое поле с количеством кругов
        enemiesKilledTM.text = enemiesKilled.ToString(); // ќбновл€ем текстовое поле с количеством убитых врагов
        completionTimeTM.text = completionTime.ToString("F2"); // ќбновл€ем текстовое поле с временем завершени€ уровн€, округл€€ его до двух знаков после зап€той
    }
}
