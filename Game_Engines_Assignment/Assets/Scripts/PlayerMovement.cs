using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;

    public float speed;

	// Use this for initialization
	void Start () {
        
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        controller.Move((Vector3.forward * speed ) * Time.deltaTime);
	}
}
