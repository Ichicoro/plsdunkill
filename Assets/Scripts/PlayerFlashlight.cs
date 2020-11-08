using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerFlashlight : NetworkBehaviour {
    public Light lightGameObject;
    public Camera camera;
    public Vector3 last;

    [SyncVar(hook=nameof(SetFlashlightEnabled))]
    public bool lightEnabled = false;

    public float lightIntensity = 6.5f;

    // Start is called before the first frame update
    void Start() {
        lightGameObject.enabled = lightEnabled;
        camera = GetComponentInChildren<Camera>();
        last = camera.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer) {
            if (SimpleInput.GetButtonDown("Flashlight")) {
                CmdSwitchFlashlight();
                lightGameObject.enabled = !lightGameObject.enabled;
            }
            lightGameObject.transform.eulerAngles = Vector3.Lerp(camera.transform.eulerAngles, last, GetMouseMag()*5f);
            last = camera.transform.eulerAngles;
        }
    }

    [Command]
    public void CmdSwitchFlashlight() {
        lightEnabled = !lightEnabled;
    }

    public void SetFlashlightEnabled(bool oldValue, bool newValue) {
        if (!isLocalPlayer) {
            lightGameObject.enabled = newValue;
        }
    }

    private float GetMouseMag() {
        return (new Vector2(
            SimpleInput.GetAxis("Mouse X"),
            SimpleInput.GetAxis("Mouse Y")
        )).magnitude;
    }
}
