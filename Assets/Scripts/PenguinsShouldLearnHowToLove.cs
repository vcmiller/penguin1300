using UnityEngine;
using System.Collections;

public class PenguinsShouldLearnHowToLove : MonoBehaviour {

    PenguinsShouldLearnHowToLove suitor;
    Vector3 potentialLoveSpot;

    PenguinsShouldLearnHowToLove lover;
    Vector3 localLoveSpot;

    Rigidbody rb;
    

	// Use this for initialization
	void Start () {
        lover = null;
        localLoveSpot = Vector3.zero;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (lover != null) {
            rb.MovePosition(rb.position + lover.transform.TransformPoint(lover.localLoveSpot) - transform.TransformPoint(localLoveSpot));
        }
	}

    public void Spheniscidomagnetism() {

        if (potentialLoveSpot.magnitude > 0)
        {
            lover = suitor;
            localLoveSpot = potentialLoveSpot;
        }

    }

    public void Divorce()
    {
        if (lover != null && localLoveSpot != Vector3.zero)
        {
            PenguinsShouldLearnHowToLove ex = lover;
            lover = null;
            localLoveSpot = Vector3.zero;
            ex.Divorce();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (suitor = col.gameObject.GetComponent<PenguinsShouldLearnHowToLove>())
        {
            potentialLoveSpot = transform.InverseTransformPoint(col.contacts[0].point);
        }else
        {
            suitor = null;
            potentialLoveSpot = Vector3.zero;
        }
    }
}
