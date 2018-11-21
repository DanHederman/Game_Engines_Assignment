using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {


    public GameObject Dot;
    public float Degree, C;
    public int N;
    public float DotScale;

    private Vector2 CalcPhyllotaxis(float Deg, float Scale, int Count)
    {
        double angle = Count * (Deg * Mathf.Deg2Rad);
        float r = Scale * Mathf.Sqrt(Count);
        float x = r * (float)System.Math.Cos(angle);
        float y = r * (float)System.Math.Sin(angle);
        Vector2 Vec2 = new Vector2(x, y);
        return Vec2;
    }

    private Vector2 PhyllotaxisPosition;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            PhyllotaxisPosition = CalcPhyllotaxis(Degree, C, N);
            GameObject DotInstance = (GameObject)Instantiate(Dot);
            DotInstance.transform.position = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            DotInstance.transform.localScale = new Vector3(DotScale, DotScale, DotScale);
            N++;
        }
	}
}
