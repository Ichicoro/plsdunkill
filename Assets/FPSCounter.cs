using UnityEngine;
using System.Collections;
using TMPro;

public class FPSCounter : MonoBehaviour
{
	float deltaTime = 0.0f;
    TextMeshProUGUI tm;

    void Start() {
        tm = GetComponent<TextMeshProUGUI>();
    }
 
	void Update() {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{1:0.} fps ({0:0.0} ms)", fps, msec);
        tm.text = text;
	}
}
