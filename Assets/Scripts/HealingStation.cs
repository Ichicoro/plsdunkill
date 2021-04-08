using Fragsurf.Movement;
using UnityEngine;
using Mirror;
using TMPro;

public class HealingStation: NetworkBehaviour, IInteractableEntity {
    [SyncVar] public float chargeLeft = 100f;
    [SyncVar] public float healthGivenPerFrame = 1f; 
    // [SyncVar] public bool inUse = false;
    [SyncVar] public float peepeepoopoo = .100f;
    [SyncVar] public float lastTimeUsed = 0f;

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

    // [Command(requiresAuthority = false)]
    public void CmdExecuteAction(GameObject user, bool usedOnce) {
        var player = user.GetComponent<SurfCharacter>();
        if (player != null && chargeLeft > 0f && player.health <= 99 && Time.time > lastTimeUsed + peepeepoopoo) {
            lastTimeUsed = Time.time;
            player.Heal(Mathf.Min(chargeLeft, healthGivenPerFrame));
            if (chargeLeft - healthGivenPerFrame < 0) chargeLeft = 0;
            else chargeLeft -= healthGivenPerFrame;
        }
        RpcCalledOnClick();
    }
}
