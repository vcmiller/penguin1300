using UnityEngine;
using System.Collections;
using System;

public class TransformSaver : StatusSaver {
    public Vector3 savedPosition { get; private set; }
    public Quaternion savedRotation { get; private set; }
    public Vector3 savedScale { get; private set; }
    public Transform savedParent { get; private set; }

    private bool saved = false;
    
    public override void Load() {
        if (saved) {
            transform.parent = savedParent;
            transform.localScale = savedScale;
            transform.rotation = savedRotation;
            transform.position = savedPosition;
        }
    }

    public override void Save() {
        savedPosition = transform.position;
        savedRotation = transform.rotation;
        savedScale = transform.localScale;
        savedParent = transform.parent;

        saved = true;
    }
}
