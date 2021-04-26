using System;
using Fragsurf.Movement;
using Mirror;
using UnityEngine;

public class HealthPack : NetworkBehaviour {
    [SyncVar] public float healthAmount = 25f;
    private AudioSource _as;

    // Start is called before the first frame update
    void Start() {
        Debug.Log(isServer);
    }

    private void OnTriggerEnter(Collider other) {
        if (!isServer) return;
        
        var player = other.GetComponentInParent<SurfCharacter>();
        if (player != null) {
            player.Heal(healthAmount);
            player.RpcPlayHEVSound("Sounds/smallmedkit1");
            NetworkServer.Destroy(gameObject);
        }
    }
}
