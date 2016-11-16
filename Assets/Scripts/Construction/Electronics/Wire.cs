using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Wire : MonoBehaviour {
    public Link link { get; private set; }
    public LineRenderer line { get; private set; }

	// Use this for initialization
	void Start () {
        link = GetComponent<Link>();
        line = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (link.ip && link.op) {
            line.SetPosition(0, link.ip.transform.position);
            line.SetPosition(1, link.op.transform.position);
        }
	}
}
