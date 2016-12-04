using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Map")) {
            SteamVR_LoadLevel.Begin(Game.instance.regions[0].scene);
        } else if (other.CompareTag("MainCamera")) {
            print("DED");
            Application.Quit();
        }
    }
}
