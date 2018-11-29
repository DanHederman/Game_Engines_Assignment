using UnityEngine;

public class PhylloGeneratedTunnel : MonoBehaviour {

    public Transform Tunnel;
    public AudioPeer AudioPeer;
    public float SpeedOfTunnel, DistanceOfCamera;
	
	// Update is called once per frame
    private void Update () {

        Tunnel.position = new Vector3(Tunnel.position.x, Tunnel.position.y, Tunnel.position.z + (AudioPeer.Amplitude * SpeedOfTunnel));
        transform.position = new Vector3(transform.position.x, transform.position.y, Tunnel.position.z + DistanceOfCamera);
	}
}
