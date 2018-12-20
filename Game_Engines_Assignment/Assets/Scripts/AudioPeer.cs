using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;

    //Mic Input
    public bool UseMicrophone;
    public AudioClip AudioClip;
    private string SelectedDevice;
    public AudioMixerGroup MixerGroupMic, MixerGroupMaster;

    private readonly float[] _samplesLeft = new float[512];
    private readonly float[] _samplesRight = new float[512];

    private readonly float[] _freqBand = new float[8];

    private readonly float[] _bandBuffer = new float[8];

    private readonly float[] _bufferDecrease = new float[8];

    private readonly float[] _freqBandHighest = new float[8];

    [HideInInspector]
    public float[] AudioBand, AudioBandBuffer;

    [HideInInspector]
    public float Amplitude, AmplitudeBuffer;
    [HideInInspector]
    public float AmplitideHighest;

    private float _AudioProfile;

    public enum Channel
    {
        Stereo,
        Left,
        Right
    };

    public Channel channel = new Channel();

    [HideInInspector]
    public float[] AudioBand64, AudioBandBuffer64;



    // Use this for initialization
    void Start()
    {

        AudioBand = new float[8];
        AudioBandBuffer = new float[8];
        
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_AudioProfile);


        //Use the microphone for input
        if (UseMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                SelectedDevice = Microphone.devices[1].ToString();
                _audioSource.outputAudioMixerGroup = MixerGroupMic;
                _audioSource.clip = Microphone.Start(SelectedDevice, true, 240, AudioSettings.outputSampleRate);
            }
            else
            {
                _audioSource.outputAudioMixerGroup = MixerGroupMaster;
                UseMicrophone = false;
            }
        }
        else
        {
            _audioSource.clip = AudioClip;
        }

        _audioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_audioSource.clip == null) return;
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

    private void MakeFrequencyBands()
    {
        var count = 0;

        for (var i = 0; i < 8; i++)
        {

            float average = 0;
            var sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (var j = 0; j < sampleCount; j++)
            {
                switch (channel)
                {
                    case Channel.Stereo:
                        average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                        break;
                    case Channel.Right:
                        average += _samplesRight[count] * (count + 1);
                        break;
                    case Channel.Left:
                        average += _samplesLeft[count] * (count + 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                count++;
            }
            average /= count;

            _freqBand[i] = average * 10;
        }
    }

    private void BandBuffer()
    {
        for (var g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (!(_freqBand[g] < _bandBuffer[g])) continue;
            _bandBuffer[g] -= _bufferDecrease[g];
            _bufferDecrease[g] *= 1.2f;
        }
    }

    public void CreateAudioBands()
    {
        for (var i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];

            }

            AudioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _bandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    private void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (var i = 0; i < 8; i++)
        {
            currentAmplitude += AudioBand[i];
            currentAmplitude += AudioBandBuffer[i];
        }
        if (currentAmplitude > AmplitideHighest)
        {
            AmplitideHighest = +currentAmplitude;
        }

        Amplitude = currentAmplitude / AmplitideHighest;
        AmplitudeBuffer = currentAmplitudeBuffer / AmplitideHighest;
    }

    public void AudioProfile(float audioProfile)
    {
        for (var i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = 0;

        }
    }
}