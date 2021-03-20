using System;
using Fragsurf.Movement;
using Mirror;
using UnityEngine;

public class DamageTrigger : NetworkBehaviour {
    public float damagePerTick = 25f;
    public float tickSizeInSeconds = 1f;

    private float lastTick;

    public void Start() {
        lastTick = Time.time;
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log(other);
        float currentTick = Time.time;
            
        var character = other.GetComponentInParent<SurfCharacter>();
        if (character != null && currentTick - lastTick > tickSizeInSeconds) {
            lastTick = currentTick;
            character.TakeDamage(damagePerTick);
        }
    }
}