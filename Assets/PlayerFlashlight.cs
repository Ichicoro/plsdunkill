using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerFlashlight : NetworkBehaviour {
    public Light lightGameObject;

    [SyncVar(hook=nameof(SetFlashlightEnabled))]
    public bool lightEnabled = false;

    public float lightIntensity = 6.5f;

    // Start is called before the first frame update
    void Start() {
        lightGameObject.enabled = lightEnabled;
    }

    // Update is called once per frame
    void Update() {
        if (base.isLocalPlayer) {
            if (SimpleInput.GetButtonDown("Flashlight")) {
                CmdSwitchFlashlight();
            }
        }
    }

    [Command]
    public void CmdSwitchFlashlight() {
        lightEnabled = !lightEnabled;
    }

    public void SetFlashlightEnabled(bool oldValue, bool newValue) {
        lightGameObject.enabled = newValue;
    }
}
