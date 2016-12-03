using UnityEngine;
using System.Collections;

public abstract class Tool : MonoBehaviour {
    protected Controller controller { get; private set; }
    protected Renderer[] meshes { get; private set; }
    public bool teleportAllowed { get; protected set; }

    public bool active {
        get {
            return isActive;
        }

        set {
            foreach (Renderer mesh in meshes) {
                mesh.enabled = value;
            }

            if (value != isActive) {
                isActive = value;
                enabled = value;
                OnActiveChange(value);
            }
        }
    }

    private bool isActive = false;

    public virtual void Awake() {
        controller = GetComponentInParent<Controller>();
        meshes = GetComponentsInChildren<Renderer>();

        active = false;
        OnActiveChange(false);
        enabled = false;

        teleportAllowed = true;
    }

    protected virtual void OnActiveChange(bool active) {

    }
}
