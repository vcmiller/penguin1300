using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditorAsSIGNment : MonoBehaviour {

    public bool doTheThing;
		
	// Update is called once per frame
	void Update () {
        if (doTheThing) {
            doTheThingy();
            doTheThing = false;
        }
	}

    void doTheThingy() {
        Sign[] signs = FindObjectsOfType<Sign>();

        Sign mySign = GetComponent<Sign>();
        mySign.index = -2;
        foreach(Sign s in signs) {
            if (mySign.index < s.index) {
                mySign.index = s.index;
            }
        }

        mySign.index++;
    }
}
