using UnityEngine;
using System.Collections;

public class PausePlayManager : MonoBehaviour {
    public static PausePlayManager instance { get; private set; }

    public bool running { get; private set; }

    private Transform light;
    private Vector3 lightAnglesPlay;
    public Vector3 lightAnglesPause;

    public void Play() {
        running = true;

        light.eulerAngles = lightAnglesPlay;

        foreach (StatusSaver saver in FindObjectsOfType<StatusSaver>()) {
            saver.Save();
        }
    }

    public void Stop() {
        running = false;

        light.eulerAngles = lightAnglesPause;

        foreach (StatusSaver saver in FindObjectsOfType<StatusSaver>()) {
            saver.Load();
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;
        foreach (Light light in FindObjectsOfType<Light>()) {
            if (light.type == LightType.Directional) {
                this.light = light.transform;
                lightAnglesPlay = light.transform.eulerAngles;
                break;
            }
        }

        Stop();
	}
}
