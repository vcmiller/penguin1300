using UnityEngine;
using System.Collections;

public class Generator : Provider {
    public Axle ax { get; private set; }

	public override float power{
		get{
			return maxOutput * ax.power;
		}
	}

    public float maxOutput = 1;

	// Use this for initialization
	public void Start () {
        ax = GetComponent<Axle>();
	}

}
