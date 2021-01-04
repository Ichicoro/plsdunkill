using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DisableOnGameStart : NetworkBehaviour {
    void Awake() {
        Debug.Log("awakened");
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("started");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
