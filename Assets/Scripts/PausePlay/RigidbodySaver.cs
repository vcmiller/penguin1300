using UnityEngine;
using System.Collections;
using System;

public class RigidbodySaver : StatusSaver {
    public PhysicsObject physicsObject { get; private set; }
    
    public bool kinematic { get; private set; }

    public bool modifyCollidersAndKinematic = true;

    int startLayer;

	// Use this for initialization
	void Awake () {
        physicsObject = GetComponent<PhysicsObject>();
        kinematic = GetComponent<Rigidbody>().isKinematic;

        startLayer = gameObject.layer;
	}

    public override void Save() {
        physicsObject.rigidbody.velocity = Vector3.zero;
        physicsObject.rigidbody.angularVelocity = Vector3.zero;

        foreach (Collider collider in GetComponents<Collider>()) {
            gameObject.layer = startLayer;
        }

        if (modifyCollidersAndKinematic) {
            //physicsObject.rigidbody.constraints = constraints;
            physicsObject.rigidbody.isKinematic = false;

        }
    }

    public override void Load() {
        physicsObject.rigidbody.velocity = Vector3.zero;
        physicsObject.rigidbody.angularVelocity = Vector3.zero;

        if (!GetComponent<DraggableObject>()) {
            foreach (Collider collider in GetComponents<Collider>()) {
                gameObject.layer = LayerMask.NameToLayer("PauseMode");
            }
        }
        

        if (modifyCollidersAndKinematic) {
            //physicsObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            physicsObject.rigidbody.isKinematic = true;

        }
    }
}
