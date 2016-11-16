using UnityEngine;
using System.Collections;

public class PenguinFlap : MonoBehaviour {
    public Transform wingLeft;
    public Transform wingRight;
    public float timescale = 60;

    public CooldownTimer activateFlap;
    public ExpirationTimer deactivateFlap;

    public DraggableObject draggable { get; private set; }

	// Use this for initialization
	void Start () {
        activateFlap.Randomize();

        draggable = GetComponent<DraggableObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (activateFlap.Use) {
            deactivateFlap.Set();
        }

        if (!deactivateFlap.Expired || draggable.held) {
            wingLeft.localEulerAngles = new Vector3(Mathf.Sin(Time.time * timescale) * 20 - 20, -90, 90);
            wingRight.localEulerAngles = new Vector3(-Mathf.Sin(Time.time * timescale) * 20 + 20, -90, -90);
        } else {
            wingLeft.localEulerAngles = new Vector3(0, -90, 90);
            wingRight.localEulerAngles = new Vector3(0, -90, -90);
        }
    }
}
