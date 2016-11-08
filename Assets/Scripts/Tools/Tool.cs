using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
    protected SteamVR_TrackedObject controller;
    protected SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    public virtual void Start() {
        enabled = false;
    }

    public void SetActive(bool b, Transform hand) {
        if (b && transform.parent) {
            return;
        }

        enabled = b;
        GetComponent<Rigidbody>().isKinematic = b;
        foreach (Collider col in GetComponents<Collider>()) {
            col.enabled = !b;
        }
        transform.parent = hand;

        if (b) {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            controller = hand.GetComponent<SteamVR_TrackedObject>();
        } else {
            controller = null;
        }
    }
}
