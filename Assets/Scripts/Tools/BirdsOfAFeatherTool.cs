using UnityEngine;
using System.Collections;

public class BirdsOfAFeatherTool : Tool {

    Rigidbody penguin;
    bool gripping;

    void Update() {
        if (penguin != null) {
            print("is " + gripping + penguin.name);
        } else {
            print("is " + gripping);
        }
        
        if (input.GetPressDown((SteamVR_Controller.ButtonMask.Trigger)) && penguin != null) {
            if (gripping) {
                penguin.GetComponent<PenguinsShouldLearnHowToLove>().Spheniscidomagnetism();
            }

            gripping = !gripping;
        }

        if (gripping) {
            penguin.velocity = (transform.position + 2 * transform.forward - penguin.position) * 5;
        } else {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 4f)) {
                if (hit.collider.GetComponent<PenguinsShouldLearnHowToLove>()) {
                    penguin = hit.collider.GetComponent<Rigidbody>();
                }else {
                    penguin = null;
                }
            }
        }
    }

    void OnTriggerStay(Collider col) {
        if (!gripping) {
            if (col.GetComponent<PenguinsShouldLearnHowToLove>()) {
                penguin = col.GetComponent<Rigidbody>();
            } else {
                penguin = null;
            }
        }
    }
}
