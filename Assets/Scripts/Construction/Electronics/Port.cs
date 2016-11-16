using UnityEngine;
using System.Collections;

public abstract class Port : MonoBehaviour {


    public Link wire;

    public abstract bool canWireTo(Port port);
}
