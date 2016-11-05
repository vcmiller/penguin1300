using UnityEngine;
using System.Collections;

public class PleaseAttachAColliderToMyHead : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetChild(2).gameObject.AddComponent<SphereCollider>().radius = 0.2f;
        transform.GetChild(2).tag = "Player";

    }
}
