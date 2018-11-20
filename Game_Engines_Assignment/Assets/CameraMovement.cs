using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float mspeed;
    public float shiftAdd;
    public float maxShift;
    public float camSens;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Mouse Control

        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;


        //Keyboard Control

        float f = 0.0f;

        Vector3 p = GetKeyboardInput();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;

            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.05f, 1f, 1000f);
            p = p * mspeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPos = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(p);
            newPos.x = transform.position.x;
            newPos.y = transform.position.z;
            transform.position = newPos;
        }

        else
        {
            transform.Translate(p);
        }

    }



    private Vector3 GetKeyboardInput()
    {

        Vector3 p_Velocity = new Vector3();

        if (Input.GetKey(KeyCode.W)){
            p_Velocity += new Vector3(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
