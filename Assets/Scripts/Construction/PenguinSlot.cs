using UnityEngine;
using System.Collections;

public class PenguinSlot : MonoBehaviour {
    public DraggableObject resident { get; private set; }

    public Vector3 offset;
    public float radius = 0.5f;

	// Use this for initialization
	void Start () {
        resident = null;
	}
	
	// Update is called once per frame
	void Update () {
	    if (resident && resident.held) {
            Destroy(resident.GetComponent<FixedJoint>());
            resident.collider.isTrigger = false;
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
                joint.connectedBody = GetComponent<Rigidbody>();
                resident.collider.isTrigger = true;
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
