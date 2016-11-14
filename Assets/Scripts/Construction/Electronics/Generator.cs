using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
    public Axle ax { get; private set; }
    public OutputPort op { get; private set; }

    public float maxOutput = 1;

	// Use this for initialization
	void Start () {
        ax = GetComponent<Axle>();
        op = GetComponentInChildren<OutputPort>();
	}
	
	// Update is called once per frame
	void Update () {
        op.power = maxOutput * ax.power;
	}
}
