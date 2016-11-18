using UnityEngine;
using System.Collections.Generic;

public class WieldableObject : PhysicsObject {
    private List<FixedJoint> wields;
    private List<WieldableObject> wieldedObjects;
    private List<WieldableObject> overlapping;

    public Color wieldTargetColor = Color.green * 0.2f;

    protected Material material { get; private set; }

	// Use this for initialization
	public override void Awake () {
        base.Awake();
        wields = new List<FixedJoint>();
        wieldedObjects = new List<WieldableObject>();
        overlapping = new List<WieldableObject>();
        
        material = GetComponent<MeshRenderer>().material;
    }

    private void CreateTriggers() {
        foreach (Collider c in GetComponents<Collider>()) {
            System.Type type = c.GetType();
            Collider copy = (Collider)gameObject.AddComponent(type);

            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields) {
                field.SetValue(copy, field.GetValue(c));
            }

            if (copy is MeshCollider) {
                MeshCollider mc = (MeshCollider)copy;
                mc.convex = true;
            }

            copy.isTrigger = true;
        }
    }

    private void DestroyTriggers() {

        foreach (Collider c in GetComponents<Collider>()) {
            if (c.isTrigger) {
                Destroy(c);
            }
        }
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

    void OnTriggerEnter(Collider col) {
        WieldableObject obj = col.GetComponent<WieldableObject>();
        if (obj && !overlapping.Contains(obj)) {
            overlapping.Add(obj);
            print("Enter");
        }
    }

    void OnTriggerExit(Collider col) {
        WieldableObject obj = col.GetComponent<WieldableObject>();
        if (obj) {
            obj.material.SetColor("_EmissionColor", Color.black);
            overlapping.Remove(obj);
            print("Exit");
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

    void PropagatePickup(int button, bool isHeld) {
        if (held != isHeld) {
            
            held = isHeld;

            if (held) {
                CreateTriggers();
            } else {
                DestroyTriggers();
            }

            foreach (WieldableObject obj in GetComponentsInChildren<WieldableObject>()) {
                if (obj != this) {
                    if (isHeld) {
                        obj.Pickup(button);
                    } else {
                        obj.Drop(button);
                    }
                }
            }
            
            Controller interactor = transform.root.GetComponent<PhysicsObject>().interactor;
            
            foreach (WieldableObject obj in wieldedObjects) {
                WieldableObject obj2 = obj.transform.root.GetComponent<WieldableObject>();
                if (obj2 && obj2.canPickup) {
                    obj2.currentButton = button;

                    if (interactor) {
                        obj2.localPickupPosition = interactor.transform.InverseTransformPoint(obj2.transform.position);
                        obj2.localPickupRotation = Quaternion.Inverse(interactor.transform.rotation) * obj2.transform.rotation;
                    }

                    obj2.interactor = interactor;

                    obj2.PropagatePickup(button, isHeld);
                }
            }
        }
    }

    public override void Pickup(int button) {
        if (button == 0) {
            Disconnect();
        }

        PropagatePickup(button, true);
    }

    public override void Drop(int button) {
        PropagatePickup(button, false);

        if (!PausePlayManager.instance.running) {
            
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
        }

        foreach (WieldableObject obj in overlapping) {

            obj.material.SetColor("_EmissionColor", Color.black);
        }

        overlapping.Clear();

    }
}
