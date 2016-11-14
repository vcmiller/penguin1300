using UnityEngine;
using System.Collections;

public class Axle : MonoBehaviour {
    public WieldableObject axis { get; private set; }
    public HingeJoint joint { get; private set; }

    public float power {
        get {
            return axis.rigidbody.angularVelocity.magnitude / axis.rigidbody.maxAngularVelocity;
        }
    }
    public float maxRot = 420.0f;

	// Use this for initialization
	void Start () {
        axis.rigidbody.maxAngularVelocity = maxRot;
        axis = transform.GetChild(0).GetComponent<WieldableObject>();
        joint = axis.GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
