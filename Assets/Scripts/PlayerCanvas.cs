using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        Input.simulateMouseWithTouches = false;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            GameObject.Find("Mobile touch Input").SetActive(true);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
