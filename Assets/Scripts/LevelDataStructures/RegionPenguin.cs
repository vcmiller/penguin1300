using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RegionPenguin : MonoBehaviour {
    

    bool initialized = false;
    public string scene { private set; get; }
    public bool completed { private set; get; }

    public void init(string s, bool c) {
        if (!initialized) {
            scene = s;
            completed = c;
            initialized = true;
        }
    }


	
	// Update is called once per frame
	void Update () {

        if (GetComponent<DraggableObject>().held) {
            SteamVR_LoadLevel.Begin(scene);
        }
    }

}
