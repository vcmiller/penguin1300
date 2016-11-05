using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
    protected SteamVR_TrackedObject controller;
    protected SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    public void SetActive(bool b, Transform hand) {
        enabled = b;
        GetComponent<Rigidbody>().isKinematic = b;
        transform.parent = hand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (hand) {
            controller = hand.GetComponent<SteamVR_TrackedObject>();
        } else {
            controller = null;
        }
    }
}
