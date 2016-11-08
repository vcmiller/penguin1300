using UnityEngine;
using System.Collections;

public class CloneGun : Tool {
    public GameObject clonePoint;

    private GameObject currentPoint = null;
	
	// Update is called once per frame
	void Update () {
        if (input.GetPressDown((SteamVR_Controller.ButtonMask.Trigger))) {
            RaycastHit hit;


            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (currentPoint) {
                    Destroy(currentPoint);
                }

                currentPoint = Instantiate(clonePoint);
                currentPoint.transform.position = hit.point + Vector3.up;
            }
        }
    }
}
