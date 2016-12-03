using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public InputPort ip;
    public OutputPort op;

    public float power {
        get {
            return op.power;
        }
    }
}
