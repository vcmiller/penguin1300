using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PenguinSlotSaver))]
public class PenguinSlot : MonoBehaviour {
    public DraggableObject resident { get; set; }

    public Vector3 offset;
    public float radius = 0.5f;
    public float power {
        get {
            return (!resident) ? 0 : 1 ;
        }
    }

	// Use this for initialization
	void Start () {
        resident = null;
	}
	
	// Update is called once per frame
	void Update () {
	    if (resident && resident.held) {
            resident = null;
        }

        if(!resident) {
            DraggableObject d = getPotentialResident();
            if (d) {
                resident = d;
            }
        } else {
            resident.rigidbody.MoveRotation(transform.rotation);
            resident.rigidbody.MovePosition(transform.TransformPoint(offset));
            resident.rigidbody.velocity = Vector3.zero;
            resident.rigidbody.angularVelocity = Vector3.zero;
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
