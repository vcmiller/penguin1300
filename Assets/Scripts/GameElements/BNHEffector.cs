using UnityEngine;
using System.Collections;

public class BNHEffector : MonoBehaviour {
    public float force = 10;

    public bool isImpulse = false;

	void OnTriggerStay(Collider other) {
        if (!isImpulse && PausePlayManager.instance.running) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(transform.forward * force, ForceMode.Acceleration);
            }
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (isImpulse && PausePlayManager.instance.running) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(transform.forward * force, ForceMode.Impulse);
            }
        }
    }
}
