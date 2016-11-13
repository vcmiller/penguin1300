using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

    public abstract bool BeginInteraction(Controller hand, int button);

    public abstract void EndInteraction();
}
