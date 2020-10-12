using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour {
    [Range(1, 300)]
    public int targetFPS = 60;

    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = targetFPS;
    }
}
