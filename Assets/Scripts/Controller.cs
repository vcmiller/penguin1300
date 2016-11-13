using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public SteamVR_TrackedObject controller { get; private set; }
    public SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    public Interactable currentInteraction { get; private set; }
    
    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !currentInteraction) {
            Interactable i = getInteractible();
            if (i && i.BeginInteraction(this, 0)) {
                currentInteraction = i;
            }
        }

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && !currentInteraction) {
            Interactable i = getInteractible();
            if (i && i.BeginInteraction(this, 1)) {
                currentInteraction = i;
            }
        }

        if ((input.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || input.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) && currentInteraction) {
            currentInteraction.EndInteraction();
            currentInteraction = null;
        }

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                transform.root.position = hit.point;
            }
        }
    }

    public Interactable getInteractible() {
        foreach (Collider col in Physics.OverlapSphere(transform.position, .2f)) {
            Interactable i = col.GetComponent<Interactable>();
            if (i) {
                return i;
            }
        }

        return null;
    }

    public bool inHand() {
        return currentInteraction != null;
    }
}
