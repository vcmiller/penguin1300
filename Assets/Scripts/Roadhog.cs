using UnityEngine;
using System.Collections;

public class Roadhog : Tool {
    private Transform hook;
    private Vector3 hookOffset;
    private bool pulling = false;

    public override void Start() {
        base.Start();

        hook = transform.GetChild(0);
        hookOffset = hook.transform.localPosition;
    }

    void Update() {
        bool hooked = false;
        Vector3 v = transform.TransformPoint(hookOffset);

        LineRenderer rope = transform.FindChild("Rope").GetComponent<LineRenderer>();
        rope.SetPosition(0, v);
        rope.SetPosition(1, hook.transform.position);

        if (input.GetPress((SteamVR_Controller.ButtonMask.Trigger))) {
            RaycastHit hit;


            if (Physics.Raycast(v, transform.forward, out hit)) {
                if (hit.collider.CompareTag("PickupObject")) {
                    hit.transform.GetComponent<Rigidbody>().velocity = (transform.position - hit.transform.position);
                    hooked = true;

                    if (!pulling) {
                        hook.transform.parent = hit.transform;
                        hook.transform.forward = -hit.normal;
                        hook.transform.position = hit.point;
                        pulling = true;
                    }
                }
            }
        }

        if (!hooked) {
            pulling = false;
            hook.transform.parent = transform;
            hook.transform.localPosition = hookOffset;
            hook.transform.localRotation = Quaternion.identity;
        }
    }
}
