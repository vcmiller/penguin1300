using UnityEngine;
using System.Collections;

public class Axle : MonoBehaviour {
    public PhysicsObject axis { get; private set; }
    public HingeJoint joint { get; private set; }

    public float power {
        get {
            Rigidbody body = GetComponent<Rigidbody>();
            if (body) {
                return (axis.rigidbody.angularVelocity - body.angularVelocity).magnitude / maxRot;
            } else {
                return axis.rigidbody.angularVelocity.magnitude / maxRot;
            }
        }
    }

    public float maxRot = 420.0f;

	// Use this for initialization
	void Start () {
        axis = transform.GetChild(0).GetComponent<PhysicsObject>();
        joint = axis.GetComponent<HingeJoint>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}


}
