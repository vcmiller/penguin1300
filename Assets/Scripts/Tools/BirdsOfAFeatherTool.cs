using UnityEngine;
using System.Collections;

public class BirdsOfAFeatherTool : Tool {

    Rigidbody ex;

    Rigidbody penguin;

    void Update() {
        
        if (input.GetPressDown((SteamVR_Controller.ButtonMask.Trigger))) {
            if (penguin) {

                penguin.GetComponent<PenguinsShouldLearnHowToLove>().Spheniscidomagnetism();
                ex = penguin;
                penguin = null;
            } else {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 4f)) {

                    if (hit.collider.GetComponent<PenguinsShouldLearnHowToLove>()) {
                        if (ex) {
                            ex.GetComponent<PenguinsShouldLearnHowToLove>().Divorce();
                            ex = null;
                        }

                        penguin = hit.collider.GetComponent<Rigidbody>();
                    } else {
                        penguin = null;
                    }
                }
            }
        }

        if (penguin) {
            penguin.velocity = (transform.position + 2 * transform.forward - penguin.position) * 5;
        }
    }

}
