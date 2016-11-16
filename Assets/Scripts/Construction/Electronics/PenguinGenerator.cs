using UnityEngine;
using System.Collections;

public class PenguinGenerator : MonoBehaviour {
    public PenguinSlot ps { get; private set; }
    public OutputPort op { get; private set; }

    public float maxOutput = 1;

    // Use this for initialization
    void Start() {
        ps = GetComponentInChildren<PenguinSlot>();
        op = GetComponentInChildren<OutputPort>();
    }

    // Update is called once per frame
    void Update() {
        if (op) {
            op.power = maxOutput * ps.power;
        }
    }
}
