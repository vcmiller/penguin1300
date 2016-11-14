using UnityEngine;
using System.Collections.Generic;

public class WieldableObject : PhysicsObject {
    private List<FixedJoint> wields;
    private List<WieldableObject> wieldedObjects;
    private List<WieldableObject> overlapping;

    public Color wieldTargetColor = Color.green * 0.2f;

    protected Material material { get; private set; }

	// Use this for initialization
	public override void Start () {
        base.Start();
        wields = new List<FixedJoint>();
        wieldedObjects = new List<WieldableObject>();
        overlapping = new List<WieldableObject>();
        
        material = GetComponent<MeshRenderer>().material;
	}

    void Update() {
        if (held) {
            foreach (WieldableObject obj in overlapping) {
                if (wieldedObjects.IndexOf(obj) == -1) {
                    obj.material.SetColor("_EmissionColor", wieldTargetColor);
                }
            }
        }
    }
	
	void OnTriggerEnter(Collider other) {
        WieldableObject obj = other.GetComponent<WieldableObject>();
        if (obj && wieldedObjects.IndexOf(obj) == -1) {
            overlapping.Add(obj);
        }
    }

    void OnTriggerExit(Collider other) {
        WieldableObject obj = other.GetComponent<WieldableObject>();
        if (obj) {
            obj.material.SetColor("_EmissionColor", Color.black);
            overlapping.Remove(obj);
        }
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
        foreach (WieldableObject obj in overlapping) {
            if (wieldedObjects.IndexOf(obj) == -1) {
                FixedJoint wield = gameObject.AddComponent<FixedJoint>();

                wields.Add(wield);
                obj.wields.Add(wield);
                wieldedObjects.Add(obj);
                obj.wieldedObjects.Add(this);

                wield.connectedBody = obj.rigidbody;
                wield.enableCollision = false;
                wield.enablePreprocessing = false;
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
