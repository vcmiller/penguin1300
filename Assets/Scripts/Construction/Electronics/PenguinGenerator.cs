using UnityEngine;
using System.Collections;

public class PenguinGenerator : Provider {
    public PenguinSlot ps { get; private set; }
	public override float power{
		get{
			return maxOutput * ps.power;
		}
	}

    public float maxOutput = 1;

    // Use this for initialization
    void Start() {
        ps = GetComponent<PenguinSlot>();
    }

 
}
