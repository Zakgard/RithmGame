using UnityEngine;

public class ResizeSystem : MonoBehaviour
{
    public float verticalSpacingMultiplier = 1.3f; // ��������� ��� ���������� ������������� ����������

    void Start()
    {
        // ��������� ���� �������� �������� ������������� GameObject
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        // �������� ������� ��� �������� ����� ������� ��������
        Vector3[] newPositions = new Vector3[childObjects.Length];

        // ������ �� ���� �������� � ���������� �� ������� �������
        for (int i = 0; i < childObjects.Length; i++)
        {
            newPositions[i] = childObjects[i].position;
        }

        // ���������� ������� ������������ ������� ���� ��������
        float averageYPosition = CalculateAverageYPosition(newPositions);

        // ������ �� ���� �������� � ���������� ������������ ������� � ������ ������������ ������������� ���������
        for (int i = 1; i < newPositions.Length; i++)
        {
            float verticalDistance = newPositions[i].y - newPositions[i - 1].y;
            float newVerticalDistance = verticalDistance * verticalSpacingMultiplier;

            newPositions[i].y = newPositions[i - 1].y + newVerticalDistance;
        }

        // ���������� ������� �������� � ������ ����� ������������ ����������
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
