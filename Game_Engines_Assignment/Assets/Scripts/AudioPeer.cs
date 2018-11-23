using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class AudioPeer : MonoBehaviour {

    AudioSource _audioSource;

    //Mic Input
    public bool _useMicrophone;
    public AudioClip _audioClip;

    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];

    private float[] _freqBand = new float[8];

    private float[] _bandBuffer = new float[8];

    private float[] _bufferDecrase = new float[8];

    private float[] _freqBandHighest = new float[8];


    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;

    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitideHighest;


    public float _audioProfile;


    public enum _channel{ Stereo,
    Left,
    Right};

    public _channel channel = new _channel();

    float[] _freqBand64 = new float[64];
    float[] _bandBuffer64 = new float[64];
    float[] _bufferDecrease64 = new float[64];
    float[] _freqBandHighest64 = new float[64];

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;



    // Use this for initialization
    void Start () {

        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);

        if (_useMicrophone)
        {

        }
        else
        {

        }

        _audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {

        if(_audioSource.clip != null)
        {
            GetSpectrumAudioSource();
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();
            GetAmplitude();
        }
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
                if(channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }

                if (channel == _channel.Right)
                {
                    average +=  _samplesRight[count] * (count + 1);
                }
                if (channel == _channel.Left)
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
            if(_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];

            }

            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _bandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
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
            _freqBandHighest[i] = 0;

        }
    }
}
