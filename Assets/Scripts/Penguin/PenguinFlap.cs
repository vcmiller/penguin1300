using UnityEngine;
using System.Collections;

public class PenguinFlap : MonoBehaviour {
    public Transform wingLeft;
    public Transform wingRight;
    public float timescale = 60;

    public CooldownTimer activateFlap;
    public ExpirationTimer deactivateFlap;

    public DraggableObject draggable { get; private set; }
	LineRenderer lr;

	// Use this for initialization
	void Start () {
        activateFlap.Randomize();

        draggable = GetComponent<DraggableObject>();
		lr = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (activateFlap.Use) {
            deactivateFlap.Set();
        }


		if (!lr.enabled && (!deactivateFlap.Expired || draggable.held || !GetComponent<PenguinStand>().grounded)) {
            wingLeft.localEulerAngles = new Vector3(Mathf.Sin(Time.time * timescale) * 20 - 20, -90, 90);
            wingRight.localEulerAngles = new Vector3(-Mathf.Sin(Time.time * timescale) * 20 + 20, -90, -90);
        } else {
            wingLeft.localEulerAngles = new Vector3(0, -90, 90);
            wingRight.localEulerAngles = new Vector3(0, -90, -90);
        }
    }
}
