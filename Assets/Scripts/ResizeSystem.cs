using UnityEngine;

public class ResizeSystem : MonoBehaviour
{
    public float verticalSpacingMultiplier = 1.3f; // Множитель для увеличения вертикального расстояния

    void Start()
    {
        // Получение всех дочерних объектов родительского GameObject
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        // Создание массива для хранения новых позиций объектов
        Vector3[] newPositions = new Vector3[childObjects.Length];

        // Проход по всем объектам и сохранение их текущих позиций
        for (int i = 0; i < childObjects.Length; i++)
        {
            newPositions[i] = childObjects[i].position;
        }

        // Вычисление средней вертикальной позиции всех объектов
        float averageYPosition = CalculateAverageYPosition(newPositions);

        // Проход по всем объектам и обновление вертикальной позиции с учетом увеличенного вертикального интервала
        for (int i = 1; i < newPositions.Length; i++)
        {
            float verticalDistance = newPositions[i].y - newPositions[i - 1].y;
            float newVerticalDistance = verticalDistance * verticalSpacingMultiplier;

            newPositions[i].y = newPositions[i - 1].y + newVerticalDistance;
        }

        // Обновление позиций объектов с учетом новых вертикальных интервалов
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i].position = newPositions[i];
        }
    }

    private float CalculateAverageYPosition(Vector3[] positions)
    {
        float totalY = 0f;
        for (int i = 0; i < positions.Length; i++)
        {
            totalY += positions[i].y;
        }
        return totalY / positions.Length;
    }
}
