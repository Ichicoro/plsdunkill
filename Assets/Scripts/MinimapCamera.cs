using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {
    public Transform playerCamera;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.eulerAngles = new Vector3(
            90,
            playerCamera.transform.eulerAngles.y,
            0
        );
    }
}
