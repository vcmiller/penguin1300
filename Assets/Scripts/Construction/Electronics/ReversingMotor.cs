using UnityEngine;
using System.Collections;

public class ReversingMotor : MonoBehaviour {

    public InputPort ip { get; private set; }
    public Axle ax { get; private set; }

    public float forcePerFlap = 50;
    public bool freeSpin = true;
    public float velocityPerFlap = 360;

	// Use this for initialization
	void Start () {
        ax = GetComponent<Axle>();
        ip = GetComponentInChildren<InputPort>();
    }
	
	// Update is called once per frame
	void Update () {
        float f = -1 + ip.power * 2;

        ax.joint.useMotor = true;

        JointMotor motor = ax.joint.motor;
        motor.force = forcePerFlap * Mathf.Abs(f);
        motor.freeSpin = freeSpin;
        motor.targetVelocity = velocityPerFlap * f;
        ax.joint.motor = motor;
    }
}
