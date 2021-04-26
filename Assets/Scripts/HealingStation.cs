using Fragsurf.Movement;
using UnityEngine;
using Mirror;
using TMPro;

public class HealingStation: NetworkBehaviour, IInteractableEntity {
    [SyncVar] public float chargeLeft = 100f;
    [SyncVar] public float healthGivenPerFrame = 1f; 
    [SyncVar(hook=nameof(ManageSound))] public bool inUse = false;
    [SyncVar] public float startedUsing;
    [SyncVar] public float peepeepoopoo = .100f;
    [SyncVar] public float lastTimeUsed = 0f;
    [SyncVar] private float soundEndAllowance = .05f;

    public TextMeshPro remainingText;
    public MeshRenderer meshRenderer;
    public AudioSource startAudioSource;
    public AudioSource loopAudioSource;

    public AudioClip startAudioClip;
    public AudioClip loopAudioClip;
    // public AudioClip endAudioClip;
    
    void Start() {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.green;
        remainingText.SetText($"{chargeLeft}");
    }

    void Update() {
        if (isServer) {
            CmdServerUpdate();
        }
        if (!inUse) {
            meshRenderer.material.color = Color.red;
        } else {
            meshRenderer.material.color = Color.green;
            // if (startedUsing < 2f) {
            //     _startAudioSource.volume = 1f;
            //     _loopAudioSource.volume = 0f;
            //     if (startedUsing == 0f) {
            //         _startAudioSource.Play();
            //     }
            // } else {
            //     _loopAudioSource.volume = 1f;
            //     _startAudioSource.volume = 0f;
            // }
        }
    }

    void CmdServerUpdate() {
        if (Time.time - peepeepoopoo - soundEndAllowance > lastTimeUsed) {
            inUse = false;
        }
    }

    void ManageSound(bool oldState, bool newState) {
        if (oldState == newState) return;

        if (newState) {
            startAudioSource.PlayOneShot(startAudioClip);
            loopAudioSource.PlayDelayed(startAudioClip.length - .2f);
        } else {
            loopAudioSource.Stop();
            startAudioSource.PlayOneShot((AudioClip) Resources.Load("Sounds/health/suitchargeno1"));
        }
    }

    [ClientRpc]
    void RpcCalledOnClick() {
        remainingText.SetText($"{chargeLeft}");
        return;
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
            inUse = true;
            player.Heal(Mathf.Min(chargeLeft, healthGivenPerFrame));
            if (chargeLeft - healthGivenPerFrame < 0) chargeLeft = 0;
            else chargeLeft -= healthGivenPerFrame;
        }
        RpcCalledOnClick();
    }
}
