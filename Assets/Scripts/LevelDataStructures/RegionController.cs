using UnityEngine;
using System.Collections;

public class RegionController : MonoBehaviour {

   // public GameObject penguin;
    RegionPenguin[] penguins;

	// Use this for initialization
	void Start () {
        penguins = FindObjectsOfType<RegionPenguin>();
        for(int i = 0; i < penguins.Length && i < Game.instance.region.levels.Length; i++) {
            penguins[i].init(Game.instance.region.levels[i], Game.instance.region.completions[i]);
        }     
   	}

}
