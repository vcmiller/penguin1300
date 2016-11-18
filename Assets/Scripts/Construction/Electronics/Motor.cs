﻿using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

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
        if (!Mathf.Approximately(ip.power, 0)) {
            ax.joint.useMotor = true;

            JointMotor motor = ax.joint.motor;
            motor.force = forcePerFlap * ip.power;
            motor.freeSpin = freeSpin;
            motor.targetVelocity = velocityPerFlap * ip.power;
            ax.joint.motor = motor;
        } else {
            ax.joint.useMotor = false;
        }
    }
}