using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeathBarrier : NetworkBehaviour {
    public Transform spawnPoint;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    [ClientRpc]
    private void RpcOnRespawn() {
        if (particleSystem) {
            particleSystem.Play();
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.transform.position = spawnPoint.position;
        other.attachedRigidbody.velocity = new Vector3(0, 0, 0);
        RpcOnRespawn();
    }

}
