using UnityEngine;
using System.Collections;

public class DraggableObject : PhysicsObject {
    public Transform holder { get; private set; }
    public Vector3 attachPoint { get; private set; }
    public Quaternion attachRot { get; private set; }

    public float dragSpeed = 50;
    public bool rotates = true;
    public bool gravityWhenHeld = false;

    public Quaternion previousRot { get; private set; }
    public Quaternion currentRot { get; private set; }

    public Vector3 angularVelocity {
        get {
            return (Quaternion.Inverse(previousRot) * currentRot).eulerAngles / Time.fixedDeltaTime;
        }
    }

    public override void FixedUpdate() {
        if (held) {
            rigidbody.velocity = (holder.position - transform.TransformPoint(attachPoint)) * dragSpeed;
            if (rotates) {
                rigidbody.MoveRotation(holder.rotation * attachRot);
            }

            previousRot = currentRot;
            currentRot = transform.rotation;
        }
    }

    public override bool BeginInteraction(Controller hand, int button) {
        if (canPickup && !held) {
            attachPoint = transform.InverseTransformPoint(hand.transform.position);
            attachRot = Quaternion.Inverse(hand.transform.rotation) * transform.rotation;
            holder = hand.transform;
            Pickup(button);
            return true;
        } else {
            return false;
        }
    }

    public override void EndInteraction() {
        if (held) {
            holder = null;
            Drop(currentButton);
        }
    }

    public override void Pickup(int button) {
        held = true;

        if (!gravityWhenHeld) {
            rigidbody.useGravity = false;
        }
    }

    public override void Drop(int button) {
        held = false;
        rigidbody.useGravity = true;
        rigidbody.angularVelocity = angularVelocity;
    }
}
