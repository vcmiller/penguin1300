using UnityEngine;
using System.Collections;

public class PenguinsShouldYearnForYourTouch : DraggableObject{
 
	Vector3 initVel;
	float initSpeed;
	float threshold = 0.2f;

	bool thrown;
	public Vector3 initPos { get; private set; }
    public AudioClip[] sounds;

	LineRenderer lr;

	public override void Awake(){
		base.Awake ();
		initPos = transform.position;
		lr = GetComponent<LineRenderer> ();
		lr.SetPosition (0, transform.position);
	}

	public override void Pickup(int button){
		held = true;

        if (sounds.Length > 0) {
            AudioSource.PlayClipAtPoint(sounds[Random.Range(0, sounds.Length)], transform.position);
        }
	}

	public override void Drop(int button){
		held = false;
		//rigidbody.angularVelocity = angularVelocity;
 
		initVel = rigidbody.velocity;
		initSpeed = initVel.magnitude;
		thrown = true;
		initPos = transform.position;

		lr.SetPosition (0, transform.position);
		lr.SetPosition (1, transform.position);
	}

	void Update(){
		if (!PausePlayManager.instance.running) {
			lr.enabled = !held;
			lr.SetPosition (1, transform.position);
			rigidbody.useGravity = false;

			if (thrown) {
				rigidbody.angularVelocity = Vector3.zero;
				rigidbody.AddForce ((initPos - transform.position) * 15, ForceMode.Acceleration);
				rigidbody.velocity *= Mathf.Pow (0.05f, Time.deltaTime);
				//rigidbody.velocity = Vector3.MoveTowards (rigidbody.velocity, initPos - transform.position, initSpeed * 5f * Time.deltaTime);
				//if (Vector3.Distance (transform.position, initPos) < threshold && Vector3.Dot(rigidbody.velocity, initVel) < 0) {
					//thrown = false;
				//}
			} else {
				rigidbody.velocity = Vector3.MoveTowards (rigidbody.velocity, Vector3.zero, 400f * Time.deltaTime);

			}
		} else {
			lr.enabled = false;
			rigidbody.useGravity = true;
		}

	}
}
