using UnityEngine;

public class PhyllotaxisMainTunnel : MonoBehaviour
{

    public AudioPeer _audioPeer;

    private float Degree = 120;
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

        //Invert
        if (Input.GetKeyDown("i"))
        {
            Degree = -Degree;
        }

        if (Input.GetKeyDown("backspace"))
        {
            if(gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
            }
            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }
            
        }

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
