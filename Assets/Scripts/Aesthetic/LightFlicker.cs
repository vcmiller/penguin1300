using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
    public Light light { get; private set; }
    public Material material { get; private set; }

    public Color baseColor { get; private set; }
    
    public float threshold = .2f;
    public ExpirationTimer flickerExpiration;

	// Use this for initialization
	void Start () {
        light = GetComponentInChildren<Light>();
        material = GetComponent<MeshRenderer>().material;
        baseColor = material.GetColor("_EmissionColor");
	}

    void FixedUpdate() {
        if (Random.value < threshold) {
            flickerExpiration.Set();
        }
    }
	
	// Update is called once per frame
	void Update () {

	    if (flickerExpiration.Expired) {
            light.enabled = true;
            material.SetColor("_EmissionColor", baseColor);
        } else {
            light.enabled = false;
            material.SetColor("_EmissionColor", Color.black);
        }
	}
}
