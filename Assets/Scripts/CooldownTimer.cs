
using UnityEngine;

[System.Serializable]
public class CooldownTimer {
    public float Cooldown;
    public float LastUse { get; set; }

    private bool started = false;

    private void checkStart() {
        if (!started) {
            started = true;
            LastUse = Time.time;
        }
    }
	
	public bool Use {
		get {
            checkStart();
			if (Time.time - LastUse > Cooldown) {
				LastUse = Time.time;
				return true;
			} else {
				return false;
			}
		}
	}

    public void Randomize() {
        checkStart();
        LastUse = Time.time - Random.value * Cooldown;
    }

    public CooldownTimer (float cooldown) {
		Cooldown = cooldown;
		LastUse = Time.time;
	}

	public void Clear() {
		LastUse = Time.time - Cooldown;
	}

    public bool CanUse {
        get {
            checkStart();
            return Time.time - LastUse > Cooldown;
        }
    }
}

