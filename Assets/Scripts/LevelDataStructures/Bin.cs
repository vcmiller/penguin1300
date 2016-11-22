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
        if (col.GetComponent<PenguinLove>()) {
            Destroy(col.GetComponent<PenguinLove>());
            Destroy(col.GetComponent<PenguinStand>());

            penguinCount++;
            if (penguinCount >= youMustProvideAdditionalPenguins) {
                g.NextLevel();
            }
        }
    }
}
