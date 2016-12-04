using UnityEngine;
using System.Collections;

public class Hand : Tool {

    public Interactable currentInteraction { get; private set; }

    public Transform fingers { get; private set; }
    public Transform thumb { get; private set; }

    public float fingersOpenAngle = -180;
    public float fingersClosedAngle = -150;

    public float thumbOpenAngle = 150;
    public float thumbClosedAngle = 170;

    public float pickupRange = 0.1f;

    public bool inHand() {
        return currentInteraction != null;
    }

    void Start() {
        Transform root = transform.GetChild(0).GetChild(0);
        fingers = root.GetChild(0);
        thumb = root.GetChild(1);
    }

    public Interactable getInteractible() {
        foreach (Collider col in Physics.OverlapSphere(transform.position, .1f)) {
            Interactable i = col.transform.root.GetComponent<Interactable>();
            if (i) {
                return i;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update () {
        UpdateFingers();
        if (PausePlayManager.instance.running) {
            if (currentInteraction) {
                currentInteraction.EndInteraction();
                currentInteraction = null;
            }

            return;
        }

        if (controller.input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !currentInteraction) {
            Interactable i = getInteractible();
            if (i && i.BeginInteraction(controller, 0)) {
                currentInteraction = i;
            }
        }

        if (controller.input.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && !currentInteraction) {
            Interactable i = getInteractible();
            if (i && i.BeginInteraction(controller, 1)) {
                currentInteraction = i;
            }
        }

        if ((controller.input.GetPressUp(SteamVR_Controller.ButtonMask.Grip) || controller.input.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) && currentInteraction) {
            currentInteraction.EndInteraction();
            currentInteraction = null;
        }
    }

    private void UpdateFingers() {


        float f = controller.input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;

        if (controller.input.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
            f = 1.0f;
        }

        thumb.localEulerAngles = new Vector3(0, -90, Mathf.LerpAngle(thumbOpenAngle, thumbClosedAngle, f));
        fingers.localEulerAngles = new Vector3(0, -90, Mathf.LerpAngle(fingersOpenAngle, fingersClosedAngle, f));
    }

    protected override void OnActiveChange(bool active) {
        if (!active && currentInteraction) {
            currentInteraction.EndInteraction();
            currentInteraction = null;
        }
    }
}
