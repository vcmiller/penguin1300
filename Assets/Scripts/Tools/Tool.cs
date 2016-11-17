using UnityEngine;
using System.Collections;

public abstract class Tool : MonoBehaviour {
    protected Controller controller { get; private set; }
    protected Renderer[] meshes { get; private set; }

    public bool active {
        get {
            return isActive;
        }

        set {
            if (value != isActive) {
                isActive = value;
                OnActiveChange(value);
                enabled = value;
            }

            foreach (Renderer mesh in meshes) {
                mesh.enabled = value;
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
    }

    protected virtual void OnActiveChange(bool active) {

    }
}
