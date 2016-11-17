using UnityEngine;
using System.Collections;

public class Merger : Provider {

    public InputPort[] ips { private set; get; }

	public override float power {
		get {
			float pow = 0;
			foreach(InputPort ip in ips){
				pow += ip.power;
			}
			return pow;
		}
	}

	// Use this for initialization
	void Start () {
        ips = GetComponentsInChildren<InputPort>();
	}
 
}
