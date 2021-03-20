using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkedButton: NetworkBehaviour, IInteractableEntity {
    [SyncVar]
    public bool enabled = false;
    public GameObject light;
    public ParticleSystem particleSystem;

    void Start() {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }


    void Update() {
        
    }

    [ClientRpc]
    void RpcCalledOnClick() {
        if (particleSystem) {
            particleSystem.Play();
        }
        if (enabled) {
            enabled = false;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            if (light) {
                light.GetComponent<Light>().enabled = false;
            }
        } else {
            enabled = true;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            if (light) {
                light.GetComponent<Light>().enabled = true;
            }
        }
    }

    [Command(ignoreAuthority = true)]
    public void CmdExecuteAction(GameObject entity) {
        RpcCalledOnClick();
    }
}
