using UnityEngine;
using System.Collections;

public class Sender : MonoBehaviour {

    public Receiver[] rc;
    public int sends = 1;
    public string targetTag;
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col) {
        if (col.transform.CompareTag(targetTag)) {
            Send();
        }
    }
	
	

    public void Send() {
        if (sends > 0) {
            sends--;
            foreach (Receiver r in rc) {
                r.Receive();
            }
        }
    }
}
