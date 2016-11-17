using UnityEngine;
using System.Collections;

public class PausePlayManager : MonoBehaviour {
    public static PausePlayManager instance { get; private set; }

    public bool running { get; private set; }

    public void Play() {
        running = true;

        foreach (StatusSaver saver in FindObjectsOfType<StatusSaver>()) {
            saver.Save();
        }
    }

    public void Stop() {
        running = false;

        foreach (StatusSaver saver in FindObjectsOfType<StatusSaver>()) {
            saver.Load();
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;

        Stop();
	}
}
