using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Game : MonoBehaviour {
    public static Game instance { get; private set; }
    static bool loaded = false;

    public string hubScene;
    public Region[] regions;
    public Region region {
        get {

            foreach(Region r in regions) {
                if(System.Array.IndexOf(r.levels, SceneManager.GetActiveScene().name) >= 0) {
                    return r;
                }
                if(SceneManager.GetActiveScene().name == r.scene) {
                    return r;
                }
            }

            return null;
        }
    }

    public string parentScene {
        get {
            if(region == null) {
                return SceneManager.GetActiveScene().name;
            } else if(SceneManager.GetActiveScene().name == region.scene) {
                return hubScene;
            } else {
                return region.scene;
            }
            
        }
    }


	// Use this for initialization
	void Start () {
        if (!loaded) {
            Load();
            loaded = true;
        }
        instance = this;
	}

    public void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        Save();
        SceneManager.LoadScene(region.NextLevel(SceneManager.GetActiveScene().name));
    }

    public void ExitLevel() {
        SceneManager.LoadScene(parentScene);
    }


    void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd");
        bf.Serialize(file, regions);
        file.Close();
    }

    void Load() {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            regions = (Region[])bf.Deserialize(file);
            file.Close();
        }
    }
}
