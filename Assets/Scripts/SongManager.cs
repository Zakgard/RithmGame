using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SongManager : MonoBehaviour
{
    private AudioMixerGroup outputMixerGroup;
    private int sampleSize;
    private int numBins = 12; // Количество музыкальных нот (полутонов) для определения

    private AudioSource audioSource;
    private float[] spectrum;
    private float sampleRate;
    private float binSize;
    private float _timer;

    private bool _firstEnnabled;

    void Start()
    {
        _timer = 0.0f;
        audioSource = GetComponent<AudioSource>();
        _firstEnnabled = false;
    }

    private void Update()
    {        
        if (audioSource.isPlaying)
        {
            _timer += Time.deltaTime;
            if (!_firstEnnabled)
            {
                sampleSize = 8192;

                sampleRate = AudioSettings.outputSampleRate;
                binSize = sampleRate / sampleSize;
                
                spectrum = new float[sampleSize / 2];

                _firstEnnabled = true;
            }
            GetData();
        }      
    }

    private void GetData()
    {        
        if (spectrum != null && _timer >= 0.5f)
        {
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

            
            float maxMagnitude = 0f;
            int maxIndex = 0;

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
            
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int octave = 4 + noteIndex / 12; 
            if (noteIndex % 12 < noteNames.Length && noteIndex >= 0)
            {
                string noteName = noteNames[noteIndex % 12];
                Debug.Log("Frequency: " + frequency + " Hz");
                Debug.Log("Note: " + noteName + octave);              
            }
            _timer = 0.0f;
        }
    }
}
