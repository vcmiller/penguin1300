using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour {
    public Link link { get; private set; }
    public LineRenderer line { get; private set; }
    public Color offColor = Color.red;
    public Color onColor = Color.blue;
    public float maxPower = 1.0f;

	// Use this for initialization
	void Start () {
        link = GetComponent<Link>();
        line = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        float f = 0.0f;

        if (link.ip && link.op) {
            line.SetPosition(0, link.ip.transform.position);
            line.SetPosition(1, link.op.transform.position);

            f = link.op.power / maxPower;

        }


        line.material.SetColor("_EmissionColor", Color.Lerp(offColor, onColor, f));

	}
}
