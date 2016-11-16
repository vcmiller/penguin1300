using UnityEngine;
using System.Collections;

public class FloorButton : MonoBehaviour {
    public OutputPort output { get; private set; }
    public Transform button { get; private set; }

    public float power = 1;

    private bool pressed;

	// Use this for initialization
	void Start () {
        button = transform.GetChild(0);
        output = GetComponentInChildren<OutputPort>();
	}
	
	// Update is called once per frame
	void Update () {
        button.localPosition = Vector3.MoveTowards(button.localPosition, pressed ? new Vector3(0, .1f, 0) : new Vector3(0, .5f, 0), Time.deltaTime);

        output.power = pressed ? power : 0;
	}

    void OnTriggerStay(Collider other) {
        pressed = true;
    }

    void OnTriggerExit(Collider other) {
        pressed = false;
    }
}
