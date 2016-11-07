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
            //rb.MovePosition(rb.position + lover.transform.TransformPoint(lover.localLoveSpot) - transform.TransformPoint(localLoveSpot));
        }
	}

    public void Spheniscidomagnetism() {

        if (suitor)
        {
            lover = suitor;
            localLoveSpot = potentialLoveSpot;

            SpringJoint child = gameObject.AddComponent<SpringJoint>();

            child.connectedBody = lover.rb;
            child.autoConfigureConnectedAnchor = false;
            child.anchor = Vector3.zero;
            child.connectedAnchor = Vector3.zero;
            child.spring = 50;
            child.damper = 50;
            child.enableCollision = true;
        }

    }

    public void Divorce()
    {
        if (lover != null && localLoveSpot != Vector3.zero)
        {
            Destroy(GetComponent<SpringJoint>());
            
            lover = null;
            localLoveSpot = Vector3.zero;
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
