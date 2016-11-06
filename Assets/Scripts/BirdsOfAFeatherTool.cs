using UnityEngine;
using System.Collections;

public class BirdsOfAFeatherTool : Tool {

    Rigidbody penguin;
    bool gripping;

    void Update()
    {
        if (input.GetPress((SteamVR_Controller.ButtonMask.Trigger)) && penguin != null)
        {
            if (gripping) {
                penguin.GetComponent<PenguinsShouldLearnHowToLove>().Spheniscidomagnetism();
            }

            gripping = !gripping;            
        }

        if (gripping)
        {
            penguin.AddForce(transform.position - penguin.position, ForceMode.Acceleration);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!gripping) {
            if (col.GetComponent<PenguinsShouldLearnHowToLove>())
            {
                penguin = col.GetComponent<Rigidbody>();
            }else
            {
                penguin = null;
            }
        }
    }
