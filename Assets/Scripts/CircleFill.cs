using UnityEngine;
using UnityEngine.UI;

public class CircleFill : MonoBehaviour
{
    public Material circleMaterial; // �������� � �������� ��� ��������� �����

    private float fillDuration = 3f; // ������������ �������� � ��������
    private float timer = 0f; // ������ ��� ������������ ������� ��������

    private void Update()
    {
        timer += Time.deltaTime; // ����������� ������ �� ��������� �����

        // ������������ ���������� ����� � ����������� �� ���������� �������
        float fillAmount = timer / fillDuration;

        // ������������� ���������� ����� � �������
        circleMaterial.SetFloat("_FillAmount", fillAmount);

        // ���������, �������� �� ����� ��������
        if (timer >= fillDuration)
        {
            // �������� ���������, ��������� ����������� ��������
            // ...

            // ������������� ���������� ������� �������
            enabled = false;
        }
    }
}
