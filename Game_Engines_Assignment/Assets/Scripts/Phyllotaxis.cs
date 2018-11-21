using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {


    public GameObject Dot;
    public float Degree, Scale;
    public int NumberStart;
    private int Number;
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

    }

    private void FixedUpdate()
    {
        PhyllotaxisPosition = CalcPhyllotaxis(Degree, Scale, Number);
        transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
        Number++;
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
