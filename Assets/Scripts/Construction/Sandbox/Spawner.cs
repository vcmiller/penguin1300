using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject toSpawn;
	public float leashDistance = 5.0f;
	Transform spawned;

	// Use this for initialization
	void Start () {
		name += "(" + toSpawn.name + ")";
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
        //if (!PausePlayManager.instance.running) {
        if (!spawned || Vector3.Distance(transform.position, spawned.position) > leashDistance) {
            Spawn();
        }
        //}
		
	}

	void Spawn(){
		spawned = ( (GameObject)Instantiate (toSpawn, transform.position, Quaternion.identity) ).transform;

        foreach (StatusSaver saver in spawned.GetComponentsInChildren<StatusSaver>()) {
            saver.Load();
        }
	}
}
