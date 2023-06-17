using UnityEngine;

public class SongManager : MonoBehaviour
{
    //private AudioMixerGroup outputMixerGroup;
    private int sampleSize;
    private int numBins = 12; // Количество музыкальных нот (полутонов) для определения

    private AudioSource audioSource;
    private string[] _noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    private float[] spectrum;
    private float sampleRate;
    private float binSize;
    private int[] _notes;
    private float _lastFrequency;

    private bool _firstEnnabled;

    public static SongManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _notes = new int[12];
 
        audioSource = GetComponent<AudioSource>();
        _firstEnnabled = false;
    }

    private void Update()
    {        
        if (audioSource.isPlaying)
        {
            if (!_firstEnnabled)
            {
                sampleSize = 8192;

                sampleRate = AudioSettings.outputSampleRate;
                binSize = sampleRate / sampleSize;
                
                spectrum = new float[sampleSize / 2];

                _firstEnnabled = true;
            }
        }      
    }

    public int GetData()
    {        
        if (spectrum != null)
        {
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
          
            float maxMagnitude = 0f;
            int maxIndex = 0;
            int returnIndex = 0;
            for (int i = 0; i < spectrum.Length; i++)
            {
                float magnitude = spectrum[i];
                if (magnitude > maxMagnitude)
                {
                    maxMagnitude = magnitude;
                    maxIndex = i;
                }
            }

            float frequency = maxIndex * binSize;
           
            float semitoneRatio = Mathf.Pow(2f, 1f / numBins);
            float semitoneOffset = 440f / semitoneRatio; // Нота A4 соответствует 440 Гц

            float semitoneDistance = Mathf.Log(frequency / semitoneOffset, semitoneRatio);
            int noteIndex = Mathf.RoundToInt(semitoneDistance);           
            
            int octave = 4 + noteIndex / 12; 
            if (noteIndex % 12 < _noteNames.Length && noteIndex >= 0)
            {
                string noteName = _noteNames[noteIndex % 12];
                _notes[noteIndex % 12]+=1;
                returnIndex = noteIndex % 12;
            }
            return returnIndex;
        }
        return -1;        
    }

    public int GetSpawnPosition()
    {
        return GetData()/4;
    }
}
