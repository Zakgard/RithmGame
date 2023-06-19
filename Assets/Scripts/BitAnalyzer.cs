using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class BitAnalyzer : MonoBehaviour
{
    private int numBits; // Количество битов для визуализации
    [SerializeField] private int bitsBerSec;
    [SerializeField] private float maxWidth;
    [SerializeField] private float bitSpacing; // Расстояние между битами
    [SerializeField] private float minWidth = 0.0f; // Минимальная ширина бита // Максимальная ширина бита

    public float logBase = 10f; // Максимальная высота бита

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

        // Создание массива для хранения игровых объектов битов
        beatBits = new GameObject[numBits];

        // Создание и позиционирование игровых объектов битов
        for (int i = 0; i < numBits; i++)
        {
            GameObject beatBit = GameObject.CreatePrimitive(PrimitiveType.Cube);
            beatBit.name = $"{length * ((i + 1.0f) / (float)numBits)}";
            beatBit.transform.SetParent(transform);

            beatBit.transform.localPosition = new Vector3(0.0f, 149.0f + i * bitSpacing, 0.0f);

            beatBit.transform.localScale = new Vector3(minWidth, bitSpacing, minWidth);
            beatBits[i] = beatBit;
        }

        // Получение аудио данных трека
        audioData = new float[audioSource.clip.samples];
        audioSource.clip.GetData(audioData, 0);
        TotalLength = bitSpacing * numBits;
        TotalTime = audioSource.clip.length;
        _speed = TotalLength / TotalTime;

        for (int i = 0; i < numBits; i++)
        {
            float t = Mathf.Clamp01((float)i / numBits); // Нормализация значения для доступа к аудио данным
            int sampleIndex = Mathf.FloorToInt(t * audioData.Length);

            // Вычисление амплитудного значения в заданном диапазоне
            float amplitude = Mathf.Lerp(minWidth, maxWidth, Mathf.Abs(audioData[sampleIndex]));

            // Изменение ширины бита
            Vector3 scale = beatBits[i].transform.localScale;
            scale.x = amplitude;
            beatBits[i].transform.localScale = scale;
        }
    }
}