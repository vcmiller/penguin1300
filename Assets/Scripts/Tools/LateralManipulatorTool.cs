using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LateralManipulatorTool : Tool {

    Vector3 init;
    public List<Interactable> currentInteraction { get; private set; }
    public LineRenderer lineRenderer { get; private set; }
    public Light indicator { get; private set; }

    public int numPoints = 10;
    public float maxDistance;

    public float holdDistance { get; private set; }
    public Vector3 lastHoldPosition { get; private set; }

    public float moveSpeed = 1;
    public float rotateSpeed = 360;

    public float framerate = 10;
    public float texRepeat = 2;

    public float triggerOffRot;
    public float triggerOnRot;
    public Transform trigger;

    public override void Awake() {
        base.Awake();
        currentInteraction = new List<Interactable>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(numPoints);
        indicator = GetComponentInChildren<Light>();
        indicator.gameObject.SetActive(false);
        lineRenderer.enabled = false;
    }

    public void TryPickup() {
        Drop();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)) {
            lastHoldPosition = hit.point;
            holdDistance = hit.distance;

            Interactable i = hit.transform.root.GetComponent<Interactable>();
            if (i) {
                PropagateThroughWelds(i.GetComponent<WeldableObject>());
            }
        }
    }

    private void PropagateThroughWelds(WeldableObject obj) {
        if (obj && !currentInteraction.Contains(obj)) {
            currentInteraction.Add(obj);

            foreach (WeldableObject other in obj.weldedObjects) {
                PropagateThroughWelds(other.transform.root.GetComponent<WeldableObject>());
            }

            for (int i = 0; i < obj.transform.childCount; i++) {
                PropagateThroughWelds(obj.transform.GetChild(i).GetComponent<WeldableObject>());
            }
        }
    }

    private void Drop() {
        if (currentInteraction != null) {
            currentInteraction.Clear();
        }
    }

    void UpdateTrigger() {
        trigger.localEulerAngles = new Vector3(0, Mathf.Lerp(triggerOffRot, triggerOnRot, controller.input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x), 0);
    }

    // Update is called once per frame
    void Update() {
        UpdateTrigger();

        if (PausePlayManager.instance.running) {
            Drop();
            return;
        }

        if (currentInteraction.Count == 0 && controller.input.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
            TryPickup();
        }

        if (controller.input.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            Drop();
        }
        
        teleportAllowed = currentInteraction.Count == 0;

        lineRenderer.enabled = controller.input.GetPress(SteamVR_Controller.ButtonMask.Trigger);
        indicator.gameObject.SetActive(!teleportAllowed);

        indicator.transform.position = lastHoldPosition;


        float length = maxDistance;
        if (currentInteraction.Count != 0) {
            length = holdDistance;
        }

        if (currentInteraction.Count != 0) {
            Vector3 lastVector = transform.InverseTransformDirection(lastHoldPosition - transform.position).normalized;
            for (int i = 0; i < numPoints; i++) {
                lineRenderer.SetPosition(i, Vector3.Lerp(Vector3.forward, lastVector, 1.0f * i / (numPoints - 1)) * length * i / (numPoints - 1));
            }
        } else {
            for (int i = 0; i < numPoints; i++) {
                lineRenderer.SetPosition(i, Vector3.forward * length * i / (numPoints - 1));
            }
        }
        

        float f = Mathf.FloorToInt(Time.time * framerate % 4);
        f *= 0.25f;
        lineRenderer.material.mainTextureOffset = new Vector2(0, f);
        

        lineRenderer.material.mainTextureScale = new Vector2(length / texRepeat, 0.25f);
    }

    void FixedUpdate() {
        float x = controller.input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
        float y = controller.input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

        bool b = controller.input.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
        bool dragging = Mathf.Abs(y) > Mathf.Abs(x);
        bool spinning = Mathf.Abs(y) < Mathf.Abs(x);


        if (b && dragging) {
            holdDistance += y * moveSpeed * Time.fixedDeltaTime;
            holdDistance = Mathf.Clamp(holdDistance, 0, maxDistance);
        }


        Vector3 newHoldPos = lastHoldPosition;
        if (!b || dragging) {
            //newHoldPos = transform.position + transform.forward * holdDistance;
            newHoldPos = Vector3.Lerp(lastHoldPosition, transform.position + transform.forward * holdDistance, 0.15f);
        }
        Vector3 delta = (b && spinning) ? Vector3.zero : newHoldPos - lastHoldPosition;

        foreach (WeldableObject obj in currentInteraction) {
            if (!obj.transform.parent) {
                if (b && spinning) {
                    obj.transform.RotateAround(newHoldPos, Vector3.up, x * rotateSpeed * Time.fixedDeltaTime);
                } else {
                    obj.rigidbody.MovePosition(obj.rigidbody.position + delta);
                }

            }
        }

        
        lastHoldPosition = newHoldPos;
        


    }

    protected override void OnActiveChange(bool active) {
        if (lineRenderer && indicator) {
            lineRenderer.enabled = false;
            indicator.gameObject.SetActive(false);
        }
        Drop();
    }
}
