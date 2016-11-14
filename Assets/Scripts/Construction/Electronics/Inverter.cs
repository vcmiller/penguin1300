using UnityEngine;
using System.Collections;

public class Inverter : MonoBehaviour {

    public InputPort ip { private set; get; }
    public OutputPort op { private set; get; }

    public float period;
    public float duty;

	// Use this for initialization
	void Start () {
        ip = GetComponentInChildren<InputPort>();
        op = GetComponentInChildren<OutputPort>();
	}
	
	// Update is called once per frame
	void Update () {
        op.power = (Time.time % period > period * duty) ? ip.power : 0;
	}
}
