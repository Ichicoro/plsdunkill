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
        button.onClick.AddListener(() => {
            Debug.Log("Haha! jumped :)");
        });
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    void OnGUI() {
        Event m_Event = Event.current;

        if (m_Event.type == EventType.MouseDown) {
            Debug.Log("Pressed jump");
            isPressed = true;
        }

        /* if (m_Event.type == EventType.MouseDrag) {
            Debug.Log("Mouse Dragged.");
        } */

        if (m_Event.type == EventType.MouseUp) {
            Debug.Log("Released jump");
            isPressed = false;
        }
    }

    IEnumerator ResetNextFrame() {
        yield return null;
        wasJustPressed = false;
    }

}
