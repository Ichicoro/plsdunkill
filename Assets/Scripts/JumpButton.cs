using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour {
    private Button button;
    public bool isPressed = false;
    public bool wasJustPressed = false;

    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
