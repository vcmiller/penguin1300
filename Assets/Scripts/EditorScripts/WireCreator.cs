using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WireCreator : MonoBehaviour {
    public InputPort input;
    public OutputPort output;

    public GameObject wirePrefab;
    public bool createWire;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (createWire) {
            createWire = false;

            if (input && output && wirePrefab) {
                if (input.wire) {
                    DestroyImmediate(input.wire);
                }

                if (output.wire) {
                    DestroyImmediate(output.wire);
                }
            }

            GameObject wireObject = Instantiate(wirePrefab);
            Link link = wireObject.GetComponent<Link>();
            link.ip = input;
            link.op = output;
            input.wire = link;
            output.wire = link;
        }
	}
}
