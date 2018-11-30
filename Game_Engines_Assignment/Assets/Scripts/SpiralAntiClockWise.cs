using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralAntiClockWise : MonoBehaviour {

    public AudioPeer _audioPeer;

    private Material _trailMat;
    public Color TrailColour;

    //public GameObject Dot;
    private float SDegree = -6;
    public float Scale;
    public int NumberStart;
    public int StepSize;
    public int MaxIteration;

    //Lerping
    public bool UseLerping;

    private bool _isLerping;
    public Vector3 StartPos, EndPos;
    private float _lerpPosTimer, _lerpPosSpeed;
    public Vector2 LerpPosSpeedMinMax;
    public AnimationCurve LerpPosAnimCurve;
    public int LerpPosBand;

    private int _number;
    private int _currentIteration;

    private TrailRenderer _trailRenderer;

    private Vector2 CalcPhyllotaxis(float Deg, float Scale, int number)
    {
        double angle = number * (Deg * Mathf.Deg2Rad);
        var r = Scale * Mathf.Sqrt(number);
        var x = r * (float)System.Math.Cos(angle);
        var y = r * (float)System.Math.Sin(angle);
        var Vec2 = new Vector2(x, y);
        return Vec2;
    }

    private Vector2 PhyllotaxisPosition;

    private bool Forward;
    public bool Repeat;
    public bool Invert;

    //Scale
    public bool UseScaleAnim, UseScaleCurve;
    public Vector2 ScaleAnimMinMax;
    public AnimationCurve ScaleAnimCurve;
    public float ScaleAnimSpeed;
    public int ScaleBand;
    private float ScaleTmr, CurrentScale;

    public void SetLerpPos()
    {

        PhyllotaxisPosition = CalcPhyllotaxis(SDegree, CurrentScale, _number);
        StartPos = transform.localPosition;
        EndPos = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);

    }

    private void Awake()
    {
        CurrentScale = Scale;
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_tintColour", TrailColour);
        _trailRenderer.material = _trailMat;
        _number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(SDegree, CurrentScale, _number);
        Forward = true;


        if (!UseLerping) return;
        _isLerping = true;
        SetLerpPos();
    }


    private void Update()
    {

        if (UseScaleAnim)
        {
            if (UseScaleCurve)
            {
                ScaleTmr += (ScaleAnimSpeed * _audioPeer.AudioBand[ScaleBand]) * Time.deltaTime;

                if (ScaleTmr >= 1)
                {
                    ScaleTmr -= 1;
                }
                CurrentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, ScaleAnimCurve.Evaluate(ScaleTmr));
            }
            else
            {
                CurrentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, _audioPeer.AudioBand[ScaleBand]);
            }
        }
        else
        {

        }
        /*
        if (Input.GetKeyDown("1"))
        {
            SDegree = 140;
        }
        */


        if (UseLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(LerpPosSpeedMinMax.x, LerpPosSpeedMinMax.y, LerpPosAnimCurve.Evaluate(_audioPeer.AudioBand[LerpPosBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
                transform.localPosition = Vector3.Lerp(StartPos, EndPos, Mathf.Clamp01(_lerpPosTimer));
                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;

                    if (Forward)
                    {
                        _number += StepSize;
                        _currentIteration++;
                    }
                    else
                    {
                        _number -= StepSize;
                        _currentIteration--;
                    }
                    if ((_currentIteration > 0) && (_currentIteration < MaxIteration))
                    {
                        SetLerpPos();
                    }
                    //Current iter hit 0/max
                    else
                    {
                        if (Repeat)
                        {
                            if (Invert)
                            {
                                Forward = !Forward;
                                SetLerpPos();
                            }
                            else
                            {
                                _number = NumberStart;
                                _currentIteration = 0;
                                SetLerpPos();
                            }
                        }
                        else
                        {
                            _isLerping = false;
                        }
                    }

                }
            }
        }
        if (!UseLerping)
        {
            PhyllotaxisPosition = CalcPhyllotaxis(SDegree, CurrentScale, _number);
            transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            _number += StepSize;
            _currentIteration++;
        }

    }
}
