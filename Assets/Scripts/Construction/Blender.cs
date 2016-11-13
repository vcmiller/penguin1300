﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PenguinSlot))]
[RequireComponent(typeof(Axle))]
public class Blender : MonoBehaviour {
    public PenguinSlot slot { get; private set; }
    public Axle axle { get; private set; }

    public float force = 50;
    public float velocity = 360;
    public bool freeSpin = true;

	// Use this for initialization
	void Start () {
        slot = GetComponent<PenguinSlot>();
        axle = GetComponent<Axle>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (slot.resident) {
            axle.joint.useMotor = true;

            JointMotor motor = axle.joint.motor;
            motor.force = force;
            motor.freeSpin = freeSpin;
            motor.targetVelocity = velocity;
            axle.joint.motor = motor;
        } else {
            axle.joint.useMotor = false;
        }
	}
}
