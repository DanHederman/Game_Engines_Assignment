using UnityEngine;

public class PhylloGeneratedTunnel : MonoBehaviour {

    public Transform Tunnel;
    public AudioPeer AudioPeer;
    public float SpeedOfTunnel, DistanceOfCamera;

    private bool control;

    public void Start()
    {
        control = false;
    }



    // Update is called once per frame
    private void Update () {


        if (Input.GetKey(KeyCode.C))
        {
            control = true;
        }
        if (Input.GetKey(KeyCode.X))
        {
            control = false;
        }
        
        if(control == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Tunnel.position = new Vector3(Tunnel.position.x, Tunnel.position.y + .5f, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
                transform.position = new Vector3(transform.position.x, transform.position.y + .5f, Tunnel.position.z + DistanceOfCamera);
            }

            else if (Input.GetKey(KeyCode.S))
            {
                Tunnel.position = new Vector3(Tunnel.position.x, Tunnel.position.y - .5f, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
                transform.position = new Vector3(transform.position.x, transform.position.y - .5f, Tunnel.position.z + DistanceOfCamera);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                Tunnel.position = new Vector3(Tunnel.position.x - .5f, Tunnel.position.y, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
                transform.position = new Vector3(transform.position.x - .5f, transform.position.y, Tunnel.position.z + DistanceOfCamera);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                Tunnel.position = new Vector3(Tunnel.position.x + .5f, Tunnel.position.y, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
                transform.position = new Vector3(transform.position.x + .5f, transform.position.y, Tunnel.position.z + DistanceOfCamera);
            }

            else
            {
                Tunnel.position = new Vector3(Tunnel.position.x, Tunnel.position.y, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
                transform.position = new Vector3(transform.position.x, transform.position.y, Tunnel.position.z + DistanceOfCamera);
            }
        }
        else
        {
            Tunnel.position = new Vector3(Tunnel.position.x, Tunnel.position.y, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
            transform.position = new Vector3(transform.position.x, transform.position.y, Tunnel.position.z + DistanceOfCamera);
        }
    }
}
