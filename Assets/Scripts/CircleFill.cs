using UnityEngine;
using UnityEngine.UI;

public class CircleFill : MonoBehaviour
{
    public Material circleMaterial; // Материал с шейдером для отрисовки круга

    private float fillDuration = 3f; // Длительность анимации в секундах
    private float timer = 0f; // Таймер для отслеживания времени анимации

    private void Update()
    {
        timer += Time.deltaTime; // Увеличиваем таймер на прошедшее время

        // Рассчитываем заполнение круга в зависимости от прошедшего времени
        float fillAmount = timer / fillDuration;

        // Устанавливаем заполнение круга в шейдере
        circleMaterial.SetFloat("_FillAmount", fillAmount);

        // Проверяем, достигли ли конца анимации
        if (timer >= fillDuration)
        {
            // Анимация завершена, выполняем необходимые действия
            // ...

            // Останавливаем выполнение данного скрипта
            enabled = false;
        }
    }
}
