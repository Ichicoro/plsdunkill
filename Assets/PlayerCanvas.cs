﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerCanvas : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    // Input.simulateMouseWithTouches = false;
        EnhancedTouchSupport.Enable();
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
