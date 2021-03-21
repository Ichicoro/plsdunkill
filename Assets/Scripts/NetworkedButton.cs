using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Mirror;

public class NetworkedButton: NetworkBehaviour, IInteractableEntity {
    [SyncVar]
    public bool active = false;

    [ShowInInspector]
    public GameObject actionReceiver;

    private ISingleActionReceiver _receiver;

    private void Start() {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        _receiver = actionReceiver.GetComponent<ISingleActionReceiver>();
    }

    [ClientRpc]
    void RpcCalledOnClick() {
        if (active) {
            // active = false;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        } else {
            // active = true;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdExecuteAction(GameObject entity) {
        active = !active;
        Debug.Log("Btn pressed");
        _receiver.ExecuteAction();
        RpcCalledOnClick();
    }
}
