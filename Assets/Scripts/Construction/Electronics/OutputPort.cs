using UnityEngine;
using System.Collections;

public class OutputPort : Port {

    public float power { set; get; }

    public override bool canWireTo(Port port) {
        return port is InputPort;
    }
}
