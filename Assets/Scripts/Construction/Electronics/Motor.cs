using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public InputPort ip { get; private set; }
    public Axle ax { get; private set; }

    public float forcePerFlap = 50;
    public bool freeSpin = true;
    public float velocityPerFlap = 360;

    public AudioSource source;

	// Use this for initialization
	void Start () {
        ax = GetComponent<Axle>();
        ip = GetComponentInChildren<InputPort>();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        JointMotor motor = ax.joint.motor;
        if (!Mathf.Approximately(ip.power, 0)) {
            ax.joint.useMotor = true;
            
            motor.force = forcePerFlap * ip.power;
            motor.targetVelocity = velocityPerFlap * ip.power;
            ax.joint.motor = motor;
            source.volume = Mathf.Clamp01(ip.power);
        } else {
            ax.joint.useMotor = false;
            source.volume = 0;
        }
        motor.freeSpin = freeSpin;
    }
}
