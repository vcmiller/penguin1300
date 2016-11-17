using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PenguinSlotSaver))]
public class PenguinSlot : MonoBehaviour {
    public DraggableObject resident { get; set; }

    public Vector3 offset;
    public float radius = 0.5f;
    public float power {
        get {
            return (resident == null) ? 0 : 1 ;
        }
    }

	// Use this for initialization
	void Start () {
        resident = null;
	}
	
	// Update is called once per frame
	void Update () {
	    if (resident && resident.held) {
            Destroy(resident.GetComponent<FixedJoint>());
            resident = null;
        }

        if(!resident) {
            DraggableObject d = getPotentialResident();
            if (d) {
                resident = d;
                resident.transform.rotation = transform.rotation;
                resident.transform.position = transform.TransformPoint(offset);

                FixedJoint joint = resident.gameObject.AddComponent<FixedJoint>();
                joint.enableCollision = false;
                joint.enablePreprocessing = false;
                joint.connectedBody = GetComponent<Rigidbody>();
            }
        }
	}

    DraggableObject getPotentialResident() {
        foreach (Collider col in Physics.OverlapSphere(transform.TransformPoint(offset), radius)) {
            DraggableObject d = col.GetComponent<DraggableObject>();
            if (d && !d.held) {
                return d;
            }
        }

        return null;
    }
}
