using UnityEngine;
using System.Collections;

public class RestartSnow : MonoBehaviour {
    public ParticleSystem snow { get; private set; }
    public Controller[] conts;
    public Transform player;
    float initEmission;

    public float snowTime {
        get {
            float progress = 0;
            foreach (Controller c in conts) {
                progress = Mathf.Max(progress, c.restartProgress);
            }

            return progress;
        }
    }

	// Use this for initialization
	void Start () {
        snow = GetComponentInChildren<ParticleSystem>();
        player = FindObjectOfType<SteamVR_Camera>().transform;
        conts = new Controller[0];

        initEmission = snow.emission.rate.constant;
    }
	
	// Update is called once per frame
	void Update () {
        if (conts.Length < 2) {
            conts = FindObjectsOfType<Controller>();
        }

        ParticleSystem.Particle[] snowflakes = new ParticleSystem.Particle[snow.particleCount];
        snow.GetParticles(snowflakes);

        for(int i = 0; i < snowflakes.Length; i++) { 
            snowflakes[i].position += Time.deltaTime * Vector3.Cross(Vector3.up, player.position - snowflakes[i].position) * 2;
           // snowflakes[i].startColor = new Color(1, 1, 1, snowTime);
        }
        snow.SetParticles(snowflakes, snowflakes.Length );
        ParticleSystem.EmissionModule emiss = snow.emission;
        emiss.rate = snowTime * initEmission;
	}
}
