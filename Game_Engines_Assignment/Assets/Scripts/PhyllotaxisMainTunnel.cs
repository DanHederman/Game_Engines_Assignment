using UnityEngine;

public class PhyllotaxisMainTunnel : MonoBehaviour
{

    public AudioPeer _audioPeer;

    private float _degree = 8;
    public float Scale;
    public int NumberStart;
    public int StepSize;
    //public int MaxIteration;

    private int _number;
    //private int _currentIteration;

    private Vector2 _phyllotaxisPosition;

    public bool Repeat;
   
    private float _scaleTmr, _currentScale;


    private void Awake()
    {
        _currentScale = Scale;   
        _number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(_degree, _currentScale, _number);
    }


    private void Update()
    {
        //Spinning triangle
        if (Input.GetKeyDown("1"))
        {
            _degree = 121;
        }

        //Spinning square
        if (Input.GetKeyDown("2")){
            _degree = 91;
        }

        //5 point star
        if (Input.GetKeyDown("3"))
        {
            _degree = 145;
        }

        //Multi point spiral
        if (Input.GetKeyDown("4"))
        {
            _degree = 175;
        }

        if (Input.GetKeyDown("5"))
        {
            _degree = 220;
        }

        if (Input.GetKeyDown("6"))
        {
            _degree = 260;
        }

        if (Input.GetKeyDown("7"))
        {
            _degree = 301;   
        }

        //Circular Spiral
        if (Input.GetKeyDown("0"))
        {
            _degree = 8;
        }

        //Invert
        if (Input.GetKeyDown("i"))
        {
            _degree = -_degree;
        }

        //Call Phyllotaxis algorithm and set positions

        _phyllotaxisPosition = CalcPhyllotaxis(_degree, _currentScale, _number);
        transform.localPosition = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
        _number += StepSize;
        //_currentIteration++;
    }

    private Vector2 CalcPhyllotaxis(float deg, float Scale, int number)
    {
        double angle = number * (deg * Mathf.Deg2Rad);
        var r = Scale * Mathf.Sqrt(number);
        var x = r * (float)System.Math.Cos(angle);
        var y = r * (float)System.Math.Sin(angle);
        var Vec2 = new Vector2(x, y);
        return Vec2;
    }
}
