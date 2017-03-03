using UnityEngine;
using System.Collections;

public class RegionController : MonoBehaviour {

   // public GameObject penguin;
    Sign[] signs;

    void Start() {
        Invoke("Setup", 1.0f);
    }

	// Use this for initialization
	void Setup () {
        print(Game.instance);
        print(Game.instance.region);
        signs = FindObjectsOfType<Sign>();
        for (int i = 0; i < signs.Length; i++) {
            int index = signs[i].index;
            if (index >= 0 && index < Game.instance.region.levels.Length) {
                signs[i].init(Game.instance.region.levels[index], Game.instance.region.completions[index]);
            } else {
                signs[i].init(Game.instance.hubScene, true);
            }
        }
   	}

}
