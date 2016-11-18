using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodySaver))]
[RequireComponent(typeof(TransformSaver))]
public class PhysicsObject : Interactable {
    public Rigidbody rigidbody { get; private set; }
    public bool held { get; protected set; }

    public float maxVelocity = 10;

    protected int currentButton { get; set; }
    
    public bool canPickup = true;

    public Controller interactor { get; protected set; }

    public Quaternion localPickupRotation { get; protected set; }
    public Vector3 localPickupPosition { get; protected set; }

    // Use this for initialization
    public virtual void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        held = false;

        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public virtual void FixedUpdate() {
        if (rigidbody.velocity.magnitude > maxVelocity) {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }

        if (held && interactor) {
            transform.position = (interactor.transform.TransformPoint(localPickupPosition));
            transform.rotation = (interactor.transform.rotation * localPickupRotation);
        }
    }

    public override bool BeginInteraction(Controller hand, int button) {
        if (canPickup && !held) {
            currentButton = button;
            interactor = hand;
            Pickup(currentButton);

            localPickupPosition = hand.transform.InverseTransformPoint(transform.position);
            localPickupRotation = Quaternion.Inverse(hand.transform.rotation) * transform.rotation;

            return true;
        } else {
            return false;
        }
    }

    public override void EndInteraction() {
        if (held) {
            Drop(currentButton);
            interactor = null;
        }
    }

    public virtual void Pickup(int button) {
        held = true;
    }

    public virtual void Drop(int button) {
        held = false;
    }
}
