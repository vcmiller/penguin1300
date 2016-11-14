using UnityEngine;
using System.Collections;

public class Splitter : MonoBehaviour {

    public InputPort ip { private set; get; }
    public OutputPort[] ops { private set; get; }

    // Use this for initialization
    void Start() {
        ip = GetComponentInChildren<InputPort>();
        ops = GetComponentsInChildren<OutputPort>();
    }

    void Update() {
        foreach(OutputPort op in ops) {
            op.power = ip.power / ops.Length;
        }
    }
}
