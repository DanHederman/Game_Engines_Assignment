using UnityEngine;

public class PhyllotaxisMainTunnel : MonoBehaviour
{

    public AudioPeer _audioPeer;

    private float Degree = 8;
    public float Scale;
    public int NumberStart;
    public int StepSize;
    public int MaxIteration;

    private int _number;
    private int _currentIteration;

    private Vector2 PhyllotaxisPosition;

    public bool Repeat;
   
    private float ScaleTmr, CurrentScale;


    private void Awake()
    {
        CurrentScale = Scale;   
        _number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(Degree, CurrentScale, _number);
    }


    private void Update()
    {
        //Spinning triangle
        if (Input.GetKeyDown("1"))
        {
            Degree = 121;
        }

        //Spinning square
        if (Input.GetKeyDown("2")){
            Degree = 91;
        }


        if (Input.GetKeyDown("3"))
        {
            Degree = 145;
        }

        if (Input.GetKeyDown("4"))
        {
            Degree = 175;
        }

        if (Input.GetKeyDown("5"))
        {
            Degree = 220;
        }

        if (Input.GetKeyDown("6"))
        {
            Degree = 260;
        }

        if (Input.GetKeyDown("7"))
        {
            Degree = 301;   
        }

        //Circular Spiral
        if (Input.GetKeyDown("0"))
        {
            Degree = 8;
        }

        //Invert
        if (Input.GetKeyDown("i"))
        {
            Degree = -Degree;
        }

        //Call Phyllotaxis algorithm and set positions

        PhyllotaxisPosition = CalcPhyllotaxis(Degree, CurrentScale, _number);
        transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
        _number += StepSize;
        _currentIteration++;
    }

    private Vector2 CalcPhyllotaxis(float Deg, float Scale, int number)
    {
        double angle = number * (Deg * Mathf.Deg2Rad);
        var r = Scale * Mathf.Sqrt(number);
        var x = r * (float)System.Math.Cos(angle);
        var y = r * (float)System.Math.Sin(angle);
        var Vec2 = new Vector2(x, y);
        return Vec2;
    }
}
