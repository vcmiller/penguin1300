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
        if (col.GetComponent<PenguinsShouldLearnHowToLove>()) {
            Destroy(col.GetComponent<PenguinsShouldLearnHowToLove>());
            Destroy(col.GetComponent<PenguinsShouldStand>());
            col.tag = "Default";

            penguinCount++;
            if (penguinCount >= youMustProvideAdditionalPenguins) {
                g.NextLevel();
            }
        }
    }
}
