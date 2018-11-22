using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {


    //public GameObject Dot;
    public float Degree, Scale;
    public int NumberStart;
    private int Number;

    public int StepSize;
    public int MaxIteration;
    public int CurrentIteration;
    //Lerp
    public bool UseLerp;
    public float IntLerp;
    private bool IsLerping;


    public Vector3 StartPos, EndPos;
    private float TimeLerpStart;

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
        Number = NumberStart;
        transform.localPosition = CalcPhyllotaxis(Degree, Scale, Number);

        if (UseLerp)
        {
            StartLerp();
        }
    }

    void StartLerp()
    {
        IsLerping = true;
        TimeLerpStart = Time.time;
        PhyllotaxisPosition = CalcPhyllotaxis(Degree, Scale, Number);
        StartPos = this.transform.position;
        EndPos = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);

    }

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
