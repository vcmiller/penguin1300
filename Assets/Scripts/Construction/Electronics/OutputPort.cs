using UnityEngine;
using System.Collections;

public class OutputPort : MonoBehaviour {

	private Provider pv;
    public float power { 
		get {
			return pv.power;
		}
	}
    public Link wire { set; get; }

	public void Start(){
		pv = transform.parent.GetComponent<Provider> ();
	}

}
