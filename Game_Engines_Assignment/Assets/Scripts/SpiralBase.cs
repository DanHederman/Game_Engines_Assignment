using UnityEngine;

public class SpiralBase : MonoBehaviour {

    public AudioPeer AudioPeer;

    //public GameObject Dot;
    private float _sDegree;
    public float Scale;
    public int NumberStart;
    public int StepSize;
    public int MaxIteration;

    public Vector3 StartPos, EndPos;

    private int _number;
    private int _currentIteration;

    private TrailRenderer _trailRenderer;

    private static Vector2 CalcPhyllotaxis(float Deg, float Scale, int number)
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
    public bool Invert = false;

    //Scale
    public bool UseScaleAnim, UseScaleCurve;
    public Vector2 ScaleAnimMinMax;
    public AnimationCurve ScaleAnimCurve;
    public float ScaleAnimSpeed;
    public int ScaleBand;
    private float _scaleTmr, _currentScale;

    private void Awake()
    {
        _currentScale = Scale;

        _number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(_sDegree, _currentScale, _number);
        Forward = true;
    }

    private void Update()
    {

        if(Invert == false)
        {
            _sDegree = -6;
        }
        if(Invert == true)
        {
            _sDegree = 6;
        }

        //Controls the behavious of the spiral
        if (UseScaleAnim)
        {
            if (UseScaleCurve)
            {
                _scaleTmr += (ScaleAnimSpeed * AudioPeer.AudioBand[ScaleBand]) * Time.deltaTime;

                if (_scaleTmr >= 1)
                {
                    _scaleTmr -= 1;
                }
                _currentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, ScaleAnimCurve.Evaluate(_scaleTmr));
            }
            else
            {
                _currentScale = Mathf.Lerp(ScaleAnimMinMax.x, ScaleAnimMinMax.y, AudioPeer.AudioBand[ScaleBand]);
            }
        }

        
       PhyllotaxisPosition = CalcPhyllotaxis(_sDegree, _currentScale, _number);
       transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
       _number += StepSize;
       _currentIteration++;
    }
}
