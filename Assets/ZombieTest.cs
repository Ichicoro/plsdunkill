using System;
using System.Linq;
using Fragsurf.Movement;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ZombieTest : NetworkBehaviour {
    private NavMeshAgent _agent;
    private GameObject _player;
    public float damagePerTick = 25f;
    public float tickSizeInSeconds = 1f;
    private Collider _collider;

    [SyncVar] private float lastTick;
    [SyncVar] private uint amountOfClosePlayers = 0;
    
    // Start is called before the first frame update
    public override void OnStartServer() {
        base.OnStartServer();
        _agent = GetComponent<NavMeshAgent>();
        _player = GetClosestPlayer();
        _collider = GetComponent<CapsuleCollider>();
        lastTick = Time.time;
    }

    private GameObject GetClosestPlayer() {
        var players = GameObject.FindGameObjectsWithTag("Player");
        return players.OrderBy(t=> Vector3.Distance(transform.position, t.transform.position)).FirstOrDefault();
    }

    // Update is called once per frame
    void Update() {
        if (!isServer) return;

        _agent.isStopped = amountOfClosePlayers > 0;

        if (_player) {
            _agent.SetDestination(_player.transform.position);
        } else {
            _player = GetClosestPlayer();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!isServer) return;
            
        if (other.GetComponentInParent<SurfCharacter>() != null) {
            amountOfClosePlayers++;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!isServer) return;
        
        float currentTick = Time.time;
            
        var character = other.GetComponentInParent<SurfCharacter>();
        if (character != null && currentTick - lastTick > tickSizeInSeconds
                              && Physics.Raycast(transform.position, other.transform.position)) {
            lastTick = currentTick;
            if (Random.value > .5f) character.TakeDamage(damagePerTick);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!isServer) return;
            
        if (other.GetComponentInParent<SurfCharacter>() != null) {
            amountOfClosePlayers--;
        }
    }
}
