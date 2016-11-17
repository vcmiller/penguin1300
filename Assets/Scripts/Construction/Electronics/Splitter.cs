using UnityEngine;
using System.Collections;

public class Splitter : Provider {

    public InputPort ip { private set; get; }
    
	public override float power{ 
		get { 
			return ip.power / ops.Length;
		} 
	}

    // Use this for initialization
    void Start() {
        ip = GetComponentInChildren<InputPort>();
	}
}
