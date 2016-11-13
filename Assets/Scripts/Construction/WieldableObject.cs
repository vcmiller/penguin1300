using UnityEngine;
using System.Collections.Generic;

public class WieldableObject : PhysicsObject {
    private List<Collider> overlapping;
    private List<WieldableObject> childWields;

    public bool printTransform = false;

	// Use this for initialization
	public override void Start () {
        base.Start();
        childWields = new List<WieldableObject>();
        overlapping = new List<Collider>();
	}

    void Update() {
        if (printTransform) {
            print(transform.parent);
        }
    }

    void OnTriggerEnter(Collider other) {
        overlapping.Add(other);
    }

    void OnTriggerExit(Collider other) {
        overlapping.Remove(other);
    }

    void Disconnect() {
        foreach (WieldableObject obj in childWields) {
            obj.transform.parent = null;
            obj.rigidbody.isKinematic = false;
        }

        transform.parent = null;
        rigidbody.isKinematic = false;
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
        base.Drop(button);

        if (button == 0) {
            foreach (Collider col in overlapping) {
                WieldableObject obj = col.GetComponent<WieldableObject>();
                if (obj) {
                    rigidbody.isKinematic = true;
                    transform.parent = obj.transform;
                    obj.childWields.Add(this);

                    break;
                }
            }
        }

        print(transform.parent);
        printTransform = true;
    }
}
