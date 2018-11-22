using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {


    //public GameObject Dot;
    public float _degree, _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;

    //Lerping
    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    public Vector3 _startPos, _endPos;

    private float _timeStartedLerping;

    private int _number;
    private int _currentIteration;

    private TrailRenderer Trail_Renderer;

    private Vector2 CalcPhyllotaxis(float Deg, float Scale, int number)
    {
        double angle = number * (Deg * Mathf.Deg2Rad);
        float r = Scale * Mathf.Sqrt(number);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        Vector2 Vec2 = new Vector2(x, y);
        return Vec2;
    }

    private Vector2 PhyllotaxisPosition;


	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        Trail_Renderer = GetComponent<TrailRenderer>();
        _number = _numberStart;
        transform.localPosition = CalcPhyllotaxis(_degree, _scale, _number);

        if (_useLerping)
        {
            StartLerp();
        }
    }

    void StartLerp()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;
        PhyllotaxisPosition = CalcPhyllotaxis(_degree, _scale, _number);
        _startPos = this.transform.position;
        _endPos = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);

    }




    /*
    private void FixedUpdate()
    {
        if (UseLerp)
        {
            float timeSinceLerp = Time.time - TimeLerpStart;
            float PercentComplete = timeSinceLerp / IntLerp;
            transform.localPosition = Vector3.Lerp(StartPos, EndPos, PercentComplete);
            if(PercentComplete >= .97f)
            {
                transform.localPosition = EndPos;
                Number += StepSize;
                CurrentIteration++;
                if(CurrentIteration <= MaxIteration)
                {
                    StartLerp();
                }
                else
                {
                    IsLerping = false;
                }
            }
        }
        else
        {
            PhyllotaxisPosition = CalcPhyllotaxis(Degree, Scale, Number);
            transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            Number += StepSize;
            CurrentIteration++;
        }
    }



    */



    /*
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            PhyllotaxisPosition = CalcPhyllotaxis(Degree, Scale, Number);
            GameObject DotInstance = (GameObject)Instantiate(Dot);
            DotInstance.transform.position = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            DotInstance.transform.localScale = new Vector3(DotScale, DotScale, DotScale);
            Number++;
        }
        
	}
    */
}
