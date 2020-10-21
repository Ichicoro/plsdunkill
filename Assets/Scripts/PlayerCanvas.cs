using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCanvas : NetworkBehaviour {
    // Start is called before the first frame update
    void Start() {
        Input.simulateMouseWithTouches = false;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            GameObject.Find("Mobile touch Input").SetActive(true);
        } else {
            GameObject.Find("Mobile touch Input").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
