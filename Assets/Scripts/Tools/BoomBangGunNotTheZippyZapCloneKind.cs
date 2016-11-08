using UnityEngine;
using System.Collections;

public class BoomBangGunNotTheZippyZapCloneKind : Tool {
    
	// Update is called once per frame
	void Update () {
        if (input.GetPressDown((SteamVR_Controller.ButtonMask.Trigger))) {
            RaycastHit hit;


            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (hit.collider.CompareTag("Player")) {
                    GameObject obj = GameObject.FindGameObjectWithTag("Respawn");
                    if (obj) {
                        hit.transform.root.position = obj.transform.position - Vector3.up;
                        Destroy(obj);
                    }
                }
            }
        }
    }
}
