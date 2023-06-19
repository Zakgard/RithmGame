using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class BitAnalyzer : MonoBehaviour
{
    private int numBits; // ���������� ����� ��� ������������
    [SerializeField] private int bitsBerSec;
    [SerializeField] private float maxWidth;
    [SerializeField] private float bitSpacing; // ���������� ����� ������
    [SerializeField] private float minWidth = 0.0f; // ����������� ������ ���� // ������������ ������ ����

    public float logBase = 10f; // ������������ ������ ����

    public static float TotalLength;
    public static float TotalTime;

    private AudioSource audioSource;
    public static float _speed;
    private float[] audioData;
    private GameObject[] beatBits;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        numBits = (int)audioSource.clip.length * bitsBerSec;

        float length = audioSource.clip.length;

        // �������� ������� ��� �������� ������� �������� �����
        beatBits = new GameObject[numBits];

        // �������� � ���������������� ������� �������� �����
        for (int i = 0; i < numBits; i++)
        {
            GameObject beatBit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            beatBit.name = $"{length * ((i + 1.0f) / (float)numBits)}";
            beatBit.transform.SetParent(transform);

            beatBit.transform.localPosition = new Vector3(0.0f, 149.0f + i * bitSpacing, 0.0f);

            beatBit.transform.localScale = new Vector3(minWidth, bitSpacing, minWidth);
            beatBits[i] = beatBit;
        }

        // ��������� ����� ������ �����
        audioData = new float[audioSource.clip.samples];
        audioSource.clip.GetData(audioData, 0);
        TotalLength = bitSpacing * numBits;
        TotalTime = audioSource.clip.length;
        _speed = TotalLength / TotalTime;

        for (int i = 0; i < numBits; i++)
        {
            float t = Mathf.Clamp01((float)i / numBits); // ������������ �������� ��� ������� � ����� ������
            int sampleIndex = Mathf.FloorToInt(t * audioData.Length);

            // ���������� ������������ �������� � �������� ���������
            float amplitude = Mathf.Lerp(minWidth, maxWidth, Mathf.Abs(audioData[sampleIndex]));

            // ��������� ������ ����
            Vector3 scale = beatBits[i].transform.localScale;
            scale.x = amplitude;
            beatBits[i].transform.localScale = scale;
        }
    }
}