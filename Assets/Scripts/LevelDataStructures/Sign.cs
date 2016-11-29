using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(EditorAsSIGNment))]
public class Sign : MonoBehaviour {
    public string sceneName { get; private set; }
    public bool completed { get; private set; }
    public int index;

    public void init(string sceneName, bool completed) {
        this.sceneName = sceneName;
        this.completed = completed;
    }

	public void Go() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

}
