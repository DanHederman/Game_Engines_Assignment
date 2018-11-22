using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class AudioPeer : MonoBehaviour {

    AudioSource _audioSource;

    public static float[] _samplesLeft = new float[512];
    public static float[] _samplesRight = new float[512];

    float[] _freqBand = new float[8];

    float[] _bandBuffer = new float[8];

    float[] _bufferDecrase = new float[8];



    float[] _frequencyBandHighest = new float[8];
    public float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float _Amplitude, _AmplitudeBuffer;
    private float _AmplitideHighest;
    public float _AudioProfile;


    public enum _channel{ STEREO,
    LEFT,
    RIGHT};

    public _channel channel = new _channel();

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_AudioProfile);
	}
	
	// Update is called once per frame
	void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
	}

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i < 8; i++)
        {

            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++)
            {
                if(channel == _channel.STEREO)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }

                if (channel == _channel.RIGHT)
                {
                    average +=  _samplesRight[count] * (count + 1);
                }
                if (channel == _channel.LEFT)
                {
                    average += _samplesLeft[count] * (count + 1);
                }
                
                count++;
            }
            average /= count;

            _freqBand[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for(int g = 0; g < 8; ++g)
        {
            if(_freqBand [g] > _bandBuffer[g]){
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrase[g] = 0.005f;
            }

            if(_freqBand [g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrase[g];
                _bufferDecrase[g] *= 1.2f;
            }
        }
    }

    void CreateAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if(_freqBand[i] > _frequencyBandHighest[i])
            {
                _frequencyBandHighest[i] = _freqBand[i];

            }

            _audioBand[i] = (_freqBand[i] / _frequencyBandHighest[i]);
            _bandBuffer[i] = (_bandBuffer[i] / _frequencyBandHighest[i]);
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;

        for(int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitude += _audioBandBuffer[i];
        }
        if(_CurrentAmplitude > _AmplitideHighest)
        {
            _AmplitideHighest = +_CurrentAmplitude;
        }

        _Amplitude = _CurrentAmplitude / _AmplitideHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitideHighest;
    }

    void AudioProfile(float ausioProfile)
    {
        for(int i = 0; i < 8; i++)
        {
            _frequencyBandHighest[i] = 0;

        }
    }
}
