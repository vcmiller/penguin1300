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

    float restartTimer = 0;
    public float timeToRestart = 2;
    public float restartProgress {
        get {
            return restartTimer / timeToRestart;
        }
    }

    public Tool curTool {
        get {
            return tools[toolIndex];
        }
    }

    
    // Use this for initialization
    void Start () {
        controller = GetComponent<SteamVR_TrackedObject>();
        line = GetComponent<LineRenderer>();

        tools = new List<Tool>(GetComponentsInChildren<Tool>());

        curTool.active = true;
    }
	
	// Update is called once per frame
	void Update () {
        int oldIndex = toolIndex;

        if (curTool.teleportAllowed && input.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            float x = input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
            float y = input.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

            float telRange = .6f;

            if (x > telRange) {
                toolIndex++;
            } else if (x < -telRange) {
                toolIndex--;
            } else if (curTool.teleportAllowed) {
                isTeleporting = true;
            }

            if (toolIndex != oldIndex) {
                if (toolIndex >= tools.Count) {
                    toolIndex = 0;
                
                } else if (toolIndex < 0) {
                    toolIndex = tools.Count - 1;
                }

                tools[oldIndex].active = false;
                curTool.active = true;
             }
            
        }

        if (input.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (isTeleporting) {
                Teleport();
                isTeleporting = false;
                TeleportView.show = false;
            }
        }

        bool canReset = true;
        if (input.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            canReset = false;

            if (PausePlayManager.instance.running) {
                PausePlayManager.instance.Stop();
            } else {
                PausePlayManager.instance.Play();
            }
        }

        if (input.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            restartTimer += Time.deltaTime;
        } else {
            restartTimer = 0;
        }

        if (restartTimer > timeToRestart) {
            SteamVR_LoadLevel.Begin(SceneManager.GetActiveScene().name);
            restartTimer = 0;
        }

        UpdateLineRenderer();
        if (isTeleporting) {
            TeleportView.show = true;
        }
    }

    void Teleport() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.CompareTag("Teleport")) {
                transform.root.position = hit.point;
                SteamVR_Fade.Start(Color.black, 0);
                SteamVR_Fade.Start(Color.clear, 1f);
            }else if (hit.collider.GetComponent<Sign>()) {
                UseSign(hit.collider.GetComponent<Sign>());
            }
        }
    }

    void UseSign(Sign s) {
        s.Go();
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
