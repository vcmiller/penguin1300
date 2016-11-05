using UnityEngine;
using System.Collections;

public class ToolsOfTheTrade : MonoBehaviour {
    private SteamVR_TrackedObject controller;
    private SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    ControllerGrabbyShit cgs;

    delegate bool Tool();
    Tool[] tools;

    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedObject>();
        cgs = GetComponent<ControllerGrabbyShit>();

        tools = new Tool[] { hand, grapple, pulse };
    }

    // Update is called once per frame
    void Update () {
        if (cgs.inHand()) {

        } else {

        }
	}

    bool hand() {


        return true;
    }
    bool grapple() {
        if(input.GetPress((SteamVR_Controller.ButtonMask.Trigger))) {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (hit.collider.tag == "PickupObject") {
                    hit.transform.GetComponent<Rigidbody>().velocity = (transform.position - hit.transform.position);
                }
            }
        }
        return true;
    }
    bool pulse() {
        if (input.GetPressDown((SteamVR_Controller.ButtonMask.Grip))) {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (hit.collider.tag == "PickupObject") {
                    hit.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 30, ForceMode.Impulse);
                }
            }
        }
        return true;
    }
}
