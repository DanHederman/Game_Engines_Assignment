using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class AudioPeer : MonoBehaviour
{

    AudioSource AudioSource;

    //Mic Input
    public bool UseMicrophone;
    public AudioClip AudioClip;
    public string SelectedDevice;
    public AudioMixerGroup MixerGroupMic, MixerGroupMaster;

    private readonly float[] SamplesLeft = new float[512];
    private readonly float[] SamplesRight = new float[512];

    private readonly float[] FreqBand = new float[8];

    private readonly float[] _bandBuffer = new float[8];

    private readonly float[] BufferDecrease = new float[8];

    private readonly float[] FreqBandHighest = new float[8];


    [HideInInspector]
    public float[] AudioBand, AudioBandBuffer;

    [HideInInspector]
    public float Amplitude, AmplitudeBuffer;
    public float AmplitideHighest;


    public float _AudioProfile;


    public enum Channel
    {
        Stereo,
        Left,
        Right
    };

    public Channel channel = new Channel();

    float[] _freqBand64 = new float[64];
    float[] _bandBuffer64 = new float[64];
    float[] _bufferDecrease64 = new float[64];
    float[] _freqBandHighest64 = new float[64];

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;



    // Use this for initialization
    void Start()
    {

        AudioBand = new float[8];
        AudioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
        AudioSource = GetComponent<AudioSource>();
        AudioProfile(_AudioProfile);

        if (UseMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                SelectedDevice = Microphone.devices[0].ToString();
                AudioSource.outputAudioMixerGroup = MixerGroupMic;
                AudioSource.clip = Microphone.Start(SelectedDevice, true, 10, AudioSettings.outputSampleRate);
            }
            else
            {
                AudioSource.outputAudioMixerGroup = MixerGroupMaster;
                UseMicrophone = false;
            }
        }
        else
        {
            AudioSource.clip = AudioClip;
        }

        AudioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (AudioSource.clip == null) return;
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetSpectrumAudioSource()
    {
        AudioSource.GetSpectrumData(SamplesLeft, 0, FFTWindow.Blackman);
        AudioSource.GetSpectrumData(SamplesRight, 1, FFTWindow.Blackman);
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
                        average += SamplesLeft[count] + SamplesRight[count] * (count + 1);
                        break;
                    case Channel.Right:
                        average += SamplesRight[count] * (count + 1);
                        break;
                    case Channel.Left:
                        average += SamplesLeft[count] * (count + 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                count++;
            }
            average /= count;

            FreqBand[i] = average * 10;
        }
    }

    private void BandBuffer()
    {
        for (var g = 0; g < 8; ++g)
        {
            if (FreqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = FreqBand[g];
                BufferDecrease[g] = 0.005f;
            }

            if (!(FreqBand[g] < _bandBuffer[g])) continue;
            _bandBuffer[g] -= BufferDecrease[g];
            BufferDecrease[g] *= 1.2f;
        }
    }

    public void CreateAudioBands()
    {
        for (var i = 0; i < 8; i++)
        {
            if (FreqBand[i] > FreqBandHighest[i])
            {
                FreqBandHighest[i] = FreqBand[i];

            }

            AudioBand[i] = (FreqBand[i] / FreqBandHighest[i]);
            _bandBuffer[i] = (_bandBuffer[i] / FreqBandHighest[i]);
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
            FreqBandHighest[i] = 0;

        }
    }
}