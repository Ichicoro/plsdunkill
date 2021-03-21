using System.Collections;
using System.Collections.Generic;
using Fragsurf.Movement;
using UnityEngine;
using Mirror;
using TMPro;

public class HealingStation: NetworkBehaviour, IInteractableEntity {
    [SyncVar]
    public float chargeLeft = 50f;

    [SyncVar]
    public bool inUse = false;

    public TextMeshPro remainingText;
    public MeshRenderer meshRenderer;
    
    void Start() {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.green;
        remainingText.SetText($"{chargeLeft}");
    }

    [ClientRpc]
    void RpcCalledOnClick() {
        remainingText.SetText($"{chargeLeft}");
        if (chargeLeft == 0) {
            meshRenderer.material.color = Color.red;
        } else {
            meshRenderer.material.color = Color.green;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdExecuteAction(GameObject user) {
        Debug.Log("Called");
        var player = user.GetComponent<SurfCharacter>();
        if (player != null && chargeLeft > 0f && player.health <= 90) {
            player.Heal(Mathf.Min(chargeLeft, 10f));
            if (chargeLeft - 10f < 0) chargeLeft = 0;
            else chargeLeft -= 10f;
        }
        RpcCalledOnClick();
    }
}
