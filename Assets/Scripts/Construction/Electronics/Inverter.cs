using UnityEngine;
using System.Collections;

public class Inverter : Provider {

    public InputPort ip { private set; get; }
	public override float power {
		get{ 
			return (Time.time % period > period * duty) ? ip.power : 0;
		}
	}

    public float period;
    public float duty;

	// Use this for initialization
	void Start () {
        ip = GetComponentInChildren<InputPort>();
	}
	

}
