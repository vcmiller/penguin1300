using UnityEngine;
using System.Collections.Generic;

public class WeldableObject : PhysicsObject {
    public List<FixedJoint> welds { get; private set; }
    public List<WeldableObject> weldedObjects { get; private set; }
    public List<WeldableObject> overlapping { get; private set; }

    public Color wieldTargetColor = Color.green * 0.2f;
    public Color invalidColor = Color.red * 0.3f;

    protected Material material { get; private set; }

    protected bool readyForWeld { get; private set; }

    public bool isWeldTarget { get; private set; }
    public bool isInvalid { get; private set; }

    public AudioClip[] dropClips;

    public Color emissionColor {
        get {
            if (isInvalid) {
                return invalidColor;
            } else if (isWeldTarget) {
                return wieldTargetColor;
            } else {
                return Color.black;
            }
        }
    }

	// Use this for initialization
	public override void Awake () {
        base.Awake();
        welds = new List<FixedJoint>();
        weldedObjects = new List<WeldableObject>();
        overlapping = new List<WeldableObject>();
        
        material = GetComponent<MeshRenderer>().material;
        readyForWeld = false;
        isWeldTarget = false;
        isInvalid = false;
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
            foreach (WeldableObject obj in overlapping) {
                if (weldedObjects.IndexOf(obj) == -1) {
                    obj.isWeldTarget = true;
                }
            }
        }

        material.SetColor("_EmissionColor", emissionColor);
    }

    void OnTriggerEnter(Collider col) {
        WeldableObject obj = col.GetComponent<WeldableObject>();
        if (obj && !overlapping.Contains(obj)) {
            overlapping.Add(obj);
            print("Enter");
        }
    }

    void OnTriggerExit(Collider col) {
        WeldableObject obj = col.GetComponent<WeldableObject>();
        if (obj) {
            obj.isWeldTarget = false;
            overlapping.Remove(obj);
            print("Exit");
        }
    }

    void Disconnect() {
        for (int i = 0; i < welds.Count; i++) {
            weldedObjects[i].welds.Remove(welds[i]);
            weldedObjects[i].weldedObjects.Remove(this);
            Destroy(welds[i]);
        }

        welds.Clear();
        weldedObjects.Clear();
    }

    void PropagatePickup(int button, bool isHeld) {
        if (held != isHeld) {
            
            held = isHeld;

            if (held) {
                CreateTriggers();
            } else {
                DestroyTriggers();
                readyForWeld = true;
            }

            foreach (WeldableObject obj in GetComponentsInChildren<WeldableObject>()) {
                if (obj != this) {
                    if (isHeld) {
                        obj.Pickup(button);
                    } else {
                        obj.Drop(button);
                    }
                }
            }
            
            Controller interactor = transform.root.GetComponent<PhysicsObject>().interactor;
            
            foreach (WeldableObject obj in weldedObjects) {
                WeldableObject obj2 = obj.transform.root.GetComponent<WeldableObject>();
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

    bool PropagateWelding() {
        bool b = false;
        if (!readyForWeld) {
            return false;
        }

        readyForWeld = false;

        foreach (WeldableObject obj in GetComponentsInChildren<WeldableObject>()) {
            b |= obj.PropagateWelding();
        }

        foreach (WeldableObject obj in weldedObjects) {
            b |= obj.PropagateWelding();
        }


        if (!PausePlayManager.instance.running) {
            foreach (WeldableObject obj in overlapping) {
                if (weldedObjects.IndexOf(obj) == -1) {
                    b = true;

                    FixedJoint wield = gameObject.AddComponent<FixedJoint>();

                    welds.Add(wield);
                    obj.welds.Add(wield);
                    weldedObjects.Add(obj);
                    obj.weldedObjects.Add(this);

                    wield.connectedBody = obj.rigidbody;
                    wield.enableCollision = false;
                    wield.enablePreprocessing = false;
                }
            }
        }



        foreach (WeldableObject obj in overlapping) {
            obj.isWeldTarget = false;
        }

        return b;
    }

    public override void Pickup(int button) {
        if (button == 0) {
            Disconnect();
        }

        PropagatePickup(button, true);
    }

    public override void Drop(int button) {
        PropagatePickup(button, false);
        if (PropagateWelding()) {
            if (dropClips.Length > 0) {
                AudioSource.PlayClipAtPoint(dropClips[Random.Range(0, dropClips.Length)], transform.position);
            }
        }

        overlapping.Clear();


    }
}
