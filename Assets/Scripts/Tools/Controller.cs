using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    public SteamVR_TrackedObject controller { get; private set; }
    public SteamVR_Controller.Device input { get { return SteamVR_Controller.Input((int)controller.index); } }

    public List<Tool> tools { get; private set; }
    public LineRenderer line { get; private set; }
    public bool isTeleporting { get; private set; }

    public int toolIndex = 0;

    public ExpirationTimer restartLevelTimer;

    
    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedObject>();
        line = GetComponent<LineRenderer>();

        tools = new List<Tool>(GetComponentsInChildren<Tool>());

        tools[toolIndex].active = true;
    }
	
	// Update is called once per frame
	void Update () {
        int oldIndex = toolIndex;

        if (input.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            float x = input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
            float y = input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

            float telRange = .6f;

            if (x > telRange) {
                toolIndex++;
            } else if (x < -telRange) {
                toolIndex--;
            } else {
                isTeleporting = true;
            }

            if (toolIndex != oldIndex) {
                if (toolIndex >= tools.Count) {
                    toolIndex = 0;
                
                } else if (toolIndex < 0) {
                    toolIndex = tools.Count - 1;
                }

                tools[oldIndex].active = false;
                tools[toolIndex].active = true;
             }
            
        }

        if (input.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (isTeleporting) {
                Teleport();
                isTeleporting = false;
                TeleportTarget.show = false;
            }
        }

        bool canReset = true;
        if (input.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            restartLevelTimer.Set();
            canReset = false;

            if (PausePlayManager.instance.running) {
                PausePlayManager.instance.Stop();
            } else {
                PausePlayManager.instance.Play();
            }
        }

        if (input.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu) && restartLevelTimer.Expired && canReset) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        UpdateLineRenderer();
        if (isTeleporting) {
            TeleportTarget.show = true;
        }
    }

    void Teleport() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.CompareTag("Teleport")) {
                transform.root.position = hit.point;
            }
        }
    }

    void UpdateLineRenderer() {
        line.enabled = false;
        if (isTeleporting) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                line.enabled = true;
                line.SetPosition(1, new Vector3(0, 0, hit.distance));
                line.material.SetTextureScale("_MainTex", new Vector2(hit.distance * 4, 1));
            }

            line.material.SetTextureOffset("_MainTex", new Vector2(-4 * Time.time, 1));
        }
        
    }

    public void AddTool(GameObject prefab) {
        GameObject newToolObject = Instantiate(prefab);
        newToolObject.transform.parent = transform;
        newToolObject.transform.localPosition = Vector3.zero;
        newToolObject.transform.localRotation = Quaternion.identity;

        Tool newTool = newToolObject.GetComponentInChildren<Tool>();
        tools.Add(newTool);
    }
}
