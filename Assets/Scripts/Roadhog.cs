using UnityEngine;
using System.Collections;

public class Roadhog : Tool {
    private Transform hook;
    private Vector3 hookOffset;
    private bool pulling = false;

    private Rigidbody hookedObject;

    public override void Start() {
        base.Start();

        hook = transform.GetChild(0);
        hookOffset = hook.transform.localPosition;
    }

    void Update() {
        Vector3 v = transform.TransformPoint(hookOffset);

        LineRenderer rope = transform.FindChild("Rope").GetComponent<LineRenderer>();
        rope.SetPosition(0, v);
        rope.SetPosition(1, hook.transform.position);


        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            RaycastHit hit;
            
            if (Physics.Raycast(v, transform.forward, out hit)) {
                if (hit.collider.CompareTag("PickupObject")) {
                    hookedObject = hit.transform.GetComponent<Rigidbody>();

                    if (!pulling) {
                        hook.transform.parent = hit.transform;
                        hook.transform.forward = -hit.normal;
                        hook.transform.position = hit.point;
                        pulling = true;
                    }
                }
            }
        }

        if (input.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            hookedObject = null;
        }
        

        if (hookedObject) {
            hookedObject.velocity = (transform.position - hookedObject.transform.position);
        } else {
            pulling = false;
            hook.transform.parent = transform;
            hook.transform.localPosition = hookOffset;
            hook.transform.localRotation = Quaternion.identity;
        }
    }
}
