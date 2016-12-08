using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    public Vector3 teleportPos { get; private set; }

    void Start() {
        teleportPos = transform.GetChild(0).localPosition;
    }

    public Vector3 target {
        get {
            return transform.TransformPoint(teleportPos);
        }
    }
}
