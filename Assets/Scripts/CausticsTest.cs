using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausticsTest : MonoBehaviour {
    private Light light;

    public bool enabled = true;
    public bool debug = false;
    [Range(1,120)] public int fps = 30;
    public Texture[] textures;
    public int idx;

    // Start is called before the first frame update
    void Start() {
        if (!enabled) return; 
        light = GetComponent<Light>();
        NextFrame();
        InvokeRepeating(nameof(NextFrame), 1/(float) fps, 1/(float) fps);
    }

    // // Update is called once per frame
    // void Update() {
        
    // }

    void NextFrame() {
        if (!enabled) return;

        light.cookie = textures[idx];
        idx = (idx + 1) % textures.Length;
        if (debug) {
            Debug.Log("current frame: " + idx);
        }
    }
}
