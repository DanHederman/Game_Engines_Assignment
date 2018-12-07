using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTunnel : MonoBehaviour {

    GameObject Activate = GameObject.Find("MainTunnelShape");

    // Use this for initialization
    void Start () {

        Activate.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("n"))
        {
            Activate.SetActive(false);
        }
	}
}
