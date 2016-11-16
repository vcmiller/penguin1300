using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodySaver))]
[RequireComponent(typeof(TransformSaver))]
public class PhysicsObject : Interactable {
    public Rigidbody rigidbody { get; private set; }
    public Collider collider { get; private set; }
    public Collider[] colliders { get; private set; }
    public bool held { get; protected set; }

    public float maxVelocity = 10;

    protected int currentButton;
    
    public bool canPickup = true;

    // Use this for initialization
    public virtual void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        colliders = GetComponents<Collider>();
        held = false;
    }

    public virtual void FixedUpdate() {
        if (rigidbody.velocity.magnitude > maxVelocity) {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
    }

    public override bool BeginInteraction(Controller hand, int button) {
        if (canPickup && !held) {
            currentButton = button;

            Pickup(currentButton);
            transform.parent = hand.transform;
            return true;
        } else {
            return false;
        }
    }

    public override void EndInteraction() {
        if (held) {
            Drop(currentButton);
            transform.parent = null;
        }
    }

    public virtual void Pickup(int button) {
        held = true;
    }

    public virtual void Drop(int button) {
        held = false;
    }
}
