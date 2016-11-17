using UnityEngine;
using System.Collections;
using System;

public class InputPort : Port {

    public float power {
        get {
            return wire ? wire.power : 0;
        }
    }

    public override bool canWireTo(Port port) {
        return port is OutputPort;
    }
}
