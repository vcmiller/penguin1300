using UnityEngine;
using System.Collections;


public class OutputPort : Port {


	private Provider pv;
    public float power { 
		get {
			return pv.power;
		}
	}

	public void Start(){
		pv = transform.parent.GetComponent<Provider> ();
	}

    public override bool canWireTo(Port port) {
        return port is InputPort;
    }
}
