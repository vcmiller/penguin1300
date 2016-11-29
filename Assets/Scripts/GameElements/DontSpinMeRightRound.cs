using UnityEngine;
using System.Collections;

public class DontSpinMeRightRound : MonoBehaviour {
    private Quaternion initial;

	// Use this for initialization
	void Start () {
        initial = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = initial;
	}
}
