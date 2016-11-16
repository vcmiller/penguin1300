
using UnityEngine;

[System.Serializable]
public class ExpirationTimer {
    public float Expiration;
    public float LastSet { get; set; }

    private bool started = false;

    private void checkStart() {
        if (!started) {
            started = true;
            Clear();
        }
    }

	public bool Expired {
		get {
            checkStart();
			return Time.time - LastSet > Expiration;
		}
	}

    public float Remaining {
        get {
            checkStart();
            return Mathf.Max(0, Expiration - (Time.time - LastSet));
        }
    }

	public ExpirationTimer (float expiration) {
		Expiration = expiration;
		Clear ();
	}

	public void Set() {
        LastSet = Time.time;
	}

	public void Clear() {
        checkStart();
        LastSet = Time.time - Expiration;
	}
}

