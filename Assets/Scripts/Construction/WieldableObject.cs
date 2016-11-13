using UnityEngine;
using System.Collections.Generic;

public class WieldableObject : PhysicsObject {
    private List<FixedJoint> wields;
    private List<WieldableObject> wieldedObjects;
    private List<Collider> overlapping;

	// Use this for initialization
	public override void Start () {
        base.Start();
        wields = new List<FixedJoint>();
        wieldedObjects = new List<WieldableObject>();
        overlapping = new List<Collider>();
	}
	
	void OnTriggerEnter(Collider other) {
        overlapping.Add(other);
    }

    void OnTriggerExit(Collider other) {
        overlapping.Remove(other);
    }

    void Disconnect() {
        for (int i = 0; i < wields.Count; i++) {
            wieldedObjects[i].wields.Remove(wields[i]);
            wieldedObjects[i].wieldedObjects.Remove(this);
            Destroy(wields[i]);
        }

        wields.Clear();
        wieldedObjects.Clear();
    }

    public override void Pickup(int button) {
        if (button == 0) {
            Disconnect();
        }

        base.Pickup(button);
        overlapping.Clear();

        foreach (WieldableObject obj in GetComponentsInChildren<WieldableObject>()) {
            if (obj != this) {
                obj.Pickup(button);
            }
        }
    }

    public override void Drop(int button) {
        if (button == 0) {
            foreach (Collider col in overlapping) {
                WieldableObject obj = col.GetComponent<WieldableObject>();
                if (obj) {
                    FixedJoint wield = gameObject.AddComponent<FixedJoint>();

                    wields.Add(wield);
                    obj.wields.Add(wield);
                    wieldedObjects.Add(obj);
                    obj.wieldedObjects.Add(this);

                    wield.connectedBody = obj.rigidbody;
                    wield.enableCollision = false;
                }
            }
        }

        base.Drop(button);

        foreach (WieldableObject obj in GetComponentsInChildren<WieldableObject>()) {
            if (obj != this) {
                obj.Drop(button);
            }
        }
    }
}
