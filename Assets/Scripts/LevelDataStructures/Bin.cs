using UnityEngine;
using System.Collections;

public class Bin : MonoBehaviour {

    public int youMustProvideAdditionalPenguins = 1;
    int penguinCount = 0;

    Game g;

	// Use this for initialization
	void Start () {
        g = Game.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<PenguinStand>() && col.GetComponent<PenguinStand>().enabled) {
            Destroy(col.GetComponent<PenguinStand>());
            Destroy(col.GetComponent<PenguinFlap>());
            Destroy(col.GetComponent<DraggableObject>());
            Destroy(col.GetComponent<Rigidbody>());
            col.transform.parent = transform;

            penguinCount++;
            if (penguinCount >= youMustProvideAdditionalPenguins) {
                g.NextLevel();
            }
        }
    }
}
