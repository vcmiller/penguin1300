using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class Region {

    public string scene;
    public string[] levels;
    public bool[] completions;

    public void MarkLevelComplete(string level) {
        int curIndex = System.Array.IndexOf(levels, level);
        completions[curIndex] = true;

        string db = "";
        foreach(bool c in completions) {
            db += c + " "; 
        }
        Debug.Log(db); 
    }

    public string NextLevel(string level) {
        int curIndex = System.Array.IndexOf(levels, level);
        int newIndex = curIndex + 1;

        if(newIndex < levels.Length) {
            return levels[newIndex];
        } else {
            return scene;
        }
    }
    
}
