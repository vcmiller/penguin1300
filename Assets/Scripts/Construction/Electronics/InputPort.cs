using UnityEngine;
using System.Collections;

public class InputPort : MonoBehaviour {

    public float power {
        get {
            return wire.power;
        }
    }
    public Link wire { set; get; }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
