using UnityEngine;
using System.Collections;

public class PenguinsShouldKnowTheirPlace : RigidbodySaver{

	PenguinsShouldYearnForYourTouch psyfyt;

	// Use this for initialization
	public override void Save(){
		if (!psyfyt) {
			psyfyt = GetComponent<PenguinsShouldYearnForYourTouch> ();
		}
		base.Save ();
		transform.position = psyfyt.initPos;
		transform.rotation = Quaternion.identity;
	}
}
