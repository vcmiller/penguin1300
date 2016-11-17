using UnityEngine;
using System.Collections;

public class WiringTool : Tool {
    public Port startPort { get; private set; }
    public GameObject wirePrefab;

    public LineRenderer line { get; private set; }

	// Use this for initialization
	public override void Awake () {
        line = GetComponentInChildren<LineRenderer>();
        base.Awake();
	}
	
	// Update is called once per frame
	void Update () {
        if (PausePlayManager.instance.running) {
            startPort = null;
            return;
        }

	    if (controller.input.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            if (!startPort) {
                startPort = getPort();
                ClearPort(startPort);
            } else {
                Port endPort = getPort();
                if (endPort) {
                    ClearPort(endPort);
                    GameObject wireObject = Instantiate(wirePrefab);
                    Link wire = wireObject.GetComponent<Link>();
                    if (endPort is InputPort) {
                        wire.ip = (InputPort)endPort;
                        wire.op = (OutputPort)startPort;
                    } else {
                        wire.ip = (InputPort)startPort;
                        wire.op = (OutputPort)endPort;
                    }
                    startPort.wire = wire;
                    endPort.wire = wire;
                }

                startPort = null;
            }
        }

        if (startPort) {
            line.SetPosition(0, startPort.transform.position);
            line.SetPosition(1, transform.position);
            line.enabled = true;
        } else {
            line.enabled = false;
        }
    }

    private void ClearPort(Port port) {
        if (port && port.wire) {
            Destroy(port.wire.gameObject);
        }
    }

    protected override void OnActiveChange(bool active) {
        if (!active) {
            startPort = null;
            line.enabled = false;
        }
    }

    public Port getPort() {
        foreach (Collider col in Physics.OverlapSphere(transform.position, .2f)) {
            Port p = col.GetComponent<Port>();
            if ((p && startPort == null) || (p != startPort && p.canWireTo(startPort))) {
                return p;
            }
        }

        return null;
    }
}
