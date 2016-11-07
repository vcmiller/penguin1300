using UnityEngine;
using System.Collections;

public class ControllerGrabbyShit : MonoBehaviour {
    private SteamVR_TrackedObject controller;
    private SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    private Transform currentlyHeld;
    private Transform currentlyTouching;
    private bool seanIsATool;

    private Vector3 attachPointPenguinSpace;
    private Quaternion attachRotInit;

      float pickupSpeed = 100;

    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = null;
        if (currentlyHeld) {
            rb = currentlyHeld.GetComponent<Rigidbody>();
        }

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && currentlyTouching && !currentlyHeld) {
            currentlyHeld = currentlyTouching;

            Tool t = currentlyTouching.GetComponent<Tool>();

            if (t) {
                t.SetActive(true, transform);
                seanIsATool = true;
            } else {
                attachPointPenguinSpace = currentlyHeld.InverseTransformPoint(transform.position);
                attachRotInit = Quaternion.Inverse(transform.rotation) * currentlyHeld.rotation;

                rb = currentlyHeld.GetComponent<Rigidbody>();
                currentlyHeld.GetComponent<Rigidbody>().useGravity = false;
                seanIsATool = false;
            }
            
        }

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && currentlyHeld && seanIsATool) {
            currentlyHeld.GetComponent<Tool>().SetActive(false, null);
            currentlyHeld = null;
        }
   

        if (currentlyHeld && !seanIsATool) {
            rb.velocity = (transform.position - currentlyHeld.TransformPoint(attachPointPenguinSpace)) * pickupSpeed;

            if (input.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
                rb.useGravity = true;

                currentlyHeld = null;
            }

         }
    }

    void FixedUpdate() {
        if (currentlyHeld && !seanIsATool) {
            //   rb.velocity = (transform.position - currentlyHeld.TransformPoint(attachPointPenguinSpace)) * pickupSpeed;
            currentlyHeld.GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(currentlyHeld.rotation, transform.rotation * attachRotInit, Time.fixedDeltaTime * pickupSpeed));
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.CompareTag("PickupObject") && currentlyTouching == null) {
            currentlyTouching = other.transform;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.transform == currentlyTouching) {
            currentlyTouching = null;
        }
    }

    public bool inHand() {
        return currentlyHeld != null;
    }
}
