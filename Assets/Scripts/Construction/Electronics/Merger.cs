using UnityEngine;
using System.Collections;

public class Merger : MonoBehaviour {

    public InputPort[] ips { private set; get; }
    public OutputPort op { private set; get; }

	// Use this for initialization
	void Start () {
        ips = GetComponentsInChildren<InputPort>();
        op = GetComponentInChildren<OutputPort>();
	}
	
	// Update is called once per frame
	void Update () {
        op.power = 0;
        foreach(InputPort ip in ips) {
            op.power += ip.power;
        }
	}
}
