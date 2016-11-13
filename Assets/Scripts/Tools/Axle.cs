using UnityEngine;
using System.Collections;

public class Axle : MonoBehaviour {
    public WieldableObject axis { get; private set; }
    public HingeJoint joint { get; private set; }

	// Use this for initialization
	void Start () {
        axis = transform.GetChild(0).GetComponent<WieldableObject>();
        joint = axis.GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
