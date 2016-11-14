using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public InputPort ip { set; get; }
    public OutputPort op { set; get; }

    public float power {
        get {
            return op.power;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
