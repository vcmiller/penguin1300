using UnityEngine;
using System.Collections;

public abstract class Provider : MonoBehaviour {

	public OutputPort[] ops {get; private set;}
	public virtual float power{ get { return 0;}}
		
	// Use this for initialization
	protected virtual void Awake () {
		ops = GetComponentsInChildren<OutputPort> ();	
	}


}
