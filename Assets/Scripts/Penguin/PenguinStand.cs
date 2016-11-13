using UnityEngine;
using System.Collections;

public class PenguinStand : MonoBehaviour {

    const float hella = 6.5f;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (GetComponent<Rigidbody>().useGravity && Physics.Raycast(transform.position, Vector3.down, 2f)) {
           // transform.Rotate(Vector3.Cross(transform.up, Vector3.up), Mathf.Min(Time.deltaTime * hella, Vector3.Angle(transform.up, Vector3.up)), Space.World);
            GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * hella, transform.position + transform.up, ForceMode.Acceleration);
        }
 	}
}
