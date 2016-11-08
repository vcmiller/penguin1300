using UnityEngine;
using System.Collections;

public class PulseGun : Tool {
    

    public int pulsesLeft = int.MaxValue;
    public Vector3 pulseOffset;
    public float force;
    
    void Update() {
        Vector3 v = transform.TransformPoint(pulseOffset);


        if (pulsesLeft-- > 0 && input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            RaycastHit hit;
            
            if (Physics.Raycast(v, transform.forward, out hit)) {
                if (hit.collider.CompareTag("PickupObject")) {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(force * -hit.normal, hit.point);
                }
            }
        }        
    }
}
