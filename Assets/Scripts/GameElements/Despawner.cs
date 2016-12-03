using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour {

    public GameObject splashPrefab;

	void OnTriggerEnter(Collider col) {
        if (col.GetComponent<PenguinFlap>()) {
            Destroy(Instantiate(splashPrefab, col.transform.position, Quaternion.Euler(-90,0,0)), 1f);
            //Destroy(col.gameObject);
        }
    }
}
