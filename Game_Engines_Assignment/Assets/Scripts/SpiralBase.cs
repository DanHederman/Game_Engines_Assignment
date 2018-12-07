using UnityEngine;

public class SpiralBase : MonoBehaviour {

    public AudioPeer _audioPeer;

    //public GameObject Dot;
    private float SDegree;
    public float Scale;
    public int NumberStart;
    public int StepSize;
    public int MaxIteration;


    public Vector3 StartPos, EndPos;


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
    public bool Invert = false;

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

        _number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(SDegree, CurrentScale, _number);
        Forward = true;
    }


    private void Update()
    {

        if(Invert == false)
        {
            SDegree = -8;
        }
        if(Invert == true)
        {
            SDegree = 8;
        }

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

        
       PhyllotaxisPosition = CalcPhyllotaxis(SDegree, CurrentScale, _number);
       transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
       _number += StepSize;
       _currentIteration++;
        

    }
}
