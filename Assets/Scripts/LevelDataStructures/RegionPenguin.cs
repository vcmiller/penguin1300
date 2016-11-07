using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RegionPenguin : MonoBehaviour {

    Vector3 initPos;
    public float maxElevation = 1;
    public float proximity = 5;

    bool initialized = false;
    public string scene { private set; get; }
    public bool completed { private set; get; }

    public void init(string s, bool c) {
        if (!initialized) {
            scene = s;
            completed = c;
            initialized = true;
        }
    }

	// Use this for initialization
	void Start () {
        initPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, FindObjectOfType<PleaseAttachAColliderToMyHead>().transform.position);
        transform.localPosition = initPos + transform.up * maxElevation * Mathf.Min(1, 1 - (distance / proximity));
	}

    void OnCollisionEnter(Collision col) {
        if (col.transform.CompareTag("Selector")) {
            SceneManager.LoadScene(scene);
        }
    }
}
