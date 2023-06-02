using UnityEngine;

public class TrackDataCollector : MonoBehaviour
{
    private AudioSource _musicSource;

    private float[] _spectrumDataForMaxAmplitude;
    private float[] _groupAmplitudes;
    private float[] _spectrumData;
    private int _groupsize;

    public static TrackDataCollector Instance;

    private void Awake()
    {
        Instance = this;
        _musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _groupsize = 4;     
    }

    public float GetMusicData()
    {
        _spectrumData = new float[8192];
        _groupAmplitudes = new float[4];

        _musicSource.GetSpectrumData(_spectrumData, 0, FFTWindow.BlackmanHarris);

        int groupSize = _spectrumData.Length / _groupsize;

        for (int i = 0; i < _spectrumData.Length; i++)
        {
            int groupIndexx = Mathf.FloorToInt(i / groupSize);
            _groupAmplitudes[groupIndexx] += _spectrumData[i];
        }

        int currentPosition = (int)(_musicSource.timeSamples * _musicSource.clip.channels);
        int groupIndex = Mathf.FloorToInt(currentPosition / _musicSource.clip.samples / 4);
        float currentAmplitude = _spectrumData[groupIndex];
        return currentAmplitude * 10000;
    }

    public float GetMaxAmplitude()
    {
        _spectrumDataForMaxAmplitude = new float[8192];

        _musicSource.GetSpectrumData(_spectrumDataForMaxAmplitude, 0, FFTWindow.BlackmanHarris);

        float maxAmplitude = 0.0f;

        for (int i = 0; i < _spectrumDataForMaxAmplitude.Length; i++)
        {
            float amplitude = Mathf.Abs(_spectrumDataForMaxAmplitude[i]);
            if (amplitude > maxAmplitude)
            {
                maxAmplitude = amplitude;
            }
        }
        return maxAmplitude;
    }

    public float GetAverageWaveAmplitude()
    {
        float maxAmplitude = 0f;
        float[] audioData = new float[_musicSource.clip.samples * _musicSource.clip.channels];

        _musicSource.clip.GetData(audioData, 0);

        for (int i = 0; i < audioData.Length; i++)
        {
            float amplitude = Mathf.Abs(audioData[i]);
            if (amplitude > maxAmplitude)
            {
                maxAmplitude = amplitude;
            }
        }

        return maxAmplitude / audioData.Length;
    }

    public float[] GetBounds(int boundsAmount)
    {
        float[] bounds = new float[boundsAmount];
        float max = GetAverageWaveAmplitude();
        max *= 10000;
        int divider = bounds.Length / 2;
        for (int i = 0; i < bounds.Length; i++)
        {
            bounds[i] = max * (i + 1) / divider;
        }
        return bounds;
    }
}
